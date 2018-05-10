using System;
using System.Collections.Generic;
using System.IO;

public class CSVWriter
{
	private List<string> columnNames;
	private string outputPath;
	private StreamWriter writer;

	public CSVWriter(string outputPath)
	{
		this.outputPath = outputPath;
	}

	private void WriteTimeStamp()
	{
		string timestamp = string.Format(
			"# Written by CAstOR on {0}",
			System.DateTime.Now.ToLocalTime().ToString()
		);
		WriteLine(timestamp);
	}

	public void WriteHeader(List<string> columnNames)
	{
		string header = columnNames[0];
		for (int i = 1; i < columnNames.Count; i++)
		{
			header = string.Format("{0}, {1}", header, columnNames[i]);
		}
		WriteLine(header);
	}

	private void WriteLine(string line)
	{
		writer.WriteLine(line);
	}

	public void WriteRow(Dictionary<string, object> rowData)
	{
		string row = Convert.ToString(rowData[columnNames[0]]);

		object value;
		for (int i = 1; i < columnNames.Count; i++)
		{
			value = rowData[columnNames[i]];
			row = string.Format("{0}, {1}", row, Convert.ToString(value));
		}
		WriteLine(row);
	}

	public void Write(List<Dictionary<string, object>> data)
	{
		this.columnNames = new List<string>(data[0].Keys);

		using (this.writer = new StreamWriter(this.outputPath))
		{
			WriteTimeStamp();
			WriteHeader(columnNames);
			foreach (Dictionary<string, object> row in data)
				WriteRow(row);
		}
	}
}
