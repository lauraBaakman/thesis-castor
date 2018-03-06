using System.Collections.Generic;
using UnityEngine;

namespace Registration
{
    namespace Error
    {
        public class ErrorMetric
        {
            protected readonly Configuration configuration;

            public ErrorMetric(Configuration configuration)
            {
                this.configuration = configuration;
            }

            /// <summary>
            /// Computes the error of the correspondences, given that the passed transform is applied to the model points.
            /// </summary>
            /// <returns>The error if the modelTransform is applied to the model points of the correspondences.</returns>
            /// <param name="correspondences">the correspondences</param>
            /// <param name="newTransform">the new transform of the correspondences.</param>
            /// <param name="orignalTransform">the original transform of the correspondences.</param>
            public float ComputeError(CorrespondenceCollection correspondences, Transform orignalTransform, Transform newTransform)
            {
                List<float> errors = new _ErrorsComputer(configuration, orignalTransform, newTransform).ComputeErrors(correspondences);
                return configuration.AggregationMethod(errors);
            }

            #region inner classes
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

                /// <summary>
                /// The method used to combine the errors of the indivual 
                /// correspondences into one error.
                /// </summary>
                public AggregationMethods.AggregationMethod AggregationMethod;

                public Configuration(
                    DistanceMetrics.Metric distanceMetric = null,
                    bool normalizePoints = true,
                    AggregationMethods.AggregationMethod aggregationMethod = null
                )
                {
                    DistanceMetric = distanceMetric ?? DistanceMetrics.SquaredEuclidean;
                    NormalizePoints = normalizePoints;
                    AggregationMethod = aggregationMethod ?? AggregationMethods.Sum;
                }
            }

            private class _ErrorsComputer
            {
                private Transform originalTransform;
                private Transform newTransform;
                private DistanceMetrics.Metric distanceMetric;

                public _ErrorsComputer(Configuration configuration, Transform originalTransform, Transform newTransform)
                {
                    this.originalTransform = originalTransform;
                    this.newTransform = newTransform;
                    this.distanceMetric = configuration.DistanceMetric;
                }

                public List<float> ComputeErrors(CorrespondenceCollection correspondences)
                {
                    List<float> errors = new List<float>(correspondences.Count);
                    foreach (Correspondence corresponence in correspondences)
                    {
                        errors.Add(ComputeError(corresponence));
                    }
                    return errors;
                }

                private float ComputeError(Correspondence correspondence)
                {
                    Point transformedModelPoint = correspondence.ModelPoint.ChangeTransform(originalTransform, newTransform);

                    return distanceMetric(
                        staticPoint: correspondence.StaticPoint,
                        modelPoint: transformedModelPoint
                    );
                }
            }
            #endregion
        }
    }
}

