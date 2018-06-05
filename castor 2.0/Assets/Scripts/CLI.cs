using UnityEngine;
using Buttons;

public class CLI : MonoBehaviour
{
	private static string experiment_flag = "-experiment";

	private static string reductionSceneName = "reduction";

	private bool CLIUsed = false;

	public ExperimentButton experiment;

	private string[] CLIArguments;

	private void Awake()
	{
		CLIArguments = System.Environment.GetCommandLineArgs();
	}

	private void OnEnable()
	{
		Run();
	}

	public void OnExperimentFinished()
	{
		if (CLIUsed) Application.Quit();
	}

	private void Run()
	{
		int experiment_index = IsCommandlineArgumentPassed(experiment_flag);
		if (experiment_index != -1) RunExperiment(experiment_index);
	}

	private void PrepApplicationForCLI()
	{
		Ticker.Receiver.Instance.ToHeadLessMode();
		CLIUsed = true;
	}

	private int IsCommandlineArgumentPassed(string argument)
	{
		for (int i = 0; i < CLIArguments.Length; i++) if (CLIArguments[i].Equals(argument)) return i;
		return -1;
	}

	private void RunExperiment(int experiment_argument_index)
	{
		string config_file = CLIArguments[experiment_argument_index + 1];
		RunExperiment(config_file);
	}

	private void RunExperiment(string configFile)
	{
		PrepApplicationForCLI();
		experiment.ProcessExperimentConfigurationFile(configFile);
	}
}
