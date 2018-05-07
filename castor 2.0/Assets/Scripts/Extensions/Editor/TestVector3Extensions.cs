using UnityEngine;
using NUnit.Framework;

namespace Tests.Extensions
{
    [TestFixture]
    public class Vector3ExtensionTests
    {
        [Test]
        public void TestCompareTo_AllEqual()
        {
            Vector3 thisVector = new Vector3(1.0f, 2.0f, 3.0f);
            Vector3 otherVector = new Vector3(1.0f, 2.0f, 3.0f);

            int expected = 0;
            int actual = thisVector.CompareTo(otherVector);

            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void TestCompareTo_FirstNotEqual()
        {
            Vector3 thisVector = new Vector3(1.0f, 2.0f, 3.0f);
            Vector3 otherVector = new Vector3(5.0f, 2.0f, 3.0f);

            Assert.AreEqual(thisVector.CompareTo(otherVector), -1);
            Assert.AreEqual(otherVector.CompareTo(thisVector), +1);
        }

        [Test]
        public void TestCompareTo_SecondNotEqual()
        {
            Vector3 thisVector = new Vector3(1.0f, 2.0f, 3.0f);
            Vector3 otherVector = new Vector3(1.0f, 5.0f, 3.0f);

            Assert.AreEqual(thisVector.CompareTo(otherVector), -1);
            Assert.AreEqual(otherVector.CompareTo(thisVector), +1);
        }

        [Test]
        public void TestCompareTo_ThirdNotEqual()
        {
            Vector3 thisVector = new Vector3(1.0f, 2.0f, 3.0f);
            Vector3 otherVector = new Vector3(1.0f, 2.0f, 5.0f);

            Assert.AreEqual(thisVector.CompareTo(otherVector), -1);
            Assert.AreEqual(otherVector.CompareTo(thisVector), +1);
        }

        [Test]
        public void ChangeTransformOfDirection_TransformsAreNull()
        {
            Vector3 vector = Auxilaries.RandomPosition();

            Vector3 actual = vector.ChangeTransformOfDirection(null, null);
            Vector3 expected = vector;

            Assert.AreEqual(expected, actual);
            Assert.AreNotSame(vector, actual);
        }

        [Test]
        public void ChangeTransformOfPosition_TransformsAreNull()
        {
            Vector3 vector = Auxilaries.RandomPosition();

            Vector3 actual = vector.ChangeTransformOfPosition(null, null);
            Vector3 expected = vector;

            Assert.AreEqual(expected, actual);
            Assert.AreNotSame(vector, actual);
        }

        [Test]
        public void DuplicateVector()
        {
            Vector3 vector = Auxilaries.RandomPosition();

            Vector3 actual = vector.Duplicate();

            Assert.AreEqual(vector, actual);
            Assert.AreNotSame(vector, actual);
        }

        [Test, TestCaseSource("ContainsNanCases")]
        public void ContainsNan(Vector3 vector, bool expected)
        {
            bool actual = vector.ContainsNaNs();
            Assert.AreEqual(expected, actual);
        }

        static object[] ContainsNanCases = {
            new object[] {new Vector3(float.NaN, 1.0f, 2.0f), true},
            new object[] {new Vector3(1.0f, float.NaN, 2.0f), true},
            new object[] {new Vector3(1.0f, 2.0f, float.NaN), true},
            new object[] {new Vector3(float.NegativeInfinity, 1.0f, 2.0f), false},
            new object[] {new Vector3(1.0f, float.NegativeInfinity, 2.0f), false},
            new object[] {new Vector3(1.0f, 2.0f, float.NegativeInfinity), false},
            new object[] {new Vector3(float.PositiveInfinity, 1.0f, 2.0f), false},
            new object[] {new Vector3(1.0f, float.PositiveInfinity, 2.0f), false},
            new object[] {new Vector3(1.0f, 2.0f, float.PositiveInfinity), false},
            new object[] {new Vector3(+0, -1, +0), false},
        };

        [Test, TestCaseSource("ContainsNegativeInfinityCases")]
        public void ContainsNegativeInfinity(Vector3 vector, bool expected)
        {
            bool actual = vector.ContainsNegativeInfinity();
            Assert.AreEqual(expected, actual);
        }

        static object[] ContainsNegativeInfinityCases = {
            new object[] {new Vector3(float.NaN, 1.0f, 2.0f), false},
            new object[] {new Vector3(1.0f, float.NaN, 2.0f), false},
            new object[] {new Vector3(1.0f, 2.0f, float.NaN), false},
            new object[] {new Vector3(float.NegativeInfinity, 1.0f, 2.0f), true},
            new object[] {new Vector3(1.0f, float.NegativeInfinity, 2.0f), true},
            new object[] {new Vector3(1.0f, 2.0f, float.NegativeInfinity), true},
            new object[] {new Vector3(float.PositiveInfinity, 1.0f, 2.0f), false},
            new object[] {new Vector3(1.0f, float.PositiveInfinity, 2.0f), false},
            new object[] {new Vector3(1.0f, 2.0f, float.PositiveInfinity), false},
            new object[] {new Vector3(+0, -1, +0), false},
        };

        [Test, TestCaseSource("ContainsPositiveInfinityCases")]
        public void ContainsPositiveInfinity(Vector3 vector, bool expected)
        {
            bool actual = vector.ContainsPositiveInfinity();
            Assert.AreEqual(expected, actual);
        }

        static object[] ContainsPositiveInfinityCases = {
            new object[] {new Vector3(float.NaN, 1.0f, 2.0f), false},
            new object[] {new Vector3(1.0f, float.NaN, 2.0f), false},
            new object[] {new Vector3(1.0f, 2.0f, float.NaN), false},
            new object[] {new Vector3(float.NegativeInfinity, 1.0f, 2.0f), false},
            new object[] {new Vector3(1.0f, float.NegativeInfinity, 2.0f), false},
            new object[] {new Vector3(1.0f, 2.0f, float.NegativeInfinity), false},
            new object[] {new Vector3(float.PositiveInfinity, 1.0f, 2.0f), true},
            new object[] {new Vector3(1.0f, float.PositiveInfinity, 2.0f), true},
            new object[] {new Vector3(1.0f, 2.0f, float.PositiveInfinity), true},
            new object[] {new Vector3(+0, -1, +0), false},
        };

        [Test, TestCaseSource("ContainsNonNumericalCases")]
        public void ContainsNonNumerical(Vector3 vector, bool expected)
        {
            bool actual = vector.ContainsNonNumerical();
            Assert.AreEqual(expected, actual);
        }

        static object[] ContainsNonNumericalCases = {
            new object[] {new Vector3(float.NaN, 1.0f, 2.0f), true},
            new object[] {new Vector3(1.0f, float.NaN, 2.0f), true},
            new object[] {new Vector3(1.0f, 2.0f, float.NaN), true},
            new object[] {new Vector3(float.NegativeInfinity, 1.0f, 2.0f), true},
            new object[] {new Vector3(1.0f, float.NegativeInfinity, 2.0f), true},
            new object[] {new Vector3(1.0f, 2.0f, float.NegativeInfinity), true},
            new object[] {new Vector3(float.PositiveInfinity, 1.0f, 2.0f), true},
            new object[] {new Vector3(1.0f, float.PositiveInfinity, 2.0f), true},
            new object[] {new Vector3(1.0f, 2.0f, float.PositiveInfinity), true},
            new object[] {new Vector3(+0, -1, +0), false},
        };

    }
}