using UnityEngine;
using System.Collections.Generic;
using System;
using Utils;
using Registration.Messages;
using System.Linq;
using System.Globalization;

namespace Registration
{
	public class ICPRegisterer
	{
		private List<GameObject> Listeners = new List<GameObject>();

		private Settings Settings;

		private Counter iterationCounter;

		private Action FinishedCallBack;

		private List<Point> StaticPoints;
		private CorrespondenceCollection Correspondences;

		private SamplingInformation ModelSamplingInformation;

		private float error = float.MaxValue;

		private float errorThreshold;

		private StabilizationTermiationCondition stabilization;

		private bool hasTerminated;
		public bool HasTerminated { get { return hasTerminated; } }

		private string errorsString = "";

		private delegate void SendMessageDelegate();

		private SendMessageDelegate stepEndNotification;
		private SendMessageDelegate preparationStepEndNotification;

		#region staticfragment
		private GameObject StaticFragment
		{
			get { return staticFragment; }
			set
			{
				staticFragment = value;
				staticFragment.SendMessage(
					"OnToggleIsICPFragment",
					Fragment.ICPFragmentType.Static,
					SendMessageOptions.DontRequireReceiver
				);
				AddListener(staticFragment);
			}
		}
		private GameObject staticFragment;
		#endregion

		#region modelfragment
		public GameObject ModelFragment
		{
			get { return modelFragment; }
			set
			{
				modelFragment = value;
				modelFragment.SendMessage(
					"OnToggleIsICPFragment",
					Fragment.ICPFragmentType.Model,
					SendMessageOptions.DontRequireReceiver
				);
				AddListener(modelFragment);
			}
		}

		private float Error
		{
			get { return error; }

			set
			{
				error = value;
				stabilization.AddError(error);
				StoreError(error);
			}
		}

		private GameObject modelFragment;
		#endregion

		public ICPRegisterer(
			GameObject staticFragment, GameObject modelFragment,
			Settings settings,
			Action callBack = null
		)
		{
			StaticFragment = staticFragment;
			ModelFragment = modelFragment;

			Settings = settings;
			FinishedCallBack = callBack;

			iterationCounter = new Counter(Settings.MaxNumIterations);

			stabilization = new StabilizationTermiationCondition();

			setNotifcationFunctions();

			hasTerminated = false;

			//The static fragment does not change during ICP, consequently its points need only be sampled once.
			StaticPoints = SamplePoints(StaticFragment);

			ModelSamplingInformation = new SamplingInformation(ModelFragment);

			settings.ErrorMetric.Set(staticFragment, settings.ReferenceTransform);
		}

		private void setNotifcationFunctions()
		{
			if (CLI.Instance.CLIModeActive)
			{
				stepEndNotification = DontNotify;
				preparationStepEndNotification = DontNotify;
			}
			else
			{
				stepEndNotification = GUIStepEndNotification;
				preparationStepEndNotification = GUIPreparationStepEndNotification;
			}
		}

		private void GUIStepEndNotification()
		{
			SendMessageToAllListeners(
				"OnStepCompleted",
				new ICPStepCompletedMessage(this.iterationCounter.CurrentCount, this.Error)
			);
		}

		private void GUIPreparationStepEndNotification()
		{
			SendMessageToAllListeners(
				"OnPreparationStepCompleted",
				new ICPPreparationStepCompletedMessage(
					this.Correspondences,
					this.Settings.ReferenceTransform,
					//The counter is only updated after the step has been set
					this.iterationCounter.CurrentCount + 1
				)
			);
		}

		private void DontNotify()
		{
			//do nothing
		}

		private float ComputeIntialError(CorrespondenceCollection initialCorrespondences)
		{
			if (initialCorrespondences.Count < 6)
			{
				Terminate(ICPTerminatedMessage.TerminationReason.Error, "Could only find " + initialCorrespondences.Count + " correspondences to compute the initial error.");
				return float.NaN;
			}
			else
			{
				float initialError = Settings.ErrorMetric.ComputeInitialError(initialCorrespondences);
				this.Error = initialError;
				return initialError;
			}
		}

		public void AddListener(GameObject listener)
		{
			Listeners.Add(listener);
		}

		public void RunUntilTermination()
		{
			while (!HasTerminated)
			{
				PrepareStep();
				Step();
			}
		}

		private void StoreError(float newError)
		{
			string newErrorString = newError.ToString("E10", CultureInfo.InvariantCulture);
			if (this.errorsString.Equals("")) this.errorsString = newErrorString;
			this.errorsString += (" " + newErrorString);
		}

		private float ComputeErrorThreshold(float initialError)
		{
			return Settings.ErrorThresholdScale * initialError;
		}

		public void PrepareStep()
		{
			if (HasTerminated) return;

			Correspondences = ComputeCorrespondences(StaticPoints);
			Correspondences = FilterCorrespondences(Correspondences);

			if (iterationCounter.AtFirstCount())
			{
				float initialError = ComputeIntialError(Correspondences);
				if (HasTerminated) return;
				this.errorThreshold = ComputeErrorThreshold(initialError);
				SendMessageToAllListeners("OnICPStarted", new ICPStartedMessage(initialError, this.errorThreshold));
			}
			this.preparationStepEndNotification();

			TerminateIfNeeded();
		}

		public void Step()
		{
			if (HasTerminated) return;
			iterationCounter.Increase();

			Matrix4x4 transformationMatrix = Settings.TransFormFinder.FindTransform(Correspondences);
			TransformModelFragment(transformationMatrix);

			Error = Settings.ErrorMetric.ComputeTerminationError(
				correspondences: Correspondences,
				originalTransform: Settings.ReferenceTransform,
				currentTransform: ModelFragment.transform
			);
			this.stepEndNotification();

			TerminateIfNeeded();
		}

		private void TerminateIfNeeded()
		{
			string message;

			if (iterationCounter.IsCompleted()) Terminate(ICPTerminatedMessage.TerminationReason.ExceededNumberOfIterations);
			if (ErrorBelowThreshold()) Terminate(ICPTerminatedMessage.TerminationReason.ErrorBelowThreshold);
			if (InvalidCorrespondences(out message)) Terminate(ICPTerminatedMessage.TerminationReason.Error, message);
			if (stabilization.ErrorHasStabilized()) Terminate(ICPTerminatedMessage.TerminationReason.ErrorStabilized);
		}

		private bool ErrorBelowThreshold()
		{
			return Error < this.errorThreshold;
		}

		private bool InvalidCorrespondences(out string message)
		{
			message = "Found fewer than six correspondences cannot register without at least six correspondences.";
			return Correspondences.Count < 6;
		}

		public void Terminate(ICPTerminatedMessage.TerminationReason reason, string message = "")
		{
			hasTerminated = true;
			if (FinishedCallBack != null) FinishedCallBack();
			SendMessageToAllListeners(
				methodName: "OnICPTerminated",
				message: new ICPTerminatedMessage(reason, this.Error, this.iterationCounter.CurrentCount, message, ModelFragment.name, this.errorsString)
			);
		}

		/// <summary>
		/// Selects the points to use for the computation of the correspondences from the gameobejct with the selector specified in the settings object. Notify the fragment of which the points are selected.
		/// </summary>
		/// <returns>The points.</returns>
		/// <param name="fragment">Fragment.</param>
		private List<Point> SamplePoints(GameObject fragment)
		{
			List<Point> points = Settings.PointSampler.Sample(new SamplingInformation(fragment));
			return points;
		}

		private CorrespondenceCollection ComputeCorrespondences(List<Point> staticPoints)
		{
			Mesh modelMesh = modelFragment.GetComponent<MeshFilter>().mesh;
			CorrespondenceCollection correspondences = Settings.CorrespondenceFinder.Find(
				staticPoints.AsReadOnly(), ModelSamplingInformation
			);
			return correspondences;
		}

		private CorrespondenceCollection FilterCorrespondences(CorrespondenceCollection correspondences)
		{
			foreach (ICorrespondenceFilter filter in Settings.CorrespondenceFilters)
			{
				correspondences = filter.Filter(correspondences);
			}
			return correspondences;
		}

		private void TransformModelFragment(Matrix4x4 transform)
		{
			Fragment.TransformController transformcontroller = ModelFragment.GetComponent<Fragment.TransformController>();
			transformcontroller.TransformFragment(transform, Settings.ReferenceTransform);
		}

		private void SendMessageToAllListeners(string methodName, System.Object message = null)
		{
			foreach (GameObject listener in Listeners)
			{
				listener.SendMessage(
					methodName: methodName,
					value: message,
					options: SendMessageOptions.DontRequireReceiver
				);
			}
			if (message is Ticker.IToTickerMessage) SendMessageToTicker(message as Ticker.IToTickerMessage);
		}

		private void SendMessageToTicker(Ticker.IToTickerMessage message)
		{
			Ticker.Receiver.Instance.SendMessage("OnMessage", message.ToTickerMessage());
		}
	}

	public class StabilizationTermiationCondition
	{
		private int numPatternsToConsider;

		//If the SD is smaller than this value we terminate
		private double threshold;

		private int storedErrorsCount;
		private int idx;

		private double[] errors;

		public StabilizationTermiationCondition(int numPatternsToConsider = 50, double threshold = 5e-08)
		{
			this.numPatternsToConsider = numPatternsToConsider;
			this.threshold = threshold;

			storedErrorsCount = 0;
			errors = new double[numPatternsToConsider];
			idx = 0;
		}

		/// <summary>
		/// Returns true if the error has stabilized. I.e. if the error for the 
		/// past numPatternsToConsider is relativley constant. Stores the passed 
		/// error in the list of errors.
		/// </summary>
		/// <returns><c>true</c>, if the error has stabilized, <c>false</c> otherwise.</returns>
		/// <param name="currentError">Current error.</param>
		public bool ErrorHasStabilized()
		{
			// We have insufficient data
			if (!ErrorsArrayIsFilled()) return false;

			return CoefficientOfVariationUnderThreshold();
		}

		/// <summary>
		/// Returns true if the variantion coefficient is below the threshold.
		/// </summary>
		/// <returns><c>true</c>, if the variation coeffiecient is below the threshold <c>false</c> otherwise.</returns>
		private bool CoefficientOfVariationUnderThreshold()
		{
			double mean = errors.Average();
			double standardDeviation = ComputeErrorStandardDeviation(mean);

			//use this instead of the SD to be scale insensitive
			double coefficientOfVariation = standardDeviation / mean;

			return coefficientOfVariation < threshold;
		}

		/// <summary>
		/// Compute the biased standarddeviation of the stored errors.
		/// </summary>
		/// <returns>The error standard deviation.</returns>
		/// <param name="mean">Mean.</param>
		private double ComputeErrorStandardDeviation(double mean)
		{
			double standardDeviation = 0;
			if (errors.Count() > 0)
			{
				double sum = errors.Sum(d => Math.Pow(d - mean, 2));
				standardDeviation = Math.Sqrt((sum) / errors.Count());
			}
			return standardDeviation;
		}

		/// <summary>
		/// Returns true if numCountErrors are stored.
		/// </summary>
		/// <returns><c>true</c>, if numCountErrors are stored <c>false</c> otherwise.</returns>
		private bool ErrorsArrayIsFilled()
		{
			return storedErrorsCount >= numPatternsToConsider;
		}

		/// <summary>
		/// Adds the error to the list of errors. If the list of errors is full 
		/// the oldest error is removed.
		/// </summary>
		/// <param name="error">Error.</param>
		public void AddError(float error)
		{
			errors[idx] = (double)error;
			idx = (idx + 1) % numPatternsToConsider;
			storedErrorsCount++;
		}
	}
}
