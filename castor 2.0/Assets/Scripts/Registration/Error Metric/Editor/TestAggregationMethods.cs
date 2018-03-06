using System.Collections.Generic;
using NUnit.Framework;
using Registration.Error;

namespace Tests
{
    [TestFixture]
    public class SumAggregationMethodTests
    {
        private static double tolerance = 0.0001;

        [Test]
        public void TestSum_NormalList()
        {
            List<float> errors = new List<float> { 1, 2, 3, 4, 5.5f };

            float expected = 15.5f;
            float actual = AggregationMethods.Sum(errors);

            Assert.That(actual, Is.EqualTo(expected).Within(tolerance));
        }

        [Test]
        public void TestSum_EmptyList()
        {
            List<float> errors = new List<float>();

            float expected = 0.0f;
            float actual = AggregationMethods.Sum(errors);

            Assert.That(actual, Is.EqualTo(expected).Within(tolerance));
        }
    }

    [TestFixture]
    public class MeanAggregationMethodTests
    {
        private static double tolerance = 0.0001;

        [Test]
        public void TestMean_NormalList()
        {
            List<float> errors = new List<float> { 1, 2, 3, 4, 5.5f };

            float expected = 2.5833333333f;
            float actual = AggregationMethods.Mean(errors);

            Assert.That(actual, Is.EqualTo(expected).Within(tolerance));
        }

        [Test]
        public void TestMean_EmptyList()
        {
            List<float> errors = new List<float>();

            float expected = 0.0f;
            float actual = AggregationMethods.Mean(errors);

            Assert.That(actual, Is.EqualTo(expected).Within(tolerance));
        }
    }
}