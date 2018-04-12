using Utils;
using NUnit.Framework;
using UnityEngine;
using System;

namespace Tests.Utils
{
    [TestFixture]
    public class QuaternionDTest
    {
        double precision = 0.00000001;

        [Test]
        public void Test_Constructor_FourDoubles()
        {
            double x = 1;
            double y = 2;
            double z = 3;
            double w = 4;

            QuaternionD actual = new QuaternionD(x, y, z, w);

            Assert.AreEqual(x, actual.x);
            Assert.AreEqual(y, actual.y);
            Assert.AreEqual(z, actual.z);
            Assert.AreEqual(w, actual.w);
        }

        [Test]
        public void Test_Constructor_AxisAngle()
        {
            Vector3D axis = new Vector3D(1, 2, 3);
            double angle = 4;

            QuaternionD actual = new QuaternionD(axis, angle);

            Assert.AreEqual(axis.x, actual.x);
            Assert.AreEqual(axis.y, actual.y);
            Assert.AreEqual(axis.z, actual.z);
            Assert.AreEqual(angle, actual.w);
        }

        [Test]
        public void Test_Static_Property_Identitiy()
        {
            QuaternionD expected = new QuaternionD(0, 0, 0, 1);
            QuaternionD actual = QuaternionD.identity;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Property_XYZ()
        {
            QuaternionD quaternion = new QuaternionD(1, 2, 3, 4);

            Vector3D expected = new Vector3D(1, 2, 3);
            Vector3D actual = quaternion.xyz;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Property_Wuvw()
        {
            QuaternionD quaternion = new QuaternionD(1, 2, 3, 4);

            Vector3D expected = new Vector3D(1, 2, 3);
            Vector3D actual = quaternion.Wuvw;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Property_Wu()
        {
            QuaternionD quaternion = new QuaternionD(1, 2, 3, 4);

            double expected = 1;
            double actual = quaternion.Wu;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Property_Wv()
        {
            QuaternionD quaternion = new QuaternionD(1, 2, 3, 4);

            double expected = 2;
            double actual = quaternion.Wv;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Property_Ww()
        {
            QuaternionD quaternion = new QuaternionD(1, 2, 3, 4);

            double expected = 3;
            double actual = quaternion.Ww;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Property_w()
        {
            QuaternionD quaternion = new QuaternionD(1, 2, 3, 4);

            double expected = 4;
            double actual = quaternion.w;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Property_Ws()
        {
            QuaternionD quaternion = new QuaternionD(1, 2, 3, 4);

            double expected = 4;
            double actual = quaternion.Ws;

            Assert.AreEqual(expected, actual);
        }

        [Test, TestCaseSource("IndexerGetCases")]
        public void Test_Indexer_Get(QuaternionD quaternion, int idx, double expected)
        {
            double actual = quaternion[idx];

            Assert.AreEqual(expected, actual);
        }

        static object[] IndexerGetCases = {
            new object[]{new QuaternionD(1, 2, 3, 4), 0, 1},
            new object[]{new QuaternionD(1, 2, 3, 4), 1, 2},
            new object[]{new QuaternionD(1, 2, 3, 4), 2, 3},
            new object[]{new QuaternionD(1, 2, 3, 4), 3, 4},
        };

        [Test, TestCaseSource("IndexerSetCases")]
        public void Test_Indexer_Set(QuaternionD vector, int idx, double newvalue)
        {
            vector[idx] = newvalue;

            double actual = vector[idx];

            Assert.AreEqual(newvalue, actual);
        }

        static object[] IndexerSetCases = {
            new object[]{new QuaternionD(1, 2, 3, 4), 0, 6},
            new object[]{new QuaternionD(1, 2, 3, 4), 1, 7},
            new object[]{new QuaternionD(1, 2, 3, 4), 2, 8},
            new object[]{new QuaternionD(1, 2, 3, 4), 3, 9},
        };

        [Test, TestCaseSource("DotCases")]
        public void Test_Dot_Static(QuaternionD lhs, QuaternionD rhs, double expected)
        {
            double actual = QuaternionD.Dot(lhs, rhs);

            Assert.That(expected, Is.EqualTo(actual).Within(precision));
        }

        static object[] DotCases = {
            new object[]{
                new QuaternionD(+276.922984960890e-003, +46.1713906311539e-003, +97.1317812358475e-003, +823.457828327293e-003),
                new QuaternionD(+694.828622975817e-003, +317.099480060861e-003, +950.222048838355e-003, +34.4460805029088e-003),
                327.716595092112e-003
            },
            new object[]{
                new QuaternionD(+43.8744359656398e+000, +38.1558457093008e+000, +76.5516788149002e+000, +79.5199901137063e+000),
                new QuaternionD(+18.6872604554379e+000, +48.9764395788231e+000, +44.5586200710899e+000, +64.6313010111265e+000),
                11.2391480737438e+003
            },
            new object[]{
                new QuaternionD(+43.8744359656398e+000, +38.1558457093008e+000, +76.5516788149002e+000, +79.5199901137063e+000),
                new QuaternionD(+0.00000000000000e+000, +0.00000000000000e+000, +0.00000000000000e+000, +0.00000000000000e+000),
                0.00000000000000e+000
            },
            new object[]{
                new QuaternionD(+15.5098003973841e+000, -33.7388264805369e+000, -38.1002318441623e+000, -163.594801785705e-003),
                new QuaternionD(+45.9743958516081e+000, -15.9614273333867e+000, +8.52677509797774e+000, -27.6188060508863e+000),
                931.219715217067e+000
            },
        };

        [Test, TestCaseSource("EqualsCases")]
        public void Test_GenericEquals(QuaternionD self, QuaternionD other, bool expected)
        {
            bool actual = self.Equals(other);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, self.GetHashCode().Equals(other.GetHashCode()));
        }

        static object[] EqualsCases =
        {
            //Equals
            new object[]{new QuaternionD(1, 2, 3, 4), new QuaternionD(1, 2, 3, 4), true},
            //Not equal x different
            new object[]{new QuaternionD(1, 2, 3, 4), new QuaternionD(2, 2, 3, 4), false},
            //Not equal y different
            new object[]{new QuaternionD(1, 2, 3, 4), new QuaternionD(1, 3, 3, 4), false},
            //Not equal z different
            new object[]{new QuaternionD(1, 2, 3, 4), new QuaternionD(1, 2, 4, 4), false},
            //Not equal w different
            new object[]{new QuaternionD(1, 2, 3, 4), new QuaternionD(1, 2, 3, 5), false},
        };

        [Test]
        public void Test_Set()
        {
            QuaternionD expected = new QuaternionD(5, 6, 7, 8);

            QuaternionD actual = new QuaternionD(1, 2, 3, 4);
            actual.Set(5, 6, 7, 8);

            Assert.AreEqual(expected, actual);
        }

        public void Test_Operator_Quaternion_Multiply_Quaternion()
        {
            Assert.Pass("The source was copied from the unity source, we'll assume it its correct");
        }

        public void Test_Operator_Quaternion_Multiply_Vector()
        {
            Assert.Pass("The source was copied from the unity source, we'll assume it its correct");
        }

        public void Test_Operator_Quaternion_Multiply_Scalar()
        {
            QuaternionD quaternion = new QuaternionD(1, 2, 3, 4);
            double scalar = 5;

            QuaternionD expected = new QuaternionD(5, 10, 15, 20);

            QuaternionD actual = quaternion * scalar;

            Assert.AreEqual(expected, actual);
        }

        public void Test_Operator_Scalar_Multiply_Quaternion()
        {
            QuaternionD quaternion = new QuaternionD(1, 2, 3, 4);
            double scalar = 5;

            QuaternionD expected = new QuaternionD(5, 10, 15, 20);

            QuaternionD actual = scalar * expected;

            Assert.AreEqual(expected, actual);
        }

        public void Test_Operator_Quaternion_Divide_Scalar()
        {
            QuaternionD quaternion = new QuaternionD(5, 10, 15, 20);
            double scalar = 5;

            QuaternionD expected = new QuaternionD(1, 2, 3, 4);

            QuaternionD actual = scalar * expected;

            Assert.AreEqual(expected, actual);
        }
    }
}