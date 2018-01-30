using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Registration
{
    public class Message
    {
        private readonly Transform transform;

        protected Message(Transform transform)
        {
            this.transform = transform;
        }

        public Transform Transform
        {
            get
            {
                return transform;
            }
        }

    }

    public class ICPPointsSelectedMessage : Message
    {
        private readonly List<Vector3> points;

        public ICPPointsSelectedMessage(List<Vector3> points, Transform transform) : base(transform)
        {
            this.points = points;
        }

        public List<Vector3> Points
        {
            get
            {
                return points;
            }
        }
    }

    public class ICPCorrespondencesChanged : Message
    {
        private readonly List<Correspondence> correspondences;

        public ICPCorrespondencesChanged(List<Correspondence> correspondences, Transform transform) : base(transform)
        {
            this.correspondences = correspondences;
        }

        public List<Correspondence> Correspondences
        {
            get
            {
                return correspondences;
            }
        }
    }
}