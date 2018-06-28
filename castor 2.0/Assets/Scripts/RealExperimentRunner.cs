using Registration.Messages;
using UnityEngine;
using System;
using IO;
using System.Collections;
using System.IO;

public class RealExperimentRunner : RTEditor.MonoSingletonBase<RealExperimentRunner>, IICPStartEndListener
{
	public GameObject FragmentsRoot;

	private bool continueCoroutine = false;

	private string outputDirectory;
	public string OutputDirectory
	{
		get { return outputDirectory; }
		set
		{
			Directory.CreateDirectory(value);
			this.outputDirectory = value;
		}
	}

	private FragmentsExporter exporter;

	private int counter = 0;

	IEnumerator Start()
	{
		yield return new WaitForSeconds(3);
		Run(
			inputDirectory: "/Users/laura/Repositories/thesis-experiment/real/_3_subsampled/test",
			outputDirectory: "/Users/laura/Repositories/thesis-experiment/real/results",
			ICPMethod: "Horn"
		);
	}

	public void Run(string inputDirectory, string outputDirectory, string ICPMethod)
	{
		Debug.Log("Run!");

		this.OutputDirectory = outputDirectory;
		this.exporter = new FragmentsExporter(FragmentsRoot, WroteCurrentConfigurationToFile);

		RealExperimentLogger.Instance.CreateLogFile(outputDirectory);

		LoadFragments(inputDirectory);

		//GatherFragments();

		//this.exporter.ExportFragments(
		//	directory: Path.Combine(OutputDirectory, counter.ToString())
		//);
	}

	private void WroteCurrentConfigurationToFile(WriteResult result)
	{
		RegisterNextPair();
	}

	private void LoadFragments(string inputDirectory)
	{
		string[] objFiles = Directory.GetFiles(inputDirectory, "*.obj");
		StartCoroutine(ImportFragments(objFiles));
	}

	private IEnumerator ImportFragments(string[] objFiles)
	{
		continueCoroutine = false;
		IO.FragmentImporter importer = new IO.FragmentImporter(FragmentsRoot, ImportedFragment);
		foreach (string currentFile in objFiles)
		{
			importer.Import(currentFile);
			yield return new WaitUntil(() => this.continueCoroutine);
		}
		continueCoroutine = false;
	}

	private void ImportedFragment(IO.ReadResult result)
	{
		Ticker.Receiver.Instance.SendMessage(
			methodName: "OnMessage",
			value: result.ToTickerMessage(),
			options: SendMessageOptions.RequireReceiver
		);
		RealExperimentLogger.Instance.Log(result);
		continueCoroutine = true;
	}

	private void CreateFragmentPairs()
	{
		throw new NotImplementedException();
	}

	private void RegisterNextPair()
	{
		throw new NotImplementedException();
	}

	private void Terminate()
	{
		throw new NotImplementedException();
	}

	public void OnICPStarted(ICPStartedMessage message)
	{
		throw new NotImplementedException();
	}

	public void OnICPTerminated(ICPTerminatedMessage message)
	{
		throw new NotImplementedException();
	}
}
