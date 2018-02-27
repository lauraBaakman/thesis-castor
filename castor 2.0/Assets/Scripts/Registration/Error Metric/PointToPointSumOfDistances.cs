using System.Collections.Generic;

namespace Registration
{
    namespace Error
    {
        public class PointToPointSumOfDistances : AbstractErrorMetric
        {
            public PointToPointSumOfDistances(
                PointToPointDistanceMetrics.DistanceMetric distanceMetric = null
            ) : base(distanceMetric) { }

            public override float ComputeError(List<Correspondence> correspondences)
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