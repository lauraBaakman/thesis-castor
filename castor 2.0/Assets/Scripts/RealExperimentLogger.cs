using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEditor.MemoryProfiler;

public class RealExperimentLogger : RTEditor.MonoSingletonBase<RealExperimentLogger>
{
	private string outputPath;

	private static string outputDirectory = "/Users/laura/Repositories/thesis-experiment/real/metadata";

	private void Start()
	{
		CheckIfOutputDirectoryExists();
	}

	private void CheckIfOutputDirectoryExists()
	{
		if (!Directory.Exists(outputDirectory))
			throw new Exception(string.Format("The directory {0} does not exist.", outputDirectory));
	}

	public void Log(string message)
	{
		using (StreamWriter writer = new StreamWriter(this.outputPath, append: true))
		{
			writer.WriteLine(TimeStamp() + " -- " + message);
		}
	}

	private string TimeStamp()
	{
		return DateTime.Now.ToLocalTime().ToString();
	}

	public void SetInputDirectory(string path)
	{
		//GetFileName returns the last component of the path
		string directoryName = Path.GetFileName(path);

		var timeSpan = (DateTime.Now.ToLocalTime() - new DateTime(1970, 1, 1, 0, 0, 0));

		this.outputPath = Path.Combine(
			outputDirectory,
			string.Format("{0}_{1}.txt",
						  directoryName,
						  timeSpan.TotalSeconds.ToString())
		);
		Log("Created File");
	}
}
