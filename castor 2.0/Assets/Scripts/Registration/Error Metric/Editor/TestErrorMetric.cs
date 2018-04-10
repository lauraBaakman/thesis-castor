using System.Collections.Generic;
using NUnit.Framework;
using Registration;
using Registration.Error;
using UnityEngine;

namespace Tests.Registration.Error
{
    [TestFixture]
    public class ErrorMetricTests
    {
        private static double tolerance = 0.0001;
        private ErrorMetric errorMetric;

        [SetUp]
        public void Init()
        {
            ErrorMetric.Configuration configuration = new ErrorMetric.Configuration(
                distanceMetric: DistanceMetrics.SquaredEuclidean,
                aggregationMethod: AggregationMethods.Sum,
                normalizePoints: false
            );

            errorMetric = new ErrorMetric(configuration);
        }

        [Test]
        public void Test_ComputeError_NormalCorrespondences()
        {
            CorrespondenceCollection correspondences = new CorrespondenceCollection(
                new List<Correspondence>{
                    new Correspondence(
                        new Point(new Vector3(1.0f, 2.0f, 3.0f)),
                        new Point(new Vector3(2.0f, 3.0f, 4.0f))
                    ),
                    new Correspondence(
                        new Point(new Vector3(3.4f, 4.5f, 5.6f)),
                        new Point(new Vector3(6.7f, 7.8f, 8.9f))
                    ),
                    new Correspondence(
                        new Point(new Vector3(9.1f, 2.3f, 3.4f)),
                        new Point(new Vector3(4.5f, 5.6f, 6.7f))
                    )
                        }
                    );

                    float expected = 78.610000f;
                    float actual = errorMetric.ComputeError(correspondences, null, null);
                    Assert.That(actual, Is.EqualTo(expected).Within(tolerance));
                }

                [Test]
                public void Test_ComputeError_EmptyCorrespondences()
                {
                    CorrespondenceCollection correspondences = new CorrespondenceCollection();

                    float expected = 0.0f;
                    float actual = errorMetric.ComputeError(correspondences, null, null);

                    Assert.That(actual, Is.EqualTo(expected).Within(tolerance));
                }


                [Test]
                public void Test_ComputeError_WithNormalization()
                {
                    CorrespondenceCollection correspondences = new CorrespondenceCollection();
                    correspondences.Add(new Correspondence(
                        staticPoint: new Point(new Vector3(1, 2, 3)),
                        modelPoint: new Point(new Vector3(2, 4, 4))
                    ));
                    correspondences.Add(new Correspondence(
                        staticPoint: new Point(new Vector3(2, 3, 4)),
                        modelPoint: new Point(new Vector3(4, 4, 3))
                    ));

                    errorMetric = new ErrorMetric(
                        new ErrorMetric.Configuration(
                            distanceMetric: DistanceMetrics.SquaredEuclidean,
                            aggregationMethod: AggregationMethods.Sum,
                            normalizePoints: true
                        )
                    );

                    float expected = 1.3333333333f;
                    float actual = errorMetric.ComputeError(correspondences, null, null);

                    Assert.That(actual, Is.EqualTo(expected).Within(tolerance));
                }

                [Test]
                public void Test_Equals_Equal()
                {
                    ErrorMetric thisMetric = new ErrorMetric(
                        new ErrorMetric.Configuration(
                            distanceMetric: DistanceMetrics.PointToPlane,
                            normalizePoints: true,
                            aggregationMethod: AggregationMethods.Sum
                        )
                    );
                    ErrorMetric otherMetric = new ErrorMetric(
                        new ErrorMetric.Configuration(
                            distanceMetric: DistanceMetrics.PointToPlane,
                            normalizePoints: true,
                            aggregationMethod: AggregationMethods.Sum
                        )
                    );

                    Assert.IsTrue(thisMetric.Equals(otherMetric));
                    Assert.IsTrue(otherMetric.Equals(thisMetric));
                    Assert.AreEqual(thisMetric.GetHashCode(), otherMetric.GetHashCode());
                }

                [Test]
                public void Test_Equals_NotEqual()
                {
                    ErrorMetric thisMetric = new ErrorMetric(
                        new ErrorMetric.Configuration(
                            distanceMetric: DistanceMetrics.PointToPlane,
                            normalizePoints: true,
                            aggregationMethod: AggregationMethods.Sum
                        )
                    );
                    ErrorMetric otherMetric = new ErrorMetric(
                        new ErrorMetric.Configuration(
                            distanceMetric: DistanceMetrics.PointToPlane,
                            normalizePoints: false,
                            aggregationMethod: AggregationMethods.Sum
                        )
                    );

                    Assert.IsFalse(thisMetric.Equals(otherMetric));
                    Assert.IsFalse(otherMetric.Equals(thisMetric));
                    Assert.AreNotEqual(thisMetric.GetHashCode(), otherMetric.GetHashCode());
                }
            }

            [TestFixture]
            public class ErrorMetricConfigurationTests
            {
                [Test]
                public void Test_Equals_Equal()
                {
                    ErrorMetric.Configuration thisConfig = new ErrorMetric.Configuration(
                        distanceMetric: DistanceMetrics.PointToPlane,
                        normalizePoints: true,
                        aggregationMethod: AggregationMethods.Sum
                    );

                    ErrorMetric.Configuration otherConfig = new ErrorMetric.Configuration(
                        distanceMetric: DistanceMetrics.PointToPlane,
                        normalizePoints: true,
                        aggregationMethod: AggregationMethods.Sum
                    );

                    Assert.IsTrue(thisConfig.Equals(otherConfig));
                    Assert.IsTrue(otherConfig.Equals(thisConfig));
                    Assert.AreEqual(thisConfig.GetHashCode(), otherConfig.GetHashCode());
                }

                [Test]
                public void Test_Equals_NotEqual_DistanceMethod()
                {
                    ErrorMetric.Configuration thisConfig = new ErrorMetric.Configuration(
                        distanceMetric: DistanceMetrics.PointToPlane,
                        normalizePoints: true,
                        aggregationMethod: AggregationMethods.Sum
                    );

                    ErrorMetric.Configuration otherConfig = new ErrorMetric.Configuration(
                        distanceMetric: DistanceMetrics.SquaredEuclidean,
                        normalizePoints: true,
                        aggregationMethod: AggregationMethods.Sum
                    );

                    Assert.IsFalse(thisConfig.Equals(otherConfig));
                    Assert.IsFalse(otherConfig.Equals(thisConfig));
                    Assert.AreNotEqual(thisConfig.GetHashCode(), otherConfig.GetHashCode());
                }

                [Test]
                public void Test_Equals_NotEqual_AggregationMethod()
                {
                    ErrorMetric.Configuration thisConfig = new ErrorMetric.Configuration(
                        distanceMetric: DistanceMetrics.PointToPlane,
                        normalizePoints: true,
                        aggregationMethod: AggregationMethods.Sum
                    );

                    ErrorMetric.Configuration otherConfig = new ErrorMetric.Configuration(
                        distanceMetric: DistanceMetrics.PointToPlane,
                        normalizePoints: true,
                        aggregationMethod: AggregationMethods.Mean
                    );
                    Assert.IsFalse(thisConfig.Equals(otherConfig));
                    Assert.IsFalse(otherConfig.Equals(thisConfig));
                    Assert.AreNotEqual(thisConfig.GetHashCode(), otherConfig.GetHashCode());
                }

                [Test]
                public void Test_Equals_NotEqual_Normalization()
                {
                    ErrorMetric.Configuration thisConfig = new ErrorMetric.Configuration(
                        distanceMetric: DistanceMetrics.PointToPlane,
                        normalizePoints: true,
                        aggregationMethod: AggregationMethods.Sum
                    );

                    ErrorMetric.Configuration otherConfig = new ErrorMetric.Configuration(
                        distanceMetric: DistanceMetrics.PointToPlane,
                        normalizePoints: false,
                        aggregationMethod: AggregationMethods.Sum
                    );
                    Assert.IsFalse(thisConfig.Equals(otherConfig));
                    Assert.IsFalse(otherConfig.Equals(thisConfig));
                    Assert.AreNotEqual(thisConfig.GetHashCode(), otherConfig.GetHashCode());
                }
            }
        }
    }
}