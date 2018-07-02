using UnityEngine;
using Buttons;

public class CLI : RTEditor.MonoSingletonBase<CLI>
{
	private static string experimentFlag = "-experiment";
	private static string statisticsFlag = "-statistics";
	private static string realDataFlag = "-real";

	private bool CLIUsed = false;

	private float startTime;

	public ExperimentButton experiment;
	public StatisticsButton statistics;

	private string[] CLIArguments;

	public bool CLIModeActive { get { return CLIUsed; } }

	private void Start()
	{
		CLIArguments = System.Environment.GetCommandLineArgs();
		Run();
	}

	/// <summary>
	/// Once the experiment is finished this method will be called. It prints the
	/// time needed for the experiment and quits the application.
	/// </summary>
	public void OnCommandFinished()
	{
		Quit();
	}

	public void OnEncounteredException()
	{
		Quit();
	}

	private void Quit()
	{
		Debug.Log("OnCommandFinished");
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
	}

	/// <summary>
	/// Check for the different command line options if they have been passed,
	/// and if so handles them.
	/// </summary>
	private void Run()
	{
		string configFile;
		if (ExtractExperimentArgument(out configFile)) RunSimulatedExperiment(configFile);

		string resultsDirectory;
		string dataFile;
		if (ExtractStatisticsArguments(out dataFile, out resultsDirectory)) RunStatisticsComputation(dataFile, resultsDirectory);

		string inputDirectory;
		string outputDirectory;
		string ICPMethod;
		int numIterations;
		float maxWithinCorrespondenceDistance;
		if (ExtractRealDataArguments(
			out inputDirectory, out outputDirectory, out ICPMethod,
			out numIterations, out maxWithinCorrespondenceDistance
		)) RunRealDataExperiment(inputDirectory, outputDirectory, ICPMethod, numIterations, maxWithinCorrespondenceDistance);
	}

	/// <summary>
	/// Extracts the statistics arguments from the command line arguments
	/// </summary>
	/// <returns><c>true</c>, if statistics arguments was extracted, <c>false</c> otherwise.</returns>
	/// <param name="dataSetFile">Data set file.</param>
	/// <param name="resultsDirectory">Results directory.</param>
	private bool ExtractStatisticsArguments(out string dataSetFile, out string resultsDirectory)
	{
		dataSetFile = null;
		resultsDirectory = null;

		int statisticsArgument = GetCLIArgumentIndex(statisticsFlag);
		if (statisticsArgument == -1) return false;

		dataSetFile = CLIArguments[statisticsArgument + 1];
		resultsDirectory = CLIArguments[statisticsArgument + 2];

		return true;
	}

	private bool ExtractRealDataArguments(out string inputDirectory, out string resultsDirectory, out string ICPMethod, out int numIterations, out float maxWithinCorrespondenceDistance)
	{
		inputDirectory = null;
		resultsDirectory = null;
		ICPMethod = null;
		numIterations = -1;
		maxWithinCorrespondenceDistance = 0.0f;

		int statisticsArgument = GetCLIArgumentIndex(realDataFlag);
		if (statisticsArgument == -1) return false;

		inputDirectory = CLIArguments[statisticsArgument + 1];
		resultsDirectory = CLIArguments[statisticsArgument + 2];
		ICPMethod = CLIArguments[statisticsArgument + 3];
		int.TryParse(CLIArguments[statisticsArgument + 4], out numIterations);
		float.TryParse(CLIArguments[statisticsArgument + 5], out maxWithinCorrespondenceDistance);

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
	private void RunSimulatedExperiment(string configFile)
	{
		PrepApplicationForCLI();
		experiment.RunExperimentWithConfigurationFile(configFile);
	}

	/// <summary>
	/// Runs the statistics computation for the passed config and results file.
	/// </summary>
	/// <param name="dataSetFile">Config file.</param>
	/// <param name="resultsDirectory">Results file.</param>
	private void RunStatisticsComputation(string dataSetFile, string resultsDirectory)
	{
		PrepApplicationForCLI();
		statistics.Listener = this.gameObject;
		StartCoroutine(statistics.ProcessExperimentResultsFolder(dataSetFile, resultsDirectory));
	}

	private void RunRealDataExperiment(string inputDirectory, string outputDirectory, string ICPMethod,
									   int maxNumIterations, float maxWithinCorrespondenceDistance)
	{
		PrepApplicationForCLI();
		RealExperimentRunner.Instance.Run(inputDirectory, outputDirectory, ICPMethod, maxNumIterations, maxWithinCorrespondenceDistance);
	}

	/// <summary>
	/// If a commandline option is passed this method makes sure that the other
	/// components of the application behave accordingly.
	/// </summary>
	private void PrepApplicationForCLI()
	{
		CLIUsed = true;

		//Store the current time so that we can show how long the run took.
		startTime = Time.realtimeSinceStartup;
	}
}
