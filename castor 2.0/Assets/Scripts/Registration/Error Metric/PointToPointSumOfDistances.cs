using System.Collections.Generic;
using UnityEngine;

namespace Registration
{
    namespace Error
    {
        public class PointToPointSumOfDistances : AbstractErrorMetric
        {
            public PointToPointSumOfDistances(
                DistanceMetrics.Metric distanceMetric = null
            ) : base(distanceMetric) { }

            public override float ComputeError(List<Correspondence> correspondences, Transform orignalTransform, Transform newTransform)
            {
                Point newModelPoint;
                float sumOfErrors = 0;
                foreach (Correspondence correspondence in correspondences)
                {
                    newModelPoint = correspondence.ModelPoint.ChangeTransform(orignalTransform, newTransform);

                    sumOfErrors += DistanceMetric(
                        staticPoint: correspondence.StaticPoint.Position,
                            modelPoint: newModelPoint.Position
                    );
                }
                return sumOfErrors;
            }
        }
    }
}