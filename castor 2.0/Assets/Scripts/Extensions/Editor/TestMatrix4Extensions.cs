using UnityEngine;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Matrix4ExtensionTests
    {
        [Test]
        public void TestExtractTranslation_OnlyTranslationSet()
        {
            Vector3 translation = new Vector3(Random.value, Random.value, Random.value);

            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetTRS(
                pos: translation,
                q: Quaternion.identity,
                s: new Vector3(1.0f, 1.0f, 1.0f)
            );

            Vector3 expected = translation;
            Vector3 actual = matrix.ExtractTranslation();

            Assert.AreEqual(
                actual: actual,
                expected: expected
            );
        }

        [Test]
        public void TestExtractTranslation_TRSSet()
        {
            Vector3 translation = new Vector3(Random.value, Random.value, Random.value);
            Quaternion rotation = Random.rotation;
            Vector3 scale = new Vector3(Random.value, Random.value, Random.value);

            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetTRS(
                pos: translation,
                q: rotation,
                s: scale
            );

            Vector3 expected = translation;
            Vector3 actual = matrix.ExtractTranslation();

            Assert.AreEqual(
                actual: actual,
                expected: expected
            );
        }

        [Test]
        public void TestExtractScale_OnlyScaleSet()
        {
            Vector3 translation = new Vector3();
            Quaternion rotation = Quaternion.identity;
            Vector3 scale = new Vector3(2, 3, 4);

            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetTRS(
                pos: translation,
                q: rotation,
                s: scale
            );

            Vector3 expected = scale;
            Vector3 actual = matrix.ExtractScale();

            Assert.AreEqual(
                actual: actual,
                expected: expected
            );
        }

        [Test]
        public void TestExtractScale_TRSSet()
        {
            Vector3 translation = new Vector3(Random.value, Random.value, Random.value);
            Quaternion rotation = Random.rotation;
            Vector3 scale = new Vector3(2, 3, 4);

            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetTRS(
                pos: translation,
                q: rotation,
                s: scale
            );

            Vector3 expected = scale;
            Vector3 actual = matrix.ExtractScale();

            Assert.That(expected.x, Is.EqualTo(actual.x).Within(0.001f));
            Assert.That(expected.y, Is.EqualTo(actual.y).Within(0.001f));
            Assert.That(expected.z, Is.EqualTo(actual.z).Within(0.001f));
        }

        [Test]
        public void TestExtractRotation_OnlyRotationSet()
        {
            Vector3 translation = new Vector3(Random.value, Random.value, Random.value);
            Quaternion rotation = Random.rotation;

            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetTRS(
                pos: new Vector3(),
                q: rotation,
                s: new Vector3(1.0f, 1.0f, 1.0f)
            );

            Quaternion expected = rotation;
            Quaternion actual = matrix.ExtractRotation();

            //source https://answers.unity.com/answers/288354/view.html
            float angle = Quaternion.Angle(actual, expected);
            Assert.AreEqual(expected: 0.0f, actual: angle);
        }

        [Test]
        public void TestExtractRotation_TRSSet()
        {
            Vector3 translation = new Vector3(Random.value, Random.value, Random.value);
            Quaternion rotation = Random.rotation;
            Vector3 scale = new Vector3(Random.value, Random.value, Random.value);

            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetTRS(
                pos: translation,
                q: rotation,
                s: scale
            );

            Quaternion expected = rotation;
            Quaternion actual = matrix.ExtractRotation();

            //source https://answers.unity.com/answers/288354/view.html
            float angle = Quaternion.Angle(actual, expected);
            Assert.AreEqual(expected: 0.0f, actual: angle);
        }

        [Test]
        public void TestSet_Translation()
        {
            Vector3 translation = new Vector3(1, 2, 3);
            Matrix4x4 matrix = new Matrix4x4().SetTranslation(translation);

            Matrix4x4 expected = new Matrix4x4();
            expected.SetTRS(translation, Quaternion.identity, new Vector3(1, 1, 1));

            Assert.AreEqual(translation, matrix.ExtractTranslation());
            Assert.AreEqual(expected, matrix);
        }

        [Test]
        public void TestSet_Scale()
        {
            Vector3 scale = new Vector3(1, 2, 3);
            Matrix4x4 matrix = new Matrix4x4().SetScale(scale);

            Matrix4x4 expected = new Matrix4x4();
            expected.SetTRS(new Vector3(), Quaternion.identity, scale);

            Assert.AreEqual(scale, matrix.ExtractScale());
            Assert.AreEqual(expected, matrix);
        }
    }
}