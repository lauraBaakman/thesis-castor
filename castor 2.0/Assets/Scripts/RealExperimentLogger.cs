using System.IO;
using System;
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

	public void OnICPStarted(ICPStartedMessage message)
	{
		this.Log("Started ICP");
		this.Log("Model Fragment: " + message.ModelFragment);
		this.Log("Static Fragment: " + message.StaticFrament);
		this.Log("Initial Error: " + message.InitialError);
		this.Log("Termination Threshold: " + message.TerminationThreshold);
	}

	public void OnPreparationStepCompleted(ICPPreparationStepCompletedMessage message)
	{
		this.LogIteration(
			message.IterationIndex,
			string.Format(
				"{0} correspondences",
				message.Correspondences.Count
			)
		);
	}

	private void LogIteration(int iteration, string message)
	{
		this.Log(
			string.Format(
				"Iteration {0}: {1}",
				iteration, message
			)
		);
	}

	public void OnStepCompleted(ICPStepCompletedMessage message)
	{
		this.LogIteration(
			message.iteration,
			string.Format(
				"Error: {0}",
				message.Error
			)
		);
	}

	public void OnICPTerminated(ICPTerminatedMessage message)
	{
		this.LogIteration(
			message.terminationIteration,
			"terminated: " + message.Message
		);
		this.Log("Termination error: " + message.errorAtTermination);
	}
}
