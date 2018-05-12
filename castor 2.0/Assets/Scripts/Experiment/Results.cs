using UnityEngine;
using System.Collections;
using System;
using Registration.Messages;
using System.Globalization;
using System.Collections.Generic;

public class Results
{

	private Dictionary<string, Result> results;

	public int ResultCount { get { return results.Count; } }

	public Results()
	{
		results = new Dictionary<string, Result>();
	}

	public static Results FromFile(string path)
	{
		throw new NotImplementedException();
	}

	public void AddResult(ICPTerminatedMessage message)
	{
		Result result = new Result(message);
		results.Add(message.modelFragmentName, result);
	}

	public bool HasResultFor(string staticObjectID)
	{
		return results.ContainsKey(staticObjectID);
	}

	public class Result
	{
		public readonly string terminationMessage;
		public readonly float errorAtTermination;
		public readonly int terminationIteration;
		public readonly string modelFragmentID;

		public Result(ICPTerminatedMessage message)
		{
			this.modelFragmentID = message.modelFragmentName;
			this.terminationMessage = message.Message;
			this.errorAtTermination = message.errorAtTermination;
			this.terminationIteration = message.terminationIteration;
		}

		public string ToCSVLine()
		{
			string line = string.Format(
				"'{0}', {1}, {2}",
				terminationMessage,
				errorAtTermination.ToString("E10", CultureInfo.InvariantCulture),
				terminationIteration
			);
			return line;
		}
	}
}
