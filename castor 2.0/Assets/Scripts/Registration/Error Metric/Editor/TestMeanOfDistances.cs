using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

using Registration;
using Registration.Error;

namespace Tests
{
    [TestFixture]
    public class MeanOfDistancesTest
    {
        [Test]
        public void TestComputeError_NoConstructorNeutralTransform()
        {
            List<Correspondence> correspondences = new List<Correspondence>
        {
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
        };

            float expected = 26.203333333333333f;
            float actual = new MeanOfDistances().ComputeError(correspondences, null, null);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestComputeError_WithConstructorNeutralTransform()
        {
            List<Correspondence> correspondences = new List<Correspondence>
        {
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
        };

            float expected = 26.203333333333333f;
            float actual =
                new MeanOfDistances(
                    DistanceMetrics.SquaredEuclidean
                ).ComputeError(
                    correspondences, null, null
                );
            Assert.AreEqual(expected, actual);
        }
    }
}