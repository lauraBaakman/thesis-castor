using Utils;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Utils
{
    [TestFixture]
    public class Matrix4x4DTest
    {
        double precision = 0.00000001;

        static Vector4D ACol0 = new Vector4D(-13, +45, +23, 00);
        static Vector4D ACol1 = new Vector4D(+10, -34, -34, 00);
        static Vector4D ACol2 = new Vector4D(-44, +37, +10, 00);
        static Vector4D ACol3 = new Vector4D(+00, +00, +00, 01);
        static Matrix4x4D A = new Matrix4x4D(ACol0, ACol1, ACol2, ACol3);

        static Vector4D BCol0 = new Vector4D(-32, -20, +02, -07);
        static Vector4D BCol1 = new Vector4D(-21, +11, -36, -21);
        static Vector4D BCol2 = new Vector4D(-13, -04, +29, -30);
        static Vector4D BCol3 = new Vector4D(+01, +09, -45, +11);
        static Matrix4x4D B = new Matrix4x4D(BCol0, BCol1, BCol2, BCol3);

        static Vector4D CCol0 = new Vector4D(-33, -43, +45, +47);
        static Vector4D CCol1 = new Vector4D(+31, -20, -40, +18);
        static Vector4D CCol2 = new Vector4D(-06, -38, +00, -47);
        static Vector4D CCol3 = new Vector4D(+41, -24, +16, -19);
        static Matrix4x4D C = new Matrix4x4D(CCol0, CCol1, CCol2, CCol3);

        [SetUp]
        public void SetUp()
        { }

        [Test]
        public void Test_Constructor_NoArguments()
        {
            Matrix4x4D actual = new Matrix4x4D();

            Assert.AreEqual(0, actual.m00);
            Assert.AreEqual(0, actual.m01);
            Assert.AreEqual(0, actual.m02);
            Assert.AreEqual(0, actual.m03);

            Assert.AreEqual(0, actual.m10);
            Assert.AreEqual(0, actual.m11);
            Assert.AreEqual(0, actual.m12);
            Assert.AreEqual(0, actual.m13);

            Assert.AreEqual(0, actual.m20);
            Assert.AreEqual(0, actual.m21);
            Assert.AreEqual(0, actual.m22);
            Assert.AreEqual(0, actual.m23);

            Assert.AreEqual(0, actual.m30);
            Assert.AreEqual(0, actual.m31);
            Assert.AreEqual(0, actual.m32);
            Assert.AreEqual(0, actual.m33);
        }

        [Test]
        public void Test_Constructor_ColumnVectors()
        {
            Vector4D column0 = new Vector4D(-13, +45, +23, +10);
            Vector4D column1 = new Vector4D(-34, -34, -44, +37);
            Vector4D column2 = new Vector4D(+10, +21, -48, +47);
            Vector4D column3 = new Vector4D(+33, -29, -32, -32);

            Matrix4x4D actual = new Matrix4x4D(
                column0, column1, column2, column3
            );

            Assert.AreEqual(-13, actual.m00);
            Assert.AreEqual(-34, actual.m01);
            Assert.AreEqual(+10, actual.m02);
            Assert.AreEqual(+33, actual.m03);

            Assert.AreEqual(+45, actual.m10);
            Assert.AreEqual(-34, actual.m11);
            Assert.AreEqual(+21, actual.m12);
            Assert.AreEqual(-29, actual.m13);

            Assert.AreEqual(+23, actual.m20);
            Assert.AreEqual(-44, actual.m21);
            Assert.AreEqual(-48, actual.m22);
            Assert.AreEqual(-32, actual.m23);

            Assert.AreEqual(+10, actual.m30);
            Assert.AreEqual(+37, actual.m31);
            Assert.AreEqual(+47, actual.m32);
            Assert.AreEqual(-32, actual.m33);
        }

        [Test]
        public void Test_Constructor_Doubles()
        {
            Matrix4x4D actual = new Matrix4x4D(
                -32, -21, -13, +01,
                -20, +11, -04, +09,
                +02, -36, +29, -45,
                -07, -21, -30, +11
            );

            Matrix4x4D expected = B;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Static_Constructor_Translation()
        {
            Vector4D translation = new Vector4D(7, 2, 3, 1);

            Matrix4x4D expected = new Matrix4x4D(
                new Vector4D(1, 0, 0, 0),
                new Vector4D(0, 1, 0, 0),
                new Vector4D(0, 0, 1, 0),
                new Vector4D(7, 2, 3, 1)
            );

            Matrix4x4D actual = Matrix4x4D.TransformationMatrixFromTranslation(translation);

            Assert.AreEqual(actual, expected);
        }

        [Test, TestCaseSource("UnitQuaternionRotationCases")]
        public void Test_Static_Constructor_Rotation_Unit_Quaternion(QuaternionD quaternion, Matrix4x4D expected)
        {
            Matrix4x4D actual = Matrix4x4D.TransformationMatrixFromUnitQuaternion(quaternion);

            Assert.AreEqual(actual, expected);
        }


        [Test, TestCaseSource("QuaternionRotationCases")]
        public void Test_Static_Constructor_Rotation_Quaternion(QuaternionD quaternion, Matrix4x4D expected)
        {
            Matrix4x4D actual = Matrix4x4D.TransformationMatrixFromUnitQuaternion(quaternion);

            Assert.AreEqual(actual, expected);
        }

        static object[] QuaternionRotationCases =
        {
            new object[]{
                QuaternionD.identity, Matrix4x4D.identity
            },
            new object[]{
                new QuaternionD(-279.664714638139e-003, +241.992424109666e-003, -755.589458510601e-003, 540.658750273731e-003),
                new Matrix4x4D(
                    new Vector4D(-258.951526277632e-003, +681.678620250251e-003, +684.294063785591e-003, 0),
                    new Vector4D(-952.385589183136e-003, -298.255564851950e-003, -63.2874991695496e-003, 0),
                    new Vector4D(+160.952777406220e-003, -668.100199617212e-003, +726.454628119792e-003, 0),
                    new Vector4D(+0.00000000000000e+000, +0.00000000000000e+000, +0.00000000000000e+000, 1)
                )
            },
        };

        [Test, TestCaseSource("EqualsCases")]
        public void Test_Static_Constructor_Rotation_Unit_Quaternion(Matrix4x4D self, Matrix4x4D other, bool expected)
        {
            bool actual = self.Equals(other);
            bool hasCodeActual = self.GetHashCode().Equals(other.GetHashCode());

            Assert.AreEqual(actual, expected);
            Assert.AreEqual(hasCodeActual, expected);
        }

        static object[] EqualsCases =
        {
            new object[]{
                B, B, true
            },
            new object[]{
                B, new Matrix4x4D(new Vector4D(-31, -20, +02, -07), BCol1, BCol2, BCol3), false
            },
            new object[]{
                B, new Matrix4x4D(new Vector4D(-32, -21, +02, -07), BCol1, BCol2, BCol3), false
            },
            new object[]{
                B, new Matrix4x4D(new Vector4D(-32, -20, +03, -07), BCol1, BCol2, BCol3), false
            },
            new object[]{
                B, new Matrix4x4D(new Vector4D(-32, -20, +02, -08), BCol1, BCol2, BCol3), false
            },
            new object[]{
                B, new Matrix4x4D(BCol0, new Vector4D(-22, +11, -36, -21), BCol2, BCol3), false
            },
            new object[]{
                B, new Matrix4x4D(BCol0, new Vector4D(-21, +12, -36, -21), BCol2, BCol3), false
            },
            new object[]{
                B, new Matrix4x4D(BCol0, new Vector4D(-21, +11, -37, -21), BCol2, BCol3), false
            },
            new object[]{
                B, new Matrix4x4D(BCol0, new Vector4D(-21, +11, -36, -22), BCol2, BCol3), false
            },
            new object[]{
                B, new Matrix4x4D(BCol0, BCol1, new Vector4D(-14, -04, +29, -45), BCol3), false
            },
            new object[]{
                B, new Matrix4x4D(BCol0, BCol1, new Vector4D(-13, -05, +29, -45), BCol3), false
            },
            new object[]{
                B, new Matrix4x4D(BCol0, BCol1, new Vector4D(-13, -04, +28, -45), BCol3), false
            },
            new object[]{
                B, new Matrix4x4D(BCol0, BCol1, new Vector4D(-13, -04, +29, -44), BCol3), false
            },
            new object[]{
                B, new Matrix4x4D(BCol0, BCol1, BCol2, new Vector4D(+02, +09, -45, +11)), false
            },
            new object[]{
                B, new Matrix4x4D(BCol0, BCol1, BCol2, new Vector4D(+01, +08, -45, +11)), false
            },
            new object[]{
                B, new Matrix4x4D(BCol0, BCol1, BCol2, new Vector4D(+01, +09, -43, +11)), false
            },
            new object[]{
                B, new Matrix4x4D(BCol0, BCol1, BCol2, new Vector4D(+01, +09, -45, +12)), false
            },
        };

        [Test, TestCaseSource("IndexGetCases")]
        public void Test_Index_Get(Matrix4x4D matrix, int row, int col, double expected)
        {
            double actual = matrix[row, col];

            Assert.AreEqual(expected, actual);
        }

        static object[] IndexGetCases =
        {
            new object[]{B, 0, 0, -32},
            new object[]{B, 0, 1, -21},
            new object[]{B, 0, 2, -13},
            new object[]{B, 0, 3, +01},

            new object[]{B, 1, 0, -20},
            new object[]{B, 1, 1, +11},
            new object[]{B, 1, 2, -04},
            new object[]{B, 1, 3, +09},

            new object[]{B, 2, 0, +02},
            new object[]{B, 2, 1, -36},
            new object[]{B, 2, 2, +29},
            new object[]{B, 2, 3, -45},

            new object[]{B, 3, 0, -07},
            new object[]{B, 3, 1, -21},
            new object[]{B, 3, 2, -30},
            new object[]{B, 3, 3, +11},

            new object[]{C, 3, 2, -47},
            new object[]{C, 2, 3, +16},
        };

        [Test, TestCaseSource("IndexSetCases")]
        public void Test_Index_Set(Matrix4x4D matrix, int row, int col, double value)
        {
            matrix[row, col] = value;
            Assert.AreEqual(value, matrix[row, col]);
        }

        static object[] IndexSetCases =
        {
            new object[]{new Matrix4x4D(BCol0, BCol1, BCol2, BCol3), 0, 0, -31},
            new object[]{new Matrix4x4D(BCol0, BCol1, BCol2, BCol3), 0, 1, -21},
            new object[]{new Matrix4x4D(BCol0, BCol1, BCol2, BCol3), 0, 2, -13},
            new object[]{new Matrix4x4D(BCol0, BCol1, BCol2, BCol3), 0, 3, +01},

            new object[]{new Matrix4x4D(BCol0, BCol1, BCol2, BCol3), 1, 0, -20},
            new object[]{new Matrix4x4D(BCol0, BCol1, BCol2, BCol3), 1, 1, +11},
            new object[]{new Matrix4x4D(BCol0, BCol1, BCol2, BCol3), 1, 2, +29},
            new object[]{new Matrix4x4D(BCol0, BCol1, BCol2, BCol3), 1, 3, -45},

            new object[]{new Matrix4x4D(BCol0, BCol1, BCol2, BCol3), 2, 0, +02},
            new object[]{new Matrix4x4D(BCol0, BCol1, BCol2, BCol3), 2, 1, -36},
            new object[]{new Matrix4x4D(BCol0, BCol1, BCol2, BCol3), 2, 2, +29},
            new object[]{new Matrix4x4D(BCol0, BCol1, BCol2, BCol3), 2, 3, -45},

            new object[]{new Matrix4x4D(BCol0, BCol1, BCol2, BCol3), 3, 0, -07},
            new object[]{new Matrix4x4D(BCol0, BCol1, BCol2, BCol3), 3, 1, -21},
            new object[]{new Matrix4x4D(BCol0, BCol1, BCol2, BCol3), 3, 2, -30},
            new object[]{new Matrix4x4D(BCol0, BCol1, BCol2, BCol3), 3, 3, +11},
        };

        [Test]
        public void Test_Static_Property_IdentityMatrix()
        {
            Matrix4x4D theActualMatrix = Matrix4x4D.identity;

            Matrix4x4D theExpectedMatrix = new Matrix4x4D(
                    new Vector4D(1, 0, 0, 0),
                    new Vector4D(0, 1, 0, 0),
                    new Vector4D(0, 0, 1, 0),
                    new Vector4D(0, 0, 0, 1)
            );

            Assert.AreEqual(theExpectedMatrix, theActualMatrix);
        }

        [Test]
        public void Test_Static_Property_ZeroMatrix()
        {
            Matrix4x4D actual = Matrix4x4D.zero;

            Matrix4x4D expected = new Matrix4x4D(
                    new Vector4D(0, 0, 0, 0),
                    new Vector4D(0, 0, 0, 0),
                    new Vector4D(0, 0, 0, 0),
                    new Vector4D(0, 0, 0, 0)
            );

            Assert.AreEqual(expected, actual);
        }

        [Test, TestCaseSource("GetColumnCases")]
        public void Test_GetColumn(Matrix4x4D matrix, int idx, Vector4D expected)
        {
            Vector4D actual = matrix.GetColumn(idx);

            Assert.AreEqual(expected, actual);
        }

        static object[] GetColumnCases =
        {
            new object[]{B, 0, BCol0},
            new object[]{B, 1, BCol1},
            new object[]{B, 2, BCol2},
            new object[]{B, 3, BCol3},
        };

        [Test]
        public void Test_ToUnityMatrix()
        {
            Matrix4x4 expected = new Matrix4x4(
                BCol0.ToUnityVector(), BCol1.ToUnityVector(), BCol2.ToUnityVector(), BCol3.ToUnityVector()
            );

            Matrix4x4 actual = B.ToUnityMatrix4x4();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Operator_Matrix_Multiplication_Matrix()
        {
            Matrix4x4D expected = new Matrix4x4D(
                new Vector4D(+1421, +0430, +0672, +0301),
                new Vector4D(-0034, -0518, -1188, +1601),
                new Vector4D(+0943, -0721, +3471, +0323),
                new Vector4D(-1035, -1319, +2265, -0472)
            );

            Matrix4x4D actual = B * C;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Operator_Matrix_Multiplication_Vector()
        {
            Vector4D expected = new Vector4D(137, 1868, -752, -1825);

            Vector4D actual = C * BCol0;

            Assert.AreEqual(expected, actual);
        }
    }
}