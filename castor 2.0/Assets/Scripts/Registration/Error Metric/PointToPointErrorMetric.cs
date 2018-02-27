using System.Collections.Generic;

namespace Registration
{
    public abstract class PointToPointErrorMetric
    {
        protected readonly PointToPointDistanceMetrics.DistanceMetric DistanceMetric;

        protected PointToPointErrorMetric(PointToPointDistanceMetrics.DistanceMetric distanceMetric = null)
        {
            DistanceMetric = distanceMetric ?? PointToPointDistanceMetrics.SquaredEuclidean;
        }

        public abstract float ComputeError(List<Correspondence> correspondences);
    }

}

