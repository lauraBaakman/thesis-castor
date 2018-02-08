using UnityEngine;
using System.Collections.Generic;

namespace Registration
{
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