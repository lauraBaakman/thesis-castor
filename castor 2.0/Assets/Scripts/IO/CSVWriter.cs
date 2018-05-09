using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class CSVWriter
{
	private Dictionary<string, object>.KeyCollection columnNames;
	private readonly StreamWriter writer;

	public CSVWriter(string outputPath)
	{
		this.writer = new StreamWriter(outputPath);
	}

	public void WriteHeader(Dictionary<string, object>.KeyCollection columnNames)
	{
		throw new NotImplementedException();
	}

	public void WriteRow(Dictionary<string, object> rowData)
	{
		throw new NotImplementedException();
	}

	public void Write(List<Dictionary<string, object>> data)
	{
		this.columnNames = data[0].Keys;

		WriteHeader(columnNames);

		foreach (Dictionary<string, object> row in data) WriteRow(row);

		writer.Dispose();
	}
}
