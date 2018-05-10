using NUnit.Framework;
using System.Collections.Generic;
using IO;
using System.IO;

namespace Tests.IO
{
	[TestFixture]
	public class CSVVWriterTests
	{
		private string path;

		[SetUp]
		public void SetUp()
		{
			path = Path.GetTempFileName();
		}

		[TearDown]
		public void Dispose()
		{
			File.Delete(path);
		}

		[Test]
		public void Test_Write()
		{
			List<Dictionary<string, object>> expected = new List<Dictionary<string, object>>
			{
				new Dictionary<string, object>{
					{"the string", "hi"},
					{"the int", 5},
					{"the float", 4.5643f},
					{"the double", 5.2},
				},
				new Dictionary<string, object>{
					{"the string", "bye"},
					{"the int", 7},
					{"the float", 9.34232f},
					{"the double", 7.243243},
				},
			};

			string path = Path.GetTempFileName();

			CSVWriter writer = new CSVWriter(path);
			writer.Write(expected);

			List<Dictionary<string, object>> actual = new CSVReader().Read(path);

			Dictionary<string, object> actualRow, expectedRow;

			for (int i = 0; i < expected.Count; i++)
			{
				actualRow = actual[i];
				expectedRow = expected[i];

				foreach (string key in expectedRow.Keys) Assert.That(actualRow[key], Is.EqualTo(expectedRow[key]).Within(0.001f));
				foreach (string key in actualRow.Keys) Assert.AreEqual(actualRow[key], expectedRow[key]);
			}
		}
	}
}