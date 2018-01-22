using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Registration {
    public interface IICPListener {
        void OnICPPointsSelected( ICPPointsSelectedMessage message );

        IEnumerator OnICPFinished();
    }

    public class ICPPointsSelectedMessage {
        private readonly List<Vector3> points;

        public ICPPointsSelectedMessage( List<Vector3> points )
        {
            this.points = points;
        }

        public List<Vector3> Points {
            get {
                return points;
            }
        }
    }
}

