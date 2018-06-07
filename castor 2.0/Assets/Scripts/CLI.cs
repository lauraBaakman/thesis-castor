using UnityEngine;
using Buttons;
using System;

public class CLI : RTEditor.MonoSingletonBase<CLI>
{
	private static string experimentFlag = "-experiment";
	private static string statisticsFlag = "-statistics";

	private bool CLIUsed = false;

	private float startTime;

	public ExperimentButton experiment;

	private string[] CLIArguments;

	public bool CLIModeActive { get { return CLIUsed; } }

	private void Awake()
	{
		CLIArguments = System.Environment.GetCommandLineArgs();
	}

	private void OnEnable()
	{
		Run();
	}

	/// <summary>
	/// Once the experiment is finished this method will be called. It prints the 
	/// time needed for the experiment and quits the application.
	/// </summary>
	public void OnExperimentFinished()
	{
		if (CLIUsed)
		{
			Debug.Log(string.Format("Needed {0} seconds", Time.realtimeSinceStartup - startTime));
			Application.Quit();
		}
	}

	/// <summary>
	/// Check for the different command line options if they have been passed, 
	/// and if so handles them.
	/// </summary>
	private void Run()
	{
		string configFile;
		if (ExtractExperimentArgument(out configFile)) RunExperiment(configFile);

		string resultsFile;
		if (ExtractStatisticsArguments(out configFile, out resultsFile)) RunStatisticsComputation(configFile, resultsFile);

	}

	private void RunStatisticsComputation(string configFile, string resultsFile)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// If a commandline option is passed this method makes sure that the other 
	/// components of the application behave accordingly.
	/// </summary>
	private void PrepApplicationForCLI()
	{
		Ticker.Receiver.Instance.ToHeadLessMode();

		CLIUsed = true;

		//Store the current time so that we can show how long the run took.
		startTime = Time.realtimeSinceStartup;
	}

	/// <summary>
	/// Extracts the statistics arguments from the command line arguments
	/// </summary>
	/// <returns><c>true</c>, if statistics arguments was extracted, <c>false</c> otherwise.</returns>
	/// <param name="configFile">Config file.</param>
	/// <param name="resultsFile">Results file.</param>
	private bool ExtractStatisticsArguments(out string configFile, out string resultsFile)
	{
		configFile = null;
		resultsFile = null;

		int statisticsArgument = GetCLIArgumentIndex(statisticsFlag);
		if (statisticsArgument == -1) return false;

		configFile = CLIArguments[statisticsArgument + 1];
		resultsFile = CLIArguments[statisticsArgument + 2];

		return true;
	}

	/// <summary>
	/// Extracts the experiment argument, i.e. the config file, from the command line arguments and stores it in confgiFile.
	/// </summary>
	/// <returns><c>true</c>, if experiment argument was extracted, <c>false</c> otherwise.</returns>
	/// <param name="configFile">The path of the config file.</param>
	private bool ExtractExperimentArgument(out string configFile)
	{
		configFile = null;

		int experimentIndex = GetCLIArgumentIndex(experimentFlag);
		if (experimentIndex == -1) return false;

		configFile = CLIArguments[experimentIndex + 1];
		return true;
	}

	/// <summary>
	/// Method to check if a specific command line argument is passed. Note that
	///  if a dash is expected in front of the parameter name that dash should 
	/// be included in <param name="argument">.
	/// </summary>
	/// <returns>The commandline argument passed.</returns>
	/// <param name="argument">Argument.</param>
	private int GetCLIArgumentIndex(string argument)
	{
		for (int i = 0; i < CLIArguments.Length; i++) if (CLIArguments[i].Equals(argument)) return i;
		return -1;
	}

	/// <summary>
	/// This function handles running the experiment from the command line, it 
	/// uses the passed configuration file.
	/// </summary>
	/// <param name="configFile">Config file.</param>
	private void RunExperiment(string configFile)
	{
		PrepApplicationForCLI();
		experiment.RunExperimentWithConfigurationFile(configFile);
	}
}
