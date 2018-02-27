using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Registration
{
    namespace Error
    {
        public class SumOfSquaredDistances : AbstractErrorMetric
        {
            public SumOfSquaredDistances(
                DistanceMetrics.Metric distanceMetric = null
            ) : base(distanceMetric ?? DistanceMetrics.PointToPlane) { }

            public override float ComputeError(List<Correspondence> correspondences, Transform orignalTransform, Transform newTransform)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}

