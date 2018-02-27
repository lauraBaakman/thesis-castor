using System.Collections.Generic;
using UnityEngine;

namespace Registration
{
    namespace Error
    {
        public class PointToPointSumOfDistances : AbstractErrorMetric
        {
            public PointToPointSumOfDistances(
                PointToPointDistanceMetrics.DistanceMetric distanceMetric = null
            ) : base(distanceMetric) { }

            public override float ComputeError(List<Correspondence> correspondences, Transform modelTransform)
            {
                float sumOfErrors = 0;
                foreach (Correspondence correspondence in correspondences)
                {
                    sumOfErrors += DistanceMetric(correspondence.StaticPoint.Position, correspondence.ModelPoint.Position);
                }
                return sumOfErrors;
            }
        }
    }
}