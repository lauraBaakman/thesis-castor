using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Registration.Error;
using System.IO;
using Utils;
using System;

namespace Registration
{
	public class Settings
	{
		/// <summary>
		/// The transform in which the registration is performed. 
		/// </summary>
		/// <value>The reference transform.</value>
		public Transform ReferenceTransform { get; set; }

		/// <summary>
		/// If the error of the current registration is smaller than the initial 
		/// error scaled with this value the algorithm terminates.
		/// </summary>
		/// <value>The error threshold.</value>
		public float ErrorThresholdScale { get; set; }

		/// <summary>
		/// The maximum number of iterations to execute.
		/// </summary>
		/// <value>The max number iterations.</value>
		public int MaxNumIterations { get; set; }

		public float MaxWithinCorrespondenceDistance { get; set; }

		/// <summary>
		/// The method used to select points from a mesh, that can be used in a 
		/// correspondence.
		/// </summary>
		/// <value>The selector.</value>
		public IPointSampler PointSampler { get; set; }

		/// <summary>
		/// The method used to find correspondecs within the points selected by
		/// the Selector.
		/// </summary>
		/// <value>The correspondence finder.</value>
		public ICorrespondenceFinder CorrespondenceFinder { get; set; }

		public readonly string name;

		/// <summary>
		/// The filters used to filter the correspondences.
		/// </summary>
		private List<ICorrespondenceFilter> correspondenceFilters;
		public ReadOnlyCollection<ICorrespondenceFilter> CorrespondenceFilters
		{
			get { return correspondenceFilters.AsReadOnly(); }
		}

		/// <summary>
		/// The method used to compute the distances between points.
		/// </summary>
		/// <value>The point to point distance metric.</value>
		public DistanceMetrics.Metric DistanceMetric { get; set; }

		/// <summary>
		/// The error metric used to compute the error of a registration.
		/// </summary>
		/// <value>The error metric.</value>
		public IErrorMetric ErrorMetric
		{
			get { return TransFormFinder.GetErrorMetric(); }
		}

		/// <summary>
		/// The method used to find the transform between the static points and the model points.
		/// </summary>
		/// <value>The trans form finder.</value>
		public ITransformFinder TransFormFinder { get; set; }

		public Settings(Transform referenceTransform)
			: this(referenceTransform, new HornTransformFinder(), "defaultHornTransformFinder")
		{ }

		public Settings(
			Transform referenceTransform,
			ITransformFinder transformFinder,
			string name,
			string correspondenceFinder = "normalshooting",
			string pointSampler = "allpoints",
			float errorThresholdScale = 0.0001f, int maxNumIterations = 500,
			float maxWithinCorrespondenceDistance = 8f
		)
		{
			this.name = name;

			ReferenceTransform = referenceTransform;

			ErrorThresholdScale = errorThresholdScale;

			MaxWithinCorrespondenceDistance = maxWithinCorrespondenceDistance;

			MaxNumIterations = maxNumIterations;

			correspondenceFilters = new List<ICorrespondenceFilter>();

			this.TransFormFinder = transformFinder;

			if (correspondenceFinder == "normalshooting")
			{
				CorrespondenceFinder = new NormalShootingCorrespondenceFinder(this);
			}
			else if (correspondenceFinder == "nearestneighbour")
			{
				CorrespondenceFinder = new NearstPointCorrespondenceFinder(PointSampler);
			}
			else
			{
				throw new Exception("Invalid Correspondence Finder name");
			}

			if (pointSampler == "allpoints")
			{
				PointSampler = new AllPointsSampler(
					new AllPointsSampler.Configuration(
						referenceTransform,
						AllPointsSampler.Configuration.NormalProcessing.VertexNormals
					)
				);
			}
			else if (pointSampler == "ndosubsampling")
			{
				PointSampler = new NDOSubsampling(
					new NDOSubsampling.Configuration(
						referenceTransform: referenceTransform,
						percentage: 50,
						binCount: 12
					)
				);
			}
			else
			{
				throw new Exception("Invalid Point Sampler name");
			}
		}

		public static Settings SeminalICP(Transform referenceTransform,
										  string sampler = "allpoints")
		{
			return new Settings(
				referenceTransform: referenceTransform,
				transformFinder: new HornTransformFinder(),
				name: "Seminal ICP",
				correspondenceFinder: "nearestneighbour",
				pointSampler: sampler
			);
		}

		public static Settings Horn(Transform referenceTransform,
									string sampler = "allpoints")
		{
			return new Settings(
				name: "Horn",
				referenceTransform: referenceTransform,
				transformFinder: new HornTransformFinder(),
				pointSampler: sampler
			);
		}

		public static Settings IntersectionError(Transform referenceTransform,
												 string sampler = "allpoints")
		{
			return new Settings(
				name: "igdIntersectionTermError",
				referenceTransform: referenceTransform,
				transformFinder: new IGDTransformFinder(
					new IGDTransformFinder.Configuration(
						convergenceError: 0.001f,
						learningRate: 0.001f,
						maxNumIterations: 200,
						errorMetric: new Registration.Error.IntersectionTermError(0.5f, 0.5f)
					)
				),
				pointSampler: sampler
			);
		}

		public static Settings Wheeler(Transform referecenTransform,
									   string sampler = "allpoints")
		{
			return new Settings(
				name: "igdWheelerError",
				referenceTransform: referecenTransform,
				transformFinder: new IGDTransformFinder(
					new IGDTransformFinder.Configuration(
						convergenceError: 0.001f,
						learningRate: 0.001f,
						maxNumIterations: 200,
						errorMetric: new Registration.Error.WheelerIterativeError()
					)
				),
				pointSampler: sampler
			);
		}

		public static Settings Low(Transform referenceTransform,
								   string sampler = "allpoints")
		{
			return new Settings(
				name: "low",
				referenceTransform: referenceTransform,
				transformFinder: new LowTransformFinder(),
				pointSampler: sampler
			);
		}

		public static Settings SettingsFromName(string name, Transform referenceTransform, string sampler)
		{
			if (name.ToLower().Equals("horn")) return Horn(referenceTransform, sampler);
			if (name.ToLower().Equals("low")) return Low(referenceTransform, sampler);
			if (name.ToLower().Equals("wheeler")) return Wheeler(referenceTransform, sampler);
			if (name.ToLower().Equals("intersection")) return IntersectionError(referenceTransform, sampler);

			throw new Exception("The name " + name + " does not represent a valid settings object");
		}

		public void ToJson(string outputPath)
		{
			new SerializableSettings(this).ToJson(outputPath);
		}

		public string ToJson()
		{
			return new SerializableSettings(this).ToJson();
		}

		[System.Serializable]
		public class SerializableSettings
		{
			[System.Serializable]
			public class SerializableCorrespondences
			{
				public List<string> correspondenceFilters;
				public float maxWithinCorrespondenceDistance;
				public SerializablePointSampler pointSampler;
				public SerializebleCorrespondenceFinder correspondenceFinder;

				private SerializableCorrespondences(
					ReadOnlyCollection<ICorrespondenceFilter> correspondenceFilters,
					float maxWithinCorrespondenceDistance,
					IPointSampler pointSampler,
					ICorrespondenceFinder correspondenceFinder
				)
				{
					foreach (ICorrespondenceFilter filter in correspondenceFilters)
					{
						this.correspondenceFilters.Add(filter.ToJson());
					}
					this.maxWithinCorrespondenceDistance = maxWithinCorrespondenceDistance;

					this.pointSampler = pointSampler.ToSerializableObject();
					this.correspondenceFinder = correspondenceFinder.Serialize();
				}

				public SerializableCorrespondences(Settings settings)
					: this(
						settings.CorrespondenceFilters,
						settings.MaxWithinCorrespondenceDistance,
						settings.PointSampler,
						settings.CorrespondenceFinder
					)
				{ }
			}

			[System.Serializable]
			public class SerializableError
			{
				public float errorThresholdScale;
				public SerializableErrorMetric errorMetric;

				private SerializableError(float errorThresholdScale, SerializableErrorMetric errorMetric)
				{
					this.errorThresholdScale = errorThresholdScale;
					this.errorMetric = errorMetric;
				}

				public SerializableError(Settings settings)
					: this(settings.ErrorThresholdScale, settings.ErrorMetric.Serialize())
				{ }
			}

			public SerializableTransform referenceTransform;

			public SerializableCorrespondences correspondences;

			public SerializableError error;

			public SerializableTransformFinder transformFinder;

			public SerializablePointSampler sampler;

			public int maxNumIterations;

			public SerializableSettings(Settings settings)
			{
				this.maxNumIterations = settings.MaxNumIterations;

				referenceTransform = new SerializableTransform(settings.ReferenceTransform);

				correspondences = new SerializableCorrespondences(settings);

				error = new SerializableError(settings);

				sampler = settings.PointSampler.ToSerializableObject();

				transformFinder = settings.TransFormFinder.Serialize();
			}

			public void ToJson(string outputPath)
			{
				string jsonString = JsonUtility.ToJson(this);

				StreamWriter streamWriter = new StreamWriter(outputPath);
				streamWriter.Write(jsonString);
				streamWriter.Close();
			}

			public string ToJson()
			{
				return JsonUtility.ToJson(this);
			}
		}
	}

}

