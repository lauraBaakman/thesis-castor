using System.Collections.Generic;
using UnityEngine;

namespace Registration
{
    namespace Error
    {
        public abstract class AbstractErrorMetric
        {
            protected readonly DistanceMetrics.Metric DistanceMetric;

            protected AbstractErrorMetric(Configuration configuration)
            {
                this.DistanceMetric = configuration.DistanceMetric;
            }

            /// <summary>
            /// Computes the error of the correspondences, given that the passed transform is applied to the model points.
            /// </summary>
            /// <returns>The error if the modelTransform is applied to the model points of the correspondences.</returns>
            /// <param name="correspondences">the correspondences</param>
            /// <param name="newTransform">the new transform of the correspondences.</param>
            /// <param name="orignalTransform">the original transform of the correspondences.</param>
            public abstract float ComputeError(CorrespondenceCollection correspondences, Transform orignalTransform, Transform newTransform);


            public class Configuration
            {
                public readonly DistanceMetrics.Metric DistanceMetric;

                public Configuration(DistanceMetrics.Metric distanceMetric = null)
                {
                    DistanceMetric = distanceMetric ?? DistanceMetrics.SquaredEuclidean;
                }
            }
        }
    }
}

