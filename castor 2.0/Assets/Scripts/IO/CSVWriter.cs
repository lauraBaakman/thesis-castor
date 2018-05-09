using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class CSVWriter
{
	private List<string> columnNames;
	private readonly StreamWriter writer;

	public CSVWriter(string outputPath)
	{
		this.writer = new StreamWriter(outputPath);
	}

	public void WriteHeader(List<string> columnNames)
	{
		throw new NotImplementedException();
	}

	public void WriteRow(Dictionary<string, object> rowData)
	{
		throw new NotImplementedException();
	}

	public void Write(List<Dictionary<string, object>> data)
	{
		this.columnNames = (List<string>)data[0].Keys.AsEnumerable<string>();

		WriteHeader(columnNames);

		foreach (Dictionary<string, object> row in data) WriteRow(row);

		writer.Dispose();
	}
}
