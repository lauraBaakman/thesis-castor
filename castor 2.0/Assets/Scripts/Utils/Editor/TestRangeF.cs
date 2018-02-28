using UnityEngine;
using System.Collections;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class RangeFTests
    {

        [Test]
        public void EqualsTest_Equal()
        {
            RangeF thisRange = new RangeF(1, 3);
            RangeF otherRange = new RangeF(1, 3);

            Assert.IsTrue(thisRange.Equals(otherRange));
            Assert.IsTrue(otherRange.Equals(thisRange));
            Assert.AreEqual(thisRange.GetHashCode(), otherRange.GetHashCode());
        }

        [Test]
        public void EqualsTest_MinNotEqual()
        {
            RangeF thisRange = new RangeF(1, 3);
            RangeF otherRange = new RangeF(2, 3);

            Assert.IsFalse(thisRange.Equals(otherRange));
            Assert.IsFalse(otherRange.Equals(thisRange));
            Assert.AreNotEqual(thisRange.GetHashCode(), otherRange.GetHashCode());
        }

        [Test]
        public void EqualsTest_MaxNotEqual()
        {
            RangeF thisRange = new RangeF(1, 3);
            RangeF otherRange = new RangeF(1, 4);

            Assert.IsFalse(thisRange.Equals(otherRange));
            Assert.IsFalse(otherRange.Equals(thisRange));
            Assert.AreNotEqual(thisRange.GetHashCode(), otherRange.GetHashCode());
        }

        [Test]
        public void EqualsTest_BothNotEqual()
        {
            RangeF thisRange = new RangeF(2, 3);
            RangeF otherRange = new RangeF(1, 4);

            Assert.IsFalse(thisRange.Equals(otherRange));
            Assert.IsFalse(otherRange.Equals(thisRange));
            Assert.AreNotEqual(thisRange.GetHashCode(), otherRange.GetHashCode());
        }

        [Test]
        public void LengthTest_BothPositive()
        {
            RangeF range = new RangeF(3, 5);

            float expected = 2.0f;
            float actual = range.Length;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LengthTest_NegativePositive()
        {
            RangeF range = new RangeF(-3, 5);

            float expected = 8.0f;
            float actual = range.Length;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LengthTest_PositiveNegative()
        {
            Assert.Throws(typeof(System.ArgumentException),
                          new TestDelegate(LengthTest_PositiveNegative_helper));
        }

        private void LengthTest_PositiveNegative_helper()
        {
            RangeF range = new RangeF(5, 3);
            float actual = range.Length;
        }

        [Test]
        public void LengthTest_NegativeNegative()
        {
            RangeF range = new RangeF(-5, -3);

            float expected = 2.0f;
            float actual = range.Length;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CenterTest_BothPositive()
        {
            RangeF range = new RangeF(3, 5);

            float expected = 4.0f;
            float actual = range.Center;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CenterTest_NegativePositive()
        {
            RangeF range = new RangeF(-3, 5);

            float expected = 1.0f;
            float actual = range.Center;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CenterTest_PositiveNegative()
        {
            Assert.Throws(typeof(System.ArgumentException),
                          new TestDelegate(CenterTest_PositiveNegative_helper));
        }

        private void CenterTest_PositiveNegative_helper()
        {
            RangeF range = new RangeF(5, 3);
            float actual = range.Center;
        }

        [Test]
        public void CenterTest_NegativeNegative()
        {
            RangeF range = new RangeF(-5, -3);

            float expected = -4.0f;
            float actual = range.Center;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void UpdateMin_SmallerThan()
        {
            RangeF actual = new RangeF(-5, -3);
            actual.UpdateMin(-7);

            RangeF expected = new RangeF(-7, -3);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void UpdateMin_GreaterThan()
        {
            RangeF actual = new RangeF(-5, -3);
            actual.UpdateMin(+7);

            RangeF expected = new RangeF(-5, -3);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void UpdateMax_SmallerThan()
        {
            RangeF actual = new RangeF(-5, -3);
            actual.UpdateMax(-4);

            RangeF expected = new RangeF(-5, -3);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void UpdateMax_GreaterThan()
        {
            RangeF actual = new RangeF(-5, -3);
            actual.UpdateMax(+7);

            RangeF expected = new RangeF(-5, 7);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void UpdateMin_OriginalNotSet()
        {
            RangeF actual = new RangeF();
            actual.UpdateMin(-7);

            RangeF expected = new RangeF(-7, float.MinValue);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void UpdateMax_OriginalNotSet()
        {
            RangeF actual = new RangeF();
            actual.UpdateMax(-4);

            RangeF expected = new RangeF(float.MaxValue, -4);

            Assert.AreEqual(expected, actual);
        }
    }
}

