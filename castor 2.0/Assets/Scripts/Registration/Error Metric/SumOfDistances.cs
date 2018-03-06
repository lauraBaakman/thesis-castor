using System.Collections.Generic;
using UnityEngine;

namespace Registration
{
    namespace Error
    {
        public class SumOfDistances : AbstractErrorMetric
        {
            public SumOfDistances(Configuration configuration)
                : base(configuration) { }

            public override float ComputeError(CorrespondenceCollection correspondences, Transform orignalTransform, Transform newTransform)
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