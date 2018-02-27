using System.Collections.Generic;

namespace Registration
{
    namespace Error
    {
        public class PointToPointMeanOfDistances : AbstractErrorMetric
        {
            public PointToPointMeanOfDistances(
                PointToPointDistanceMetrics.DistanceMetric distanceMetric = null
            ) : base(distanceMetric) { }

            public override float ComputeError(List<Correspondence> correspondences)
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