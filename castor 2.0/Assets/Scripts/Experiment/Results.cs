using UnityEngine;
using System.Collections;
using System;
using Registration.Messages;
using System.Globalization;
using System.Collections.Generic;

public class Results
{

	private List<string> processedModelFragments;

	public int Count { get { return processedModelFragments.Count; } }

	public Results()
	{
		processedModelFragments = new List<string>();
	}

	public static Results FromFile(string path)
	{
		Results results = new Results();
		List<Dictionary<string, object>> csvData = new IO.CSVReader().Read(path);
		foreach (Dictionary<string, object> row in csvData) results.AddResult(row);

		return results;
	}

	private void AddResult(Dictionary<string, object> csvrow)
	{
		processedModelFragments.Add((string)csvrow["'id'"]);
	}

	public void AddResult(ICPTerminatedMessage message)
	{
		processedModelFragments.Add(message.modelFragmentName);
	}

	public bool HasResultFor(string staticObjectID)
	{
		return processedModelFragments.Contains(staticObjectID);
	}
}
