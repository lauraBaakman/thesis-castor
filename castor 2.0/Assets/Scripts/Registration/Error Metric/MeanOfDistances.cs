using System.Collections.Generic;
using UnityEngine;

namespace Registration
{
    namespace Error
    {
        public class MeanOfDistances : AbstractErrorMetric
        {
            public MeanOfDistances(Configuration configuration)
                : base(configuration) { }

            public override float ComputeError(CorrespondenceCollection correspondences, Transform orignalTransform, Transform newTransform)
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