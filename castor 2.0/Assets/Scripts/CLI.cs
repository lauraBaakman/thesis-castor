using UnityEngine;
using Buttons;

public class CLI : RTEditor.MonoSingletonBase<CLI>
{
	private static string experiment_flag = "-experiment";

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
		int experiment_index = IsCommandlineArgumentPassed(experiment_flag);
		if (experiment_index != -1) RunExperiment(experiment_index);
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
	/// Method to check if a specific command line argument is passed. Note that
	///  if a dash is expected in front of the parameter name that dash should 
	/// be included in <param name="argument">.
	/// </summary>
	/// <returns>The commandline argument passed.</returns>
	/// <param name="argument">Argument.</param>
	private int IsCommandlineArgumentPassed(string argument)
	{
		for (int i = 0; i < CLIArguments.Length; i++) if (CLIArguments[i].Equals(argument)) return i;
		return -1;
	}

	/// <summary>
	/// This function handles running the experiment from the command line, it 
	/// reads the configuration file from the command line arguments.
	/// </summary>
	/// <param name="experiment_argument_index">Experiment argument index.</param>
	private void RunExperiment(int experiment_argument_index)
	{
		string config_file = CLIArguments[experiment_argument_index + 1];
		RunExperiment(config_file);
	}

	/// <summary>
	/// This function handles running the experiment from the command line, it 
	/// uses the passed configuration file.
	/// </summary>
	/// <param name="configFile">Config file.</param>
	private void RunExperiment(string configFile)
	{
		PrepApplicationForCLI();
		experiment.ProcessExperimentConfigurationFile(configFile);
	}
}
