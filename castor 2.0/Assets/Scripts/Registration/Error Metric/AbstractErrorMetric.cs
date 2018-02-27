using System.Collections.Generic;

namespace Registration
{
    namespace Error
    {
        public abstract class AbstractErrorMetric
        {
            protected readonly PointToPointDistanceMetrics.DistanceMetric DistanceMetric;

            protected AbstractErrorMetric(PointToPointDistanceMetrics.DistanceMetric distanceMetric = null)
            {
                DistanceMetric = distanceMetric ?? PointToPointDistanceMetrics.SquaredEuclidean;
            }

            public abstract float ComputeError(List<Correspondence> correspondences);
        }
    }
}

