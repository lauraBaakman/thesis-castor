using System.Collections.Generic;
using UnityEngine;

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

            /// <summary>
            /// Compute the error of the correspondences given the transform that should be applied to the model points.
            /// </summary>
            /// <returns>The error of the correspondences if the transform is applied to the model fragments.</returns>
            /// <param name="correspondences">The correspondences before the transform is applied.</param>
            /// <param name="transform">The transform that should be applid to the model points of the correspondences</param>
            public abstract float ComputeError(List<Correspondence> correspondences, Matrix4x4 transform);
        }
    }
}

