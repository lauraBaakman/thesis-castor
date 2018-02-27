using UnityEngine;
using NUnit.Framework;

namespace Tests
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
    }
}