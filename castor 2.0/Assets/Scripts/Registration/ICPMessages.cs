using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Registration
{
    public class ICPPreparationStepCompletedMessage
    {
        public readonly Transform Transform;

        public readonly ReadOnlyCollection<Correspondence> Correspondences;
        public readonly ReadOnlyCollection<Point> ModelPoints;
        public readonly ReadOnlyCollection<Point> StaticPoints;

        public ICPPreparationStepCompletedMessage(List<Correspondence> correspondences, Transform transform)
        {
            this.Correspondences = correspondences.AsReadOnly();
            this.Transform = transform;

            List<Point> modelPoints = new List<Point>(correspondences.Count);
            List<Point> staticPoints = new List<Point>(correspondences.Count);

            ExtractPoints(correspondences, ref modelPoints, ref staticPoints);

            this.ModelPoints = modelPoints.AsReadOnly();
            this.StaticPoints = staticPoints.AsReadOnly();
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

    public class ICPPointsSelectedMessage
    {
        public Transform Transform
        {
            get { return transform; }
        }
        private readonly Transform transform;

        public List<Point> Points
        {
            get { return points; }
        }
        private readonly List<Point> points;

        public ICPPointsSelectedMessage(List<Point> points, Transform transform)
        {
            this.transform = transform;
            this.points = points;
        }
    }

    public class ICPCorrespondencesChanged
    {
        public List<Correspondence> Correspondences
        {
            get { return correspondences; }
        }
        private readonly List<Correspondence> correspondences;

        public Transform Transform
        {
            get { return transform; }
        }
        private readonly Transform transform;

        public ICPCorrespondencesChanged(List<Correspondence> correspondences, Transform transform)
        {
            this.correspondences = correspondences;
            this.transform = transform;
        }
    }

    public class ICPTerminatedMessage
    {
        public enum TerminationReason { UserTerminated, ExceededNumberOfIterations, ErrorBelowThreshold }

        private TerminationReason reason;
        public TerminationReason Reason
        {
            get { return reason; }
        }

        public ICPTerminatedMessage(TerminationReason reason)
        {
            this.reason = reason;
        }
    }
}