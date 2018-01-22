using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Registration
{
    public interface IICPListener
    {
        void OnICPPointsSelected(List<Vector3> points);

        IEnumerator OnICPFinished();
    }
}

