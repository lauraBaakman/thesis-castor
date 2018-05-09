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
					{"the string", (object) "hi"},
					{"the int", (object) 5},
					{"the float", 4.5f},
					{"the double", 5.2}
				},
				new Dictionary<string, object>{
					{"the string", (object) "bye"},
					{"the int", (object) 6},
					{"the float", 7.2f},
					{"the double", 9.3}
				}
			};

			CSVWriter writer = new CSVWriter(path);
			writer.Write(expected);

			List<Dictionary<string, object>> actual = new CSVReader().Read(path);

			Dictionary<string, object> actualRow, expectedRow;

			for (int i = 0; i < expected.Count; i++)
			{
				actualRow = actual[i];
				expectedRow = expected[i];

				foreach (string key in expectedRow.Keys)
				{
					Assert.That(actualRow[key].Equals(expectedRow[key]));
				}
			}
		}
	}
}