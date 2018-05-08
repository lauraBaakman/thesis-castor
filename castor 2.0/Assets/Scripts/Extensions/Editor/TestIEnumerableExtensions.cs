using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests.Extensions
{
	[TestFixture]
	public class IEnumerableExtensionTests
	{
		[Test]
		public void TestUnorderedElementsEqual_EmptyIEnumerables()
		{
			IEnumerable<DummyObject> thisList = new List<DummyObject>();
			IEnumerable<DummyObject> otherList = new List<DummyObject>();

			Assert.IsTrue(thisList.UnorderedElementsAreEqual(otherList));
			Assert.AreEqual(thisList.UnorderedElementsGetHashCode(), otherList.UnorderedElementsGetHashCode());
		}

		[Test]
		public void TestUnorderedElementsEqual_EqualLists()
		{
			DummyObject a = DummyObject.Random();
			DummyObject b = DummyObject.Random();
			DummyObject c = DummyObject.Random();

			IEnumerable<DummyObject> thisList = new List<DummyObject> { a, b, c };
			IEnumerable<DummyObject> otherList = new List<DummyObject> { a, b, c };

			Assert.IsTrue(thisList.UnorderedElementsAreEqual(otherList));
			Assert.AreEqual(thisList.UnorderedElementsGetHashCode(), otherList.UnorderedElementsGetHashCode());
		}

		[Test]
		public void TestUnorderedElementsEqual_EqualListsDifferentOrder()
		{
			DummyObject a = DummyObject.Random();
			DummyObject b = DummyObject.Random();
			DummyObject c = DummyObject.Random();

			IEnumerable<DummyObject> thisList = new List<DummyObject> { a, b, c };
			IEnumerable<DummyObject> otherList = new List<DummyObject> { c, a, b };

			Assert.IsTrue(thisList.UnorderedElementsAreEqual(otherList));
			Assert.AreEqual(thisList.UnorderedElementsGetHashCode(), otherList.UnorderedElementsGetHashCode());
		}

		[Test]
		public void TestUnorderedElementsEqual_EqualListsDifferentListsSameLength()
		{
			DummyObject a = DummyObject.Random();
			DummyObject b = DummyObject.Random();

			IEnumerable<DummyObject> thisList = new List<DummyObject> { a, b, DummyObject.Random() };
			IEnumerable<DummyObject> otherList = new List<DummyObject> { a, b, DummyObject.Random() };

			Assert.IsFalse(thisList.UnorderedElementsAreEqual(otherList));
			Assert.AreNotEqual(thisList.UnorderedElementsGetHashCode(), otherList.UnorderedElementsGetHashCode());
		}

		[Test]
		public void TestUnorderedElementsEqual_EqualListsDifferentListsDifferentLength()
		{
			DummyObject a = DummyObject.Random();
			DummyObject b = DummyObject.Random();
			DummyObject c = DummyObject.Random();

			IEnumerable<DummyObject> thisList = new List<DummyObject> { a, b, c, DummyObject.Random() };
			IEnumerable<DummyObject> otherList = new List<DummyObject> { a, b, c };

			Assert.IsFalse(thisList.UnorderedElementsAreEqual(otherList, new DummyObject.Comparer()));
			Assert.AreNotEqual(thisList.UnorderedElementsGetHashCode(), otherList.UnorderedElementsGetHashCode());
		}

		[Test]
		public void TestUnorderedElementsEqualWithComparer_EmptyLists()
		{
			IEnumerable<DummyObject> thisList = new List<DummyObject>();
			IEnumerable<DummyObject> otherList = new List<DummyObject>();

			Assert.IsTrue(thisList.UnorderedElementsAreEqual(otherList, new DummyObject.Comparer()));
			Assert.AreEqual(
				thisList.UnorderedElementsGetHashCode(new DummyObject.Comparer()),
				otherList.UnorderedElementsGetHashCode(new DummyObject.Comparer())
			);
		}

		[Test]
		public void TestUnorderedElementsEqualWithComparer_EqualLists()
		{
			DummyObject a = DummyObject.Random();
			DummyObject b = DummyObject.Random();
			DummyObject c = DummyObject.Random();

			IEnumerable<DummyObject> thisList = new List<DummyObject> { a, b, c };
			IEnumerable<DummyObject> otherList = new List<DummyObject> { a, b, c };

			Assert.IsTrue(thisList.UnorderedElementsAreEqual(otherList, new DummyObject.Comparer()));
			Assert.AreEqual(
				thisList.UnorderedElementsGetHashCode(new DummyObject.Comparer()),
				otherList.UnorderedElementsGetHashCode(new DummyObject.Comparer())
			);
		}

		[Test]
		public void TestUnorderedElementsEqualWithComparer_EqualListsDifferentOrder()
		{
			DummyObject a = DummyObject.Random();
			DummyObject b = DummyObject.Random();
			DummyObject c = DummyObject.Random();

			IEnumerable<DummyObject> thisList = new List<DummyObject> { a, b, c };
			IEnumerable<DummyObject> otherList = new List<DummyObject> { c, a, b };

			Assert.IsTrue(thisList.UnorderedElementsAreEqual(otherList, new DummyObject.Comparer()));
			Assert.AreEqual(
				thisList.UnorderedElementsGetHashCode(new DummyObject.Comparer()),
				otherList.UnorderedElementsGetHashCode(new DummyObject.Comparer())
			);
		}

		[Test]
		public void TestUnorderedElementsEqualWithComparer_EqualListsDifferentListsSameLength()
		{
			DummyObject a = DummyObject.Random();
			DummyObject b = DummyObject.Random();

			IEnumerable<DummyObject> thisList = new List<DummyObject> { a, b, DummyObject.Random() };
			IEnumerable<DummyObject> otherList = new List<DummyObject> { a, b, DummyObject.Random() };

			Assert.IsFalse(thisList.UnorderedElementsAreEqual(otherList, new DummyObject.Comparer()));
			Assert.AreNotEqual(
				thisList.UnorderedElementsGetHashCode(new DummyObject.Comparer()),
				otherList.UnorderedElementsGetHashCode(new DummyObject.Comparer())
			);
		}

		[Test]
		public void TestUnorderedElementsEqualWithComparer_EqualListsDifferentListsDifferentLength()
		{
			DummyObject a = DummyObject.Random();
			DummyObject b = DummyObject.Random();
			DummyObject c = DummyObject.Random();

			IEnumerable<DummyObject> thisList = new List<DummyObject> { a, b, c, DummyObject.Random() };
			IEnumerable<DummyObject> otherList = new List<DummyObject> { a, b, c };

			Assert.IsFalse(thisList.UnorderedElementsAreEqual(otherList, new DummyObject.Comparer()));
			Assert.AreNotEqual(
				thisList.UnorderedElementsGetHashCode(new DummyObject.Comparer()),
				otherList.UnorderedElementsGetHashCode(new DummyObject.Comparer())
			);
		}

		[Test]
		public void TestOorderedElementsEqual_EmptyIEnumerables()
		{
			IEnumerable<DummyObject> thisList = new List<DummyObject>();
			IEnumerable<DummyObject> otherList = new List<DummyObject>();

			Assert.IsTrue(thisList.OrderedElementsAreEqual(otherList));
			Assert.AreEqual(thisList.OrderedElementsGetHashCode(), otherList.OrderedElementsGetHashCode());
		}

		[Test]
		public void TestOrderedElementsEqual_EqualLists()
		{
			DummyObject a = DummyObject.Random();
			DummyObject b = DummyObject.Random();
			DummyObject c = DummyObject.Random();

			IEnumerable<DummyObject> thisList = new List<DummyObject> { a, b, c };
			IEnumerable<DummyObject> otherList = new List<DummyObject> { a, b, c };

			Assert.IsTrue(thisList.OrderedElementsAreEqual(otherList));
			Assert.AreEqual(thisList.OrderedElementsGetHashCode(), otherList.OrderedElementsGetHashCode());
		}

		[Test]
		public void TestOrderedElementsEqual_NotEqualListsDifferentOrder()
		{
			DummyObject a = DummyObject.Random();
			DummyObject b = DummyObject.Random();
			DummyObject c = DummyObject.Random();

			IEnumerable<DummyObject> thisList = new List<DummyObject> { a, b, c };
			IEnumerable<DummyObject> otherList = new List<DummyObject> { c, a, b };

			Assert.IsFalse(thisList.OrderedElementsAreEqual(otherList));
			Assert.AreNotEqual(thisList.OrderedElementsGetHashCode(), otherList.OrderedElementsGetHashCode());
		}

		[Test]
		public void TestOrderedElementsEqual_NotEqualListsDifferentListsSameLength()
		{
			DummyObject a = DummyObject.Random();
			DummyObject b = DummyObject.Random();

			IEnumerable<DummyObject> thisList = new List<DummyObject> { a, b, DummyObject.Random() };
			IEnumerable<DummyObject> otherList = new List<DummyObject> { a, b, DummyObject.Random() };

			Assert.IsFalse(thisList.OrderedElementsAreEqual(otherList));
			Assert.AreNotEqual(thisList.OrderedElementsGetHashCode(), otherList.OrderedElementsGetHashCode());
		}

		[Test]
		public void TestOrderedElementsEqual_NotEqualListsDifferentListsDifferentLength()
		{
			DummyObject a = DummyObject.Random();
			DummyObject b = DummyObject.Random();
			DummyObject c = DummyObject.Random();

			IEnumerable<DummyObject> thisList = new List<DummyObject> { a, b, c, DummyObject.Random() };
			IEnumerable<DummyObject> otherList = new List<DummyObject> { a, b, c };

			Assert.IsFalse(thisList.OrderedElementsAreEqual(otherList));
			Assert.AreNotEqual(thisList.OrderedElementsGetHashCode(), otherList.OrderedElementsGetHashCode());
		}
	}

	internal class DummyObject : IEquatable<DummyObject>
	{
		private static System.Random random = new System.Random();

		private readonly int member;

		internal DummyObject(int property)
		{
			this.member = property;
		}

		internal static DummyObject Random()
		{
			return new DummyObject(random.Next());
		}

		public bool Equals(DummyObject other)
		{
			if (other == null) return false;

			return this.member.Equals(other.member);
		}

		internal class Comparer : IEqualityComparer<DummyObject>
		{
			public bool Equals(DummyObject x, DummyObject y)
			{
				if (x == null && y == null) return true;
				if (x == null || y == null) return false;

				return x.member.Equals(y.member);
			}

			public int GetHashCode(DummyObject obj)
			{
				if (obj == null) return 0;

				return obj.member.GetHashCode();
			}
		}
	}
}