using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Registration
{
    public interface IErrorMetric
    {
        float ComputeError(List<Correspondence> correspondences);
    }
}
