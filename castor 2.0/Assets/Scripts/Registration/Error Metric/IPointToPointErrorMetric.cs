using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Registration
{
    internal interface IPointToPointErrorMetric
    {
        float ComputeError(List<Correspondence> correspondences);
    }
}
