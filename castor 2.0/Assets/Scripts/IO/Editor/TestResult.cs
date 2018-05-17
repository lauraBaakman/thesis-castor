using NUnit.Framework;
using UnityEngine;
using IO;

namespace Tests.IO
{
	[TestFixture]
	public class ReadResultTests
	{

		[Test, TestCaseSource("HasPivotCases")]
		public void TestHasPivot(ReadResult readResult, bool expected)
		{
			bool actual = readResult.HasPivot;
			Assert.AreEqual(expected, actual);
		}

		private static object[] HasPivotCases =
		{
			new object[] {
				ReadResult.OKResult("some path", new Mesh(), new Vector3(1, 2, 3)),
				true
			},
			new object[] {
				ReadResult.OKResult("some path", new Mesh()),
				false
			}
		};
	}
}