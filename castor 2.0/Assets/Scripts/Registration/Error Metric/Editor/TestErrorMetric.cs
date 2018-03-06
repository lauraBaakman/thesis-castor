using System.Collections.Generic;
using NUnit.Framework;
using Registration;
using Registration.Error;
using UnityEngine;

namespace Tests
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

    }
}