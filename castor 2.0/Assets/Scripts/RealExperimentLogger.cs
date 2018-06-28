using System.IO;
using System;
using Registration.Messages;

public class RealExperimentLogger : RTEditor.MonoSingletonBase<RealExperimentLogger>, IICPListener, IICPStartEndListener
{
	private string outputPath = null;
	private static string filename = "log.txt";

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

	public void Log(IO.WriteResult result)
	{
		Log(result.Message);
	}

	private string TimeStamp()
	{
		return DateTime.Now.ToLocalTime().ToString();
	}

	public void CreateLogFile(string directory)
	{
		Directory.CreateDirectory(directory);

		this.outputPath = Path.Combine(directory, filename);

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
