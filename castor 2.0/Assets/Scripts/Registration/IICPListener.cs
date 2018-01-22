using UnityEngine;
using System.Collections.Generic;

namespace Registration
{
    public interface IICPListener
    {
        void OnICPPointsSelected(List<Vector3> points);
    }
}

