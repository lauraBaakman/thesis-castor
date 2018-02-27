using System.Collections.Generic;
using UnityEngine;

namespace Registration
{
    namespace Error
    {
        public class PointToPointMeanOfDistances : AbstractErrorMetric
        {
            public PointToPointMeanOfDistances(
                DistanceMetrics.Metric distanceMetric = null
            ) : base(distanceMetric) { }

            public override float ComputeError(List<Correspondence> correspondences, Transform orignalTransform, Transform newTransform)
            {
                float error = 0;
                foreach (Correspondence correspondence in correspondences)
                {
                    error += DistanceMetric(correspondence.StaticPoint.Position, correspondence.ModelPoint.Position);
                }
                error /= correspondences.Count;
                return error;
            }
        }
    }
}