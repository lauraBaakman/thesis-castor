using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using Utils;

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
            /// <param name="originalTransform">the original transform of the correspondences.</param>
            public float ComputeError(CorrespondenceCollection correspondences, Transform originalTransform, Transform newTransform)
            {
                //Apply the newTransform to the model points
                List<Point> modelPoints = TransformPoints(correspondences.ModelPoints, originalTransform, newTransform);

                //Normalize the points if required
                Matrix4x4 normalizationMatrix = Matrix4x4.identity;
                if (configuration.NormalizePoints) normalizationMatrix = new PointNormalizer().ComputeNormalizationMatrix(modelPoints, correspondences.StaticPoints);

                //Compute the correspondence errors
                List<float> errors = ComputeCorrespondenceErrors(
                    normalizationMatrix,
                    modelPoints: modelPoints,
                    staticPoints: correspondences.StaticPoints
                );

                //Aggregate the errors
                return configuration.AggregationMethod(errors);
            }

            private List<Point> TransformPoints(List<Point> points, Transform orignalTransform, Transform newTransform)
            {
                List<Point> transformedPoints = new List<Point>(points.Count);
                foreach (Point point in points)
                {
                    transformedPoints.Add(
                        point.ChangeTransform(
                            sourceTransform: orignalTransform,
                            destinationTransform: newTransform
                        )
                    );
                }
                return transformedPoints;
            }

            private List<float> ComputeCorrespondenceErrors(Matrix4x4 normalizationMatrix, List<Point> modelPoints, List<Point> staticPoints)
            {
                List<float> errors = new List<float>(modelPoints.Count);

                for (int i = 0; i < modelPoints.Count; i++)
                {
                    errors.Add(
                        ComputeCorrespondenceError(
                            normalizationMatrix,
                            modelPoint: modelPoints[i],
                            staticPoint: staticPoints[i]
                        )
                    );
                }
                return errors;
            }

            private float ComputeCorrespondenceError(Matrix4x4 normalizationMatrix, Point modelPoint, Point staticPoint)
            {
                return configuration.DistanceMetric(
                    staticPoint: staticPoint.ApplyTransform(normalizationMatrix),
                    modelPoint: modelPoint.ApplyTransform(normalizationMatrix)
                );
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
            #endregion
        }
    }
}