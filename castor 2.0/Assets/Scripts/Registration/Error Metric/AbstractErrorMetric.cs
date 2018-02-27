using System.Collections.Generic;
using UnityEngine;

namespace Registration
{
    namespace Error
    {
        public abstract class AbstractErrorMetric
        {
            protected readonly DistanceMetrics.Metric DistanceMetric;

            protected AbstractErrorMetric(DistanceMetrics.Metric distanceMetric = null)
            {
                DistanceMetric = distanceMetric ?? DistanceMetrics.SquaredEuclidean;
            }

            /// <summary>
            /// Computes the error of the correspondences, given that the passed transform is applied to the model points.
            /// </summary>
            /// <returns>The error if the modelTransform is applied to the model points of the correspondences.</returns>
            /// <param name="correspondences">the correspondences</param>
            /// <param name="modelTransform">Model transform to be applied to the model points of the correspondenes.</param>
            public abstract float ComputeError(List<Correspondence> correspondences, Transform modelTransform);
        }
    }
}
