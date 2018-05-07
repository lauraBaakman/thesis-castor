using NUnit.Framework;
using UnityEngine;
using Utils;

namespace Tests.Utils
{
    [TestFixture]
    public class MatrixUtilsTests
    {
        [Test, TestCaseSource("TranslationCases")]
        public void TestTransformationMatrixFromTranslation(Vector3 translation, Matrix4x4 expected)
        {
            Matrix4x4 actual = MatrixUtils.TransformationMatrixFromTranslation(translation);

            Assert.AreEqual(actual, expected);
        }

        static object[] TranslationCases =
        {
            new object[]{
                new Vector3(0, 0, 0),
                new Matrix4x4(
                    column0: new Vector4(1, 0, 0, 0),
                    column1: new Vector4(0, 1, 0, 0),
                    column2: new Vector4(0, 0, 1, 0),
                    column3: new Vector4(0, 0, 0, 1)
                )
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
            Matrix4x4 actual = MatrixUtils.TransformationMatrixFromUnitQuaternion(quaternion);

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


        [Test, TestCaseSource("QuaternionRotationCases")]
        public void TransformationMatrixFromQuaternion(Quaternion quaternion, Matrix4x4 expected)
        {
            Matrix4x4 actual = MatrixUtils.TransformationMatrixFromQuaternion(quaternion);

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