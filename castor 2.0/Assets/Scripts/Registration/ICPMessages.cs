using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ticker;

namespace Registration
{
	namespace Messages
	{
		public class ICPPreparationStepCompletedMessage
		{
			static int firstIterationIdx = 1;

			/// <summary>
			/// The transform of the corespondences
			/// </summary>
			public readonly Transform Transform;

			/// <summary>
			/// The iteration of the ICP algorithm for which the preparation 
			/// step was completed.
			/// </summary>
			public readonly int IterationIndex;

			/// <summary>
			/// The correspondences determined in the preparation step.
			/// </summary>
			public readonly CorrespondenceCollection Correspondences;

			public ICPPreparationStepCompletedMessage(CorrespondenceCollection correspondences, Transform transform, int iterationIndex)
			{
				this.Correspondences = correspondences;
				this.Transform = transform;

				this.IterationIndex = iterationIndex;
			}

			public bool IsFirstPreparationStep()
			{
				return this.IterationIndex == firstIterationIdx;
			}
		}

		public class ICPTerminatedMessage : IToTickerMessage
		{
			public enum TerminationReason
			{
				UserTerminated,
				ExceededNumberOfIterations,
				ErrorBelowThreshold,
				ErrorStabilized,
				Error
			}

			public readonly TerminationReason Reason;

			private readonly string message;
			public string Message { get { return message; } }

			public float errorAtTermination;

			public readonly int terminationIteration;

			public ICPTerminatedMessage(TerminationReason reason, float currentError, int terminationIteration, string message)
			{
				this.Reason = reason;
				this.message = (message == "" ? ReasonToString() : message);
				this.errorAtTermination = currentError;
				this.terminationIteration = terminationIteration;
			}

			public Message ToTickerMessage()
			{
				return new Message.InfoMessage("Terminated ICP: " + ReasonToString());
			}

			private string ReasonToString()
			{
				switch (Reason)
				{
					case TerminationReason.UserTerminated:
						return "The user terminated the registration process.";
					case TerminationReason.ExceededNumberOfIterations:
						return "The number of iterations exceed the maximum number of iterations";
					case TerminationReason.ErrorBelowThreshold:
						return "The error of the current iteration was below the threshold.";
					case TerminationReason.ErrorStabilized:
						return "The error of the process had stabilized";
					case TerminationReason.Error:
						return message;
				}
				return "";
			}
		}

		public class ICPStepCompletedMessage : IToTickerMessage
		{
			public readonly int iteration;
			public readonly float Error;

			public ICPStepCompletedMessage(int iteration, float error)
			{
				this.iteration = iteration;
				this.Error = error;
			}

			public Message ToTickerMessage()
			{
				return new Message.InfoMessage(
					string.Format(
						"Finished iteration {0}, the current error is {1}",
						iteration, Error
					)
				);
			}
		}
	}
}