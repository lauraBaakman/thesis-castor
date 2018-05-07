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


        [Test, TestCaseSource("TranslationCases")]
        public void TestTransformationMatrixFromTranslation(Vector3 translation, Matrix4x4 expected)
        {
            Matrix4x4 actual = new Matrix4x4().TransformationMatrixFromTranslation(translation);

            Assert.AreEqual(actual, expected);
        }

        static object[] TranslationCases =
        {
            new object[]{
                new Vector3(), Matrix4x4.identity
            },
            new object[]{
                new Vector3(7, 2, 3),
                new Matrix4x4(
                    column0: new Vector4(1, 0, 0, 0),
                    column1: new Vector4(0, 1, 0, 0),
                    column2: new Vector4(0, 0, 1, 0),
                    column3: new Vector4(7, 2, 3, 1)
                )
            },
        };

        [Test, TestCaseSource("UnitQuaternionRotationCases")]
        public void TransformationMatrixFromUnitQuaternion(Quaternion quaternion, Matrix4x4 expected)
        {
            Matrix4x4 actual = new Matrix4x4().TransformationMatrixFromUnitQuaternion(quaternion);

            Assert.AreEqual(actual, expected);
        }

        static object[] UnitQuaternionRotationCases =
        {
            new object[]{
                Quaternion.identity, Matrix4x4.identity
            },
            new object[]{
                new Quaternion(207.143590429944e-003f, -477.275046098083e-003f, 848.758235335545e-003f, 94.3902604401274e-003f),
                new Matrix4x4(
                    column0: new Vector4(-896.364023355673e-003f, -37.4999115746978e-003f, +441.729888354320e-003f, 0),
                    column1: new Vector4(-357.957955110798e-003f, -526.598018212234e-003f, -771.077576893430e-003f, 0),
                    column2: new Vector4(+261.529424743232e-003f, -849.286926690172e-003f, +458.600126631728e-003f, 0),
                    column3: new Vector4(+0.00000000000000e+000f, +0.00000000000000e+000f, +0.00000000000000e+000f, 1)
                )
            },
        };


        [Test]
        public void TransformationMatrixFromTranslation()
        {
            Vector4 translation = new Vector4(7, 2, 3, 1);

            Matrix4x4 expected = new Matrix4x4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(7, 2, 3, 1)
            );

            Matrix4x4 actual = new Matrix4x4().TransformationMatrixFromTranslation(translation);

            Assert.AreEqual(actual, expected);
        }

        [Test, TestCaseSource("QuaternionRotationCases")]
        public void TransformationMatrixFromQuaternion(Quaternion quaternion, Matrix4x4 expected)
        {
            Matrix4x4 actual = new Matrix4x4().TransformationMatrixFromQuaternion(quaternion);

            Assert.AreEqual(actual, expected);
        }

        static object[] QuaternionRotationCases =
        {
            new object[]{
                Quaternion.identity, Matrix4x4.identity
            },
            new object[]{
                new Quaternion(-279.664714638139e-003f, +241.992424109666e-003f, -755.589458510601e-003f, 540.658750273731e-003f),
                new Matrix4x4(
                    column0: new Vector4(-258.951526277632e-003f, -952.385589183136e-003f, +160.952777406220e-003f, 0),
                    column1: new Vector4(+681.678620250251e-003f, -298.255564851950e-003f, -668.100199617212e-003f, 0),
                    column2: new Vector4(+684.294063785591e-003f, -63.2874991695496e-003f, +726.454628119792e-003f, 0),
                    column3: new Vector4(+0.00000000000000e+000f, +0.00000000000000e+000f, +0.00000000000000e+000f, 1)
                )
            },
        };
    }
}