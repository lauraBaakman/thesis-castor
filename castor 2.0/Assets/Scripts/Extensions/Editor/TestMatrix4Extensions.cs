using UnityEngine;
using NUnit.Framework;
using Utils;

namespace Tests.Extensions
{
    [TestFixture]
    public class Matrix4ExtensionTests
    {
        float precision = 0.0001f;

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

        [Test, TestCaseSource("PrecisionIssueCases")]
        public void TestExtractRotation_PrecisionIssue(Matrix4x4 matrix, Quaternion expected)
        {
            Quaternion actual = matrix.ExtractRotation();

            float angle = Quaternion.Angle(actual, expected);
            Debug.Log(angle.ToString("e"));
            Assert.AreEqual(expected: 0.0f, actual: angle);
        }

        #region PrecisionIssueCases
        static object[] PrecisionIssueCases = {
            new object[] {
                new Matrix4x4(
                    column0: new Vector4(1, 0, 0, 0),
                    column1: new Vector4(0, 1, 0, 0),
                    column2: new Vector4(0, 0, 1, 0),
                    column3: new Vector4(0, 0, 0, 1)
                ),
                Quaternion.identity,
            },
            new object[] {
                new Matrix4x4(
                    column0: new Vector4(+0.612372435695795f, -0.353553390593274f, -0.707106781186547f, 0),
                    column1: new Vector4(+0.612372435695794f, -0.353553390593274f, +0.707106781186548f, 0),
                    column2: new Vector4(-0.500000000000000f, -0.866025403784439f, +0.000000000000000f, 0),
                    column3: new Vector4(+0.500000000000000f, +0.700000000000000f, -0.900000000000000f, 1)
                ),
                new Quaternion(
                    x: +0.7010573744773865f,
                    y: +0.09229595214128494f,
                    z: -0.4304593503475189f,
                    w: +0.5609855055809021f
                ),
            },
            new object[] {
                new Matrix4x4(
                    column0: new Vector4(+6.123722e-001f, -3.535532e-001f, -7.071065e-001f, 0),
                    column1: new Vector4(+6.123723e-001f, -3.535532e-001f, +7.071065e-001f, 0),
                    column2: new Vector4(-4.999998e-001f, -8.660251e-001f, +7.658173e-008f, 0),
                    column3: new Vector4(+0.000000e+000f, +0.000000e+000f, +0.000000e+000f, 1)
                ),
                new Quaternion(
                    x: +0.7010573744773865f,
                    y: +0.09229595214128494f,
                    z: -0.4304593503475189f,
                    w: +0.5609855055809021f
                ),
            },
        };
        #endregion

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
        public void TestSet_Scale_VectorInput()
        {
            Vector3 scale = new Vector3(1, 2, 3);
            Matrix4x4 matrix = new Matrix4x4().SetScale(scale);

            Matrix4x4 expected = new Matrix4x4();
            expected.SetTRS(new Vector3(), Quaternion.identity, scale);

            Assert.AreEqual(scale, matrix.ExtractScale());
            Assert.AreEqual(expected, matrix);
        }

        [Test]
        public void TestSet_Scale_ScalarInput()
        {
            float scale = 5;
            Vector3 scaleVector = new Vector3(scale, scale, scale);
            Matrix4x4 matrix = new Matrix4x4().SetScale(scale);

            Matrix4x4 expected = new Matrix4x4();
            expected.SetTRS(new Vector3(), Quaternion.identity, scaleVector);

            Assert.AreEqual(scaleVector, matrix.ExtractScale());
            Assert.AreEqual(expected, matrix);
        }
    }
}