using System.Collections.Generic;
using UnityEngine;

namespace Registration
{
    namespace Error
    {
        public class MeanOfDistances : AbstractErrorMetric
        {
            public MeanOfDistances(
                DistanceMetrics.Metric distanceMetric = null
            ) : base(distanceMetric) { }

            public override float ComputeError(List<Correspondence> correspondences, Transform orignalTransform, Transform newTransform)
            {
                Point newModelPoint;
                float error = 0;
                foreach (Correspondence correspondence in correspondences)
                {
                    newModelPoint = correspondence.ModelPoint.ChangeTransform(orignalTransform, newTransform);

                    error += DistanceMetric(
                        staticPoint: correspondence.StaticPoint,
                        modelPoint: newModelPoint
                    );
                }
                error /= correspondences.Count;
                return error;
            }
        }
    }
}