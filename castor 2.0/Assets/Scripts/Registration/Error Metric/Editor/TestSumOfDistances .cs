using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

using Registration;
using Registration.Error;

namespace Tests
{
    [TestFixture]
    public class SumOfDistancesTest
    {
        ErrorMetric.Configuration configuration;

        [SetUp]
        public void Init()
        {
            configuration = new ErrorMetric.Configuration(
                distanceMetric: DistanceMetrics.SquaredEuclidean,
                normalizePoints: false
            );
        }

        [Test]
        public void TestComputeError_WithNeutralTransform()
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
            float actual =
                new SumOfDistances(configuration).ComputeError(
                    correspondences, null, null
                );
            Assert.AreEqual(expected, actual);
        }
    }

}