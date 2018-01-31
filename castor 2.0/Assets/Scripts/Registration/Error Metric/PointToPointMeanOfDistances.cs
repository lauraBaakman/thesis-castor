using UnityEngine;
using System.Collections.Generic;

namespace Registration
{
    public class PointToPointMeanOfDistances : PointToPointErrorMetric
    {
        public PointToPointMeanOfDistances(
            PointToPointDistanceMetrics.DistanceMetric distanceMetric = null
        ) : base(distanceMetric) { }

        public override float ComputeError(List<Correspondence> correspondences)
        {
            float error = 0;
            foreach (Correspondence correspondence in correspondences)
            {
                error += DistanceMetric(correspondence.StaticPoint, correspondence.ModelPoint);
            }
            error /= correspondences.Count;
            return error;
        }
    }
}