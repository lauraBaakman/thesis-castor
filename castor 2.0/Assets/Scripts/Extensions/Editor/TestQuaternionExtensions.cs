using UnityEngine;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class QuaternionExtensionTests
    {
        [Test]
        public void TestExtractEulerXAngle()
        {
            float expected = Random.value;

            Quaternion rotation = Quaternion.identity;
            rotation.eulerAngles = new Vector3(expected, Random.value, Random.value);

            float actual = rotation.ExtractEulerXAngle();

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001));
        }

        [Test]
        public void TestExtractEulerYAngle()
        {
            float expected = Random.value;

            Quaternion rotation = Quaternion.identity;
            rotation.eulerAngles = new Vector3(Random.value, expected, Random.value);

            float actual = rotation.ExtractEulerYAngle();

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001));
        }

        [Test]
        public void TestExtractEulerZAngle()
        {
            float expected = Random.value;

            Quaternion rotation = Quaternion.identity;
            rotation.eulerAngles = new Vector3(Random.value, Random.value, expected);

            float actual = rotation.ExtractEulerZAngle();

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001));
        }
    }
}