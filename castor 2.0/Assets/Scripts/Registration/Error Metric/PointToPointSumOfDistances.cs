using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Registration
{
    public class PointToPointSumOfDistances : IPointToPointErrorMetric
    {
        private readonly PointToPointDistanceMetrics.DistanceMetric DistanceMetric;

        public PointToPointSumOfDistances(PointToPointDistanceMetrics.DistanceMetric distanceMetric = null)
        {
            DistanceMetric = distanceMetric ?? PointToPointDistanceMetrics.SquaredEuclidean;
        }

        public float ComputeError(List<Correspondence> correspondences)
        {
            float sumOfErrors = 0;
            foreach (Correspondence correspondence in correspondences)
            {
                sumOfErrors += DistanceMetric(correspondence.StaticPoint, correspondence.ModelPoint);
            }
            return sumOfErrors;
        }
    }
}