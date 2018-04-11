using Utils;
using NUnit.Framework;
using UnityEngine;
using System;

namespace Tests.Utils
{
    [TestFixture]
    public class Vector3DTest
    {
        double precision = 0.00000001;

        [Test]
        public void Test_Constructor_Three_Doubles()
        {
            double x = 1;
            double y = 2;
            double z = 3;

            Vector3D actual = new Vector3D(x, y, z);

            Assert.AreEqual(x, actual.x);
            Assert.AreEqual(y, actual.y);
            Assert.AreEqual(z, actual.z);
        }


        [Test]
        public void Test_Constructor_NoArguments()
        {
            Vector3D actual = new Vector3D();

            Assert.AreEqual(0, actual.x);
            Assert.AreEqual(0, actual.y);
            Assert.AreEqual(0, actual.z);
        }

        [Test]
        public void Test_Constructor_CopyConstructor()
        {
            Vector3D input = new Vector3D(1, 2, 3);
            Vector3D actual = new Vector3D(input);

            Assert.AreEqual(input.x, actual.x);
            Assert.AreEqual(input.y, actual.y);
            Assert.AreEqual(input.z, actual.z);
        }

        [Test, TestCaseSource("EqualsCases")]
        public void Test_GenericEquals(Vector3D self, Vector3D other, bool expected)
        {
            bool actual = self.Equals(other);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, self.GetHashCode().Equals(other.GetHashCode()));
        }

        static object[] EqualsCases =
        {
            //Equals
            new object[]{
                new Vector3D(1, 2, 3), new Vector3D(1, 2, 3), true
            },
            //Not equal x different
            new object[]{
                new Vector3D(1, 2, 3), new Vector3D(2, 2, 3), false
            },
            //Not equal y different
            new object[]{
                new Vector3D(1, 2, 3), new Vector3D(1, 3, 3), false
            },
            //Not equal z different
            new object[]{
                new Vector3D(1, 2, 3), new Vector3D(1, 2, 4), false
            },
        };

        [Test, TestCaseSource("ScalarMultiplicationCases")]
        public void Test_Scaling_Operator(Vector3D vector, double scale, Vector3D expected)
        {
            Vector3D actual = vector * scale;
            Assert.IsTrue(expected.Equals(actual));

            actual = scale * vector;
            Assert.IsTrue(expected.Equals(actual));
        }

        [Test, TestCaseSource("ScalarMultiplicationCases")]
        public void Test_Scaling_ShortHandOperator(Vector3D lhs, double scale, Vector3D expected)
        {
            Vector3D actual = new Vector3D(lhs);
            actual *= scale;
            Assert.IsTrue(expected.Equals(actual));
        }

        static object[] ScalarMultiplicationCases = {
            new object[] {
                new Vector3D(+530.797553008973e-003, +779.167230102011e-003, +934.010684229183e-003),
                +817.303220653433e-003,
                new Vector3D(+433.822549589195e-003, +636.815886589988e-003, +763.369940345228e-003),
            },
            new object[] {
                new Vector3D(+16.2182308193243e+000, +79.4284540683907e+000, +31.1215042044805e+000),
                +86.8694705363510e+000,
                new Vector3D(+1.40886912431103e+003, +6.89990775044197e+003, +2.70350859253804e+003),
            },
            new object[] {
                new Vector3D(+189.214503140008e-003, +248.151592823709e-003, -49.4584014975022e-003),
                -415.564154489090e-003,
                new Vector3D(-78.6307650144505e-003, -103.122906856906e-003, +20.5531388006914e-003),
            },
            new object[] {
                new Vector3D(+3.83424352600571e+000, +49.6134716626885e+000, -42.1824471246816e+000),
                -10.0217350901104e+000,
                new Vector3D(-38.4257728885999e+000, -497.213069904161e+000, +422.741310536146e+000),
            },
        };

        [Test, TestCaseSource("ScalarDivisionCases")]
        public void Test_Division_Operator(Vector3D vector, double scale, Vector3D expected)
        {
            Vector3D actual = vector / scale;
            Assert.IsTrue(expected.Equals(actual));
        }

        [Test, TestCaseSource("ScalarDivisionCases")]
        public void Test_Division_ShortHandOperator(Vector3D lhs, double scale, Vector3D expected)
        {
            Vector3D actual = new Vector3D(lhs);
            actual /= scale;
            Assert.IsTrue(expected.Equals(actual));
        }

        static object[] ScalarDivisionCases = {
            new object[] {
                new Vector3D(+530.797553008973e-003, +779.167230102011e-003, +934.010684229183e-003),
                +817.303220653433e-003,
                new Vector3D(+649.449971070224e-003, 953.339238623173e-003, 1.14279579552182e+000),
            },
            new object[] {
                new Vector3D(+16.2182308193243e+000, +79.4284540683907e+000, +31.1215042044805e+000),
                +86.8694705363510e+000,
                new Vector3D(+186.696554257663e-003, 914.342559911810e-003, 358.255944376425e-003),
            },
            new object[] {
                new Vector3D(+189.214503140008e-003, +248.151592823709e-003, -49.4584014975022e-003),
                -415.564154489090e-003,
                new Vector3D(-455.319596495601e-003, -597.143882943408e-003, +119.015080976626e-003),
            },
            new object[] {
                new Vector3D(+3.83424352600571e+000, +49.6134716626885e+000, -42.1824471246816e+000),
                -10.0217350901104e+000,
                new Vector3D(-382.592783737560e-003, -4.95058702076930e+000, +4.20909620394059e+000),
            },
        };
    }
}