using UnityEngine;
using System.Collections.Generic;

namespace Registration
{
    public interface IICPListener
    {
        public vond OnICPPointsSelected(List<Vector3> points);
    }
}

