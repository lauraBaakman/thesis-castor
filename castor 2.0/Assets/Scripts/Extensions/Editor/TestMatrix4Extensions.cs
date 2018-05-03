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

        [Test]
        public void TestFill_ValidArray()
        {
            double[,] array = {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 10, 11, 12 },
                { 13, 14, 15, 16 }
            };

            Matrix4x4 expected = new Matrix4x4(
                new Vector4(1, 5, 9, 13),
                new Vector4(2, 6, 10, 14),
                new Vector4(3, 7, 11, 15),
                new Vector4(4, 8, 12, 16)
            );

            Matrix4x4 actual = new Matrix4x4().Filled(array);

            for (int i = 0; i < 16; i++) Assert.That(actual[i], Is.EqualTo(expected[i]).Within(precision));
        }

        [Test]
        public void TestFill_InvalidRowCountArray()
        {
            Assert.Throws(typeof(System.ArgumentException), new TestDelegate(TestFill_InvalidRowCountArray_Helper));
        }

        private void TestFill_InvalidRowCountArray_Helper()
        {
            double[,] array = {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 10, 11, 12 }
            };
            new Matrix4x4().Filled(array);
        }

        [Test]
        public void TestFill_InvalidColCountArray()
        {
            Assert.Throws(typeof(System.ArgumentException), new TestDelegate(TestFill_InvalidColCountArray_Helper));
        }

        private void TestFill_InvalidColCountArray_Helper()
        {
            double[,] array = {
                { 1, 2, 3, 4, 5 },
                { 5, 6, 7, 8, 6 },
                { 9, 10, 11, 12, 13},
                { 13, 14, 15, 16, 17}
            };
            new Matrix4x4().Filled(array);
        }

        [Test]
        public void Test_DiagonalSet_InvalidArray()
        {
            Assert.Throws(typeof(System.ArgumentException), new TestDelegate(Test_DiagonalSet_InvalidArray_Helper));
        }

        private void Test_DiagonalSet_InvalidArray_Helper()
        {
            double[] array = { 1, 2, 3 };
            new Matrix4x4().DiagonalFilled(array);
        }

        [Test]
        public void Test_DiagonalSet_ValidArray()
        {
            double[] array = { 1, 2, 3, 4 };

            Matrix4x4 expected = new Matrix4x4();
            expected[0, 0] = 1;
            expected[1, 1] = 2;
            expected[2, 2] = 3;
            expected[3, 3] = 4;

            Matrix4x4 actual = new Matrix4x4().DiagonalFilled(array);

            for (int i = 0; i < 16; i++) Assert.That(actual[i], Is.EqualTo(expected[i]).Within(precision));
        }

        [Test]
        public void MultiplyVector()
        {
            //Note, the vectors are the columns of the matrix
            Matrix4x4 matrix = new Matrix4x4(
                new Vector4(-20, +02, -07, -21),
                new Vector4(+11, -36, -21, -13),
                new Vector4(-04, +29, -30, +01),
                new Vector4(+09, -45, +11, -33)
            );
            Vector4D vector = new Vector4D(+33, -29, -32, -32);

            Vector4D expected = new Vector4D(-1139, +1622, +0986, +0708);
            Vector4D actual = matrix.MultiplyVector(vector);

            Assert.AreEqual(expected, actual);
        }
    }
}