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

            public readonly Transform Transform;

            public readonly int IterationIndex;
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
                Error
            }

            public readonly TerminationReason Reason;
            private readonly string message;

            public ICPTerminatedMessage(TerminationReason reason, string message = "")
            {
                this.Reason = reason;
                this.message = message;
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
                    case TerminationReason.Error:
                        return message;
                }
                return "";
            }
        }

        public class ICPStepCompletedMessage : IToTickerMessage
        {
            public readonly int IterationIndx;
            public readonly float Error;

            public ICPStepCompletedMessage(int iterationIndx, float error)
            {
                this.IterationIndx = iterationIndx;
                this.Error = error;
            }

            public Message ToTickerMessage()
            {
                return new Message.InfoMessage(
                    string.Format(
                        "Finished iteration {0}, the current error is {1}",
                        IterationIndx, Error
                    )
                );
            }
        }
    }
}