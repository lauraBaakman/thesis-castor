using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEditor.MemoryProfiler;
using Registration.Messages;

public class RealExperimentLogger : RTEditor.MonoSingletonBase<RealExperimentLogger>, IICPListener, IICPStartEndListener
{
	private string outputPath = null;

	private static string outputDirectory = "/Users/laura/Repositories/thesis-experiment/real/metadata";

	private void CheckIfOutputDirectoryExists()
	{
		if (!Directory.Exists(outputDirectory))
			throw new Exception(string.Format("The directory {0} does not exist.", outputDirectory));
	}

	public void Log(string message)
	{
		if (outputPath == null) return;
		using (StreamWriter writer = new StreamWriter(this.outputPath, append: true))
		{
			writer.WriteLine(TimeStamp() + " -- " + message);
		}
	}

	public void Log(IO.ReadResult result)
	{
		Log(result.Message);
	}

	private string TimeStamp()
	{
		return DateTime.Now.ToLocalTime().ToString();
	}

	public void SetInputDirectory(string path)
	{
		//No need to do this check if the logger is not used
		CheckIfOutputDirectoryExists();

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

	public void OnPreparationStepCompleted(ICPPreparationStepCompletedMessage message)
	{
		throw new NotImplementedException();
	}

	public void OnStepCompleted(ICPStepCompletedMessage message)
	{
		throw new NotImplementedException();
	}

	public void OnICPTerminated(ICPTerminatedMessage message)
	{
		throw new NotImplementedException();
	}

	public void OnICPStarted(ICPStartedMessage message)
	{
		throw new NotImplementedException();
	}
}
