using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Registration
{
    public class Message {
        
    }

    public class ICPPointsSelectedMessage : Message
    {
        private readonly List<Vector3> points;
        private readonly Transform transform;

        public ICPPointsSelectedMessage(List<Vector3> points, Transform pointsTransform)
        {
            this.points = points;
            this.transform = pointsTransform;
        }

        public List<Vector3> Points
        {
            get
            {
                return points;
            }
        }

        public Transform Transform
        {
            get
            {
                return transform;
            }
        }
    }

    public class ICPCorrespondencesDeterminedMessage : Message
    {
        private readonly List<Correspondence> correspondences;
        private readonly Transform transform;

        public ICPCorrespondencesDeterminedMessage(List<Correspondence> correspondences, Transform transform)
        {
            this.correspondences = correspondences;
            this.transform = transform;
        }

        public List<Correspondence> Correspondences
        {
            get
            {
                return correspondences;
            }
        }

        public Transform Transform
        {
            get
            {
                return transform;
            }
        }
    }
}