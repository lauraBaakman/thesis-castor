using NUnit.Framework;
using System.Collections.Generic;

namespace Tests.Extensions
{
    [TestFixture]
    public class IListExtensionTests
    {
        IList<int> thisList;

        [SetUp]
        public void SetUp()
        {
            thisList = new int[] { 1, 2, 3, 4 };
        }

        [Test]
        public void Test_Equals_Equal()
        {
            IList<int> otherList = new int[] { 1, 2, 3, 4 };

            Assert.IsTrue(thisList.OrderedElementsAreEqual(otherList));
            Assert.IsTrue(otherList.OrderedElementsAreEqual(thisList));
            Assert.AreEqual(thisList.OrderedElementsGetHashCode(), otherList.OrderedElementsGetHashCode());
        }

        [Test]
        public void Test_Equals_LengthDifferent_Shorter()
        {
            IList<int> otherList = new int[] { 1, 2, 3 };

            Assert.IsFalse(thisList.OrderedElementsAreEqual(otherList));
            Assert.IsFalse(otherList.OrderedElementsAreEqual(thisList));
            Assert.AreNotEqual(thisList.OrderedElementsGetHashCode(), otherList.OrderedElementsGetHashCode());
        }

        [Test]
        public void Test_Equals_LengthDifferent_Longer()
        {
            IList<int> otherList = new int[] { 1, 2, 3, 4, 5 };

            Assert.IsFalse(thisList.OrderedElementsAreEqual(otherList));
            Assert.IsFalse(otherList.OrderedElementsAreEqual(thisList));
            Assert.AreNotEqual(thisList.OrderedElementsGetHashCode(), otherList.OrderedElementsGetHashCode());
        }

        [Test]
        public void Test_Equals_ContentDifferent_Shuffeled()
        {
            IList<int> otherList = new int[] { 2, 3, 4, 1 };

            Assert.IsFalse(thisList.OrderedElementsAreEqual(otherList));
            Assert.IsFalse(otherList.OrderedElementsAreEqual(thisList));
            Assert.AreNotEqual(thisList.OrderedElementsGetHashCode(), otherList.OrderedElementsGetHashCode());
        }

        [Test]
        public void Test_Equals_ContentDifferent_EmptyIntersection()
        {
            IList<int> otherList = new int[] { 5, 6, 7, 8 };

            Assert.IsFalse(thisList.OrderedElementsAreEqual(otherList));
            Assert.IsFalse(otherList.OrderedElementsAreEqual(thisList));
            Assert.AreNotEqual(thisList.OrderedElementsGetHashCode(), otherList.OrderedElementsGetHashCode());
        }
    }
}