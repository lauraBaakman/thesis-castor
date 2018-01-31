using UnityEngine;
using System.Collections.Generic;

namespace Registration
{
    public class PointToPointMeanOfDistances : IPointToPointErrorMetric
    {
        private readonly PointToPointDistanceMetrics.DistanceMetric DistanceMetric;

        public PointToPointMeanOfDistances(PointToPointDistanceMetrics.DistanceMetric distanceMetric = null)
        {
            DistanceMetric = distanceMetric ?? PointToPointDistanceMetrics.SquaredEuclidean;
        }

        public float ComputeError(List<Correspondence> correspondences)
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