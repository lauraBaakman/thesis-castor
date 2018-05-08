using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests.Extensions
{
	[TestFixture]
	public class ICollectionExtensionTests
	{
		[Test]
		public void Test_IsEmpty_EmptyList()
		{
			ICollection<int> list = new List<int>();

			Assert.IsTrue(list.IsEmpty());
		}

		[Test]
		public void Test_IsEmpty_EmptyList_WithNonZeroCapacity()
		{
			ICollection<int> list = new List<int> { 1 };
			list.Clear();

			Assert.IsTrue(list.IsEmpty());
		}

		[Test]
		public void Test_IsEmpty_NonEmptyList()
		{
			ICollection<int> list = new List<int> { 1, 2, 3 };

			Assert.IsFalse(list.IsEmpty());
		}

	}
}