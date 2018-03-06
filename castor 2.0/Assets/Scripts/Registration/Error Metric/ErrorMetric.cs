using UnityEngine;

namespace Registration
{
    namespace Error
    {
        public abstract class ErrorMetric
        {
            protected readonly DistanceMetrics.Metric DistanceMetric;

            protected ErrorMetric(Configuration configuration)
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
                /// <summary>
                /// The distance metric used to compute the error.
                /// </summary>
                public readonly DistanceMetrics.Metric DistanceMetric;

                /// <summary>
                /// Should the points be normalize, i.e. scaled and translated 
                /// to fit within the unit cube, before computing the error?
                /// </summary>
                public readonly bool NormalizePoints;

                public Configuration(
                    DistanceMetrics.Metric distanceMetric = null,
                    bool normalizePoints = true
                )
                {
                    DistanceMetric = distanceMetric ?? DistanceMetrics.SquaredEuclidean;
                    NormalizePoints = normalizePoints;
                }
            }
        }
    }
}

