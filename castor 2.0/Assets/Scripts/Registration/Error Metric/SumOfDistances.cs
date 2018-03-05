using System.Collections.Generic;
using UnityEngine;

namespace Registration
{
    namespace Error
    {
        public class SumOfDistances : AbstractErrorMetric
        {
            public SumOfDistances(
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
                        staticPoint: correspondence.StaticPoint,
                        modelPoint: newModelPoint
                    );
                }
                return sumOfErrors;
            }
        }
    }
}