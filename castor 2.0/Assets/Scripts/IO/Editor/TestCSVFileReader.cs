using NUnit.Framework;
using IO;
using System.Collections.Generic;

namespace Tests.IO
{
	[TestFixture]
	public class CSVFileReaderTests
	{
		const string inputPath_no_comment = "/Users/laura/Repositories/thesis-castor/castor 2.0/Assets/Scripts/IO/Editor/testcsv.csv";
		const string inputPath_with_comment = "/Users/laura/Repositories/thesis-castor/castor 2.0/Assets/Scripts/IO/Editor/testcsv_withcomments.csv";

		[SetUp]
		public void SetUp()
		{

		}

		[TestCase(inputPath_no_comment)]
		[TestCase(inputPath_with_comment)]
		public void Test_Read(string path)
		{
			List<Dictionary<string, object>> expected = new List<Dictionary<string, object>>
			{
				new Dictionary<string, object>{
					{"path", "/Users/laura/Repositories/thesis-experiment/simulated/test_data/6200ea96-2c42-3778-b7a7-4463dd1b508d.obj"},
					{"uuid", "6200ea96-2c42-3778-b7a7-4463dd1b508d"},
					{"translation x", -2.0489096641540527e-07f},
					{"translation y", -1.0058283805847168e-07f},
					{"translation z", -2.0f},
					{"rotation x", -1.5707963705062866f},
					{"rotation y", -1.5707963705062866f},
					{"rotation z", -1.5707963705062866f}
				},
				new Dictionary<string, object>{
					{"path", "/Users/laura/Repositories/thesis-experiment/simulated/test_data/a6aeaeda-90f4-35b5-8e65-5fffe6c8f53c.obj"},
					{"uuid", "a6aeaeda-90f4-35b5-8e65-5fffe6c8f53c"},
					{"translation x", -2.0489096641540527e-07f},
					{"translation y", -1.0058283805847168e-07f},
					{"translation z", -2.0f},
					{"rotation x", -1.5707963705062866f},
					{"rotation y", -1.5707963705062866f},
					{"rotation z", +1.5707963705062866f}
				}
			};
			List<Dictionary<string, object>> actual = new CSVFileReader().Read(path);

			Dictionary<string, object> actualRow, expectedRow;
			for (int i = 0; i < actual.Count; i++)
			{
				actualRow = actual[i];
				expectedRow = expected[i];

				Assert.That(actual, Is.EquivalentTo(expected));
			}
		}
	}
}