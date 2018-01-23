using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Registration {
    public interface IICPListener {
        void OnICPPointsSelected( ICPPointsSelectedMessage message );

        void OnICPCorrespondencesDetermined(ICPCorrespondencesDeterminedMessage message);

        IEnumerator OnICPFinished();
    }

    public class ICPPointsSelectedMessage {
        private readonly List<Vector3> points;
        private readonly Transform pointsTransform;

        public ICPPointsSelectedMessage( List<Vector3> points, Transform pointsTransform )
        {
            this.points = points;
            this.pointsTransform = pointsTransform;
        }

        public List<Vector3> Points {
            get {
                return points;
            }
        }

        public Transform PointsTransform {
            get {
                return pointsTransform;
            }
        }
    }

    public class ICPCorrespondencesDeterminedMessage {
        private readonly List<Correspondence> correspondences;
        private readonly Transform transform;

        public ICPCorrespondencesDeterminedMessage(List<Correspondence> correspondences, Transform transform){
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

