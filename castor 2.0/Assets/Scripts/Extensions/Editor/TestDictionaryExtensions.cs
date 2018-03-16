using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests.Extensions
{
    [TestFixture]
    public class DictionaryExtensionTests
    {
        Dictionary<int, string> thisDictionary;

        [SetUp]
        public void SetUp()
        {
            thisDictionary = new Dictionary<int, string>();
            thisDictionary.Add(1, "one");
            thisDictionary.Add(2, "two");
            thisDictionary.Add(3, "three");
        }

        [Test]
        public void Test_UnorderedElementsGetHashCode_Equals()
        {
            Dictionary<int, string> otherDictionary = new Dictionary<int, string>();
            otherDictionary.Add(1, "one");
            otherDictionary.Add(2, "two");
            otherDictionary.Add(3, "three");

            Assert.AreEqual(thisDictionary.UnorderedElementsGetHashCode(), otherDictionary.UnorderedElementsGetHashCode());
        }

        [Test]
        public void Test_UnorderedElementsGetHashCode_EqualDifferentOrder()
        {
            Dictionary<int, string> otherDictionary = new Dictionary<int, string>();
            otherDictionary.Add(1, "one");
            otherDictionary.Add(3, "three");
            otherDictionary.Add(2, "two");

            Assert.AreEqual(thisDictionary.UnorderedElementsGetHashCode(), otherDictionary.UnorderedElementsGetHashCode());
        }

        [Test]
        public void Test_UnorderedElementsGetHashCode_NotEqualDifferentKeysSameValues()
        {
            Dictionary<int, string> otherDictionary = new Dictionary<int, string>();
            otherDictionary.Add(0, "one");
            otherDictionary.Add(2, "two");
            otherDictionary.Add(3, "three");

            Assert.AreNotEqual(thisDictionary.UnorderedElementsGetHashCode(), otherDictionary.UnorderedElementsGetHashCode());
        }

        [Test]
        public void Test_UnorderedElementsGetHashCode_NotEqualDifferentValuesSameKeys()
        {
            Dictionary<int, string> otherDictionary = new Dictionary<int, string>();
            otherDictionary.Add(1, "two");
            otherDictionary.Add(2, "two");
            otherDictionary.Add(3, "three");

            Assert.AreNotEqual(thisDictionary.UnorderedElementsGetHashCode(), otherDictionary.UnorderedElementsGetHashCode());
        }
    }
}