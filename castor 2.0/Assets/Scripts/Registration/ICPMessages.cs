using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ticker;

namespace Registration
{
    namespace Messages
    {
        public readonly Transform Transform;

        public readonly int IterationIndex;
        public readonly ReadOnlyCollection<Correspondence> Correspondences;
        public readonly ReadOnlyCollection<Point> ModelPoints;
        public readonly ReadOnlyCollection<Point> StaticPoints;

        public ICPPreparationStepCompletedMessage(List<Correspondence> correspondences, Transform transform, int iterationIndex)
        {
            this.Correspondences = correspondences.AsReadOnly();
            this.Transform = transform;

            List<Point> modelPoints = new List<Point>(correspondences.Count);
            List<Point> staticPoints = new List<Point>(correspondences.Count);

            ExtractPoints(correspondences, ref modelPoints, ref staticPoints);

            this.IterationIndex = iterationIndex;

            this.ModelPoints = modelPoints.AsReadOnly();
            this.StaticPoints = staticPoints.AsReadOnly();
        }

        public ReadOnlyCollection<Point> GetPointsByType(Fragment.ICPFragmentType type)
        {
            switch (type)
            {
                case Fragment.ICPFragmentType.Model:
                    return ModelPoints;
                case Fragment.ICPFragmentType.Static:
                    return StaticPoints;
                default:
                    throw new System.ArgumentException("Invalid enum type.");
            }
        }

        private void ExtractPoints(List<Correspondence> correspondenceList, ref List<Point> modelPoints, ref List<Point> staticPoints)
        {
            foreach (Correspondence correspondence in correspondenceList)
            {
                modelPoints.Add(correspondence.ModelPoint);
                staticPoints.Add(correspondence.StaticPoint);
            }
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
}