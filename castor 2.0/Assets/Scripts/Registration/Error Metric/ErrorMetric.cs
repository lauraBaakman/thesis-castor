using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Registration
{
    namespace Error
    {
        public class ErrorMetric : IEquatable<ErrorMetric>
        {
            #region the class
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
                float error = configuration.AggregationMethod(errors);
                return error;
            }

            private List<Point> TransformPoints(List<Point> points, Transform orignalTransform, Transform newTransform)
            {
                List<Point> transformedPoints = new List<Point>(points.Count);
                Matrix4x4 transformation = orignalTransform.LocalToOther(newTransform);

                foreach (Point point in points)
                {
                    transformedPoints.Add(point.ChangeTransform(transformation));
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

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                    return false;
                return this.Equals(obj as ErrorMetric);
            }

            public override int GetHashCode()
            {
                int hashCode = 67;

                hashCode *= (31 + configuration.GetHashCode());

                return hashCode;
            }

            public bool Equals(ErrorMetric other)
            {
                return this.configuration.Equals(other.configuration);
            }

            /// <summary>
            /// The error metric as minimized by the <see cref="HornTransformFinder"/> .
            /// 
            /// Horn, Berthold KP. "Closed-form solution of absolute orientation using unit quaternions." JOSA A 4.4 (1987): 629-642.
            /// </summary>
            /// <returns>The error used by the horn transform finder.</returns>
            public static ErrorMetric Horn()
            {
                return new ErrorMetric(Configuration.Horn());
            }

            /// <summary>
            /// The error metric as minimized by the <see cref="LowTransformFinder"/>
            /// 
            /// Low, Kok-Lim. "Linear least-squares optimization for 
            /// point-to-plane icp surface registration." Chapel Hill, 
            /// University of North Carolina 4 (2004).
            /// </summary>
            /// <returns>The low.</returns>
            public static ErrorMetric Low()
            {
                return new ErrorMetric(Configuration.Low());
            }
            #endregion

            #region inner classes
            public class Configuration : IEquatable<Configuration>
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

                public override bool Equals(object obj)
                {
                    if (obj == null || GetType() != obj.GetType())
                        return false;
                    return this.Equals(obj as Configuration);
                }

                public override int GetHashCode()
                {
                    int hashCode = 67;

                    hashCode *= (31 + DistanceMetric.GetHashCode());
                    hashCode *= (31 + AggregationMethod.GetHashCode());
                    hashCode *= (31 + NormalizePoints.GetHashCode());

                    return hashCode;
                }

                public bool Equals(Configuration other)
                {
                    return (
                        this.DistanceMetric.Equals(other.DistanceMetric) &&
                        this.AggregationMethod.Equals(other.AggregationMethod) &&
                        this.NormalizePoints.Equals(other.NormalizePoints)
                    );
                }

                /// <summary>
                /// The error metric as minimized by the <see cref="HornTransformFinder"/> .
                /// 
                /// Horn, Berthold KP. "Closed-form solution of absolute orientation using unit quaternions." JOSA A 4.4 (1987): 629-642.
                /// </summary>
                /// <returns>The error used by the horn transform finder.</returns>
                public static Configuration Horn()
                {
                    return new Configuration(
                        distanceMetric: DistanceMetrics.SquaredEuclidean,
                        aggregationMethod: AggregationMethods.Sum,
                        normalizePoints: true
                    );
                }

                /// <summary>
                /// The error metric as minimized by the <see cref="LowTransformFinder"/>
                /// 
                /// Low, Kok-Lim. "Linear least-squares optimization for 
                /// point-to-plane icp surface registration." Chapel Hill, 
                /// University of North Carolina 4 (2004).
                /// </summary>
                /// <returns>The low.</returns>
                public static Configuration Low()
                {
                    return new Configuration(
                        distanceMetric: DistanceMetrics.SquaredPointToPlane,
                        aggregationMethod: AggregationMethods.Sum,
                        normalizePoints: true
                    );
                }
            }
            #endregion
        }
    }
}