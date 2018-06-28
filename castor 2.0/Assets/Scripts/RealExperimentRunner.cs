using Registration.Messages;
using UnityEngine;
using System;
using IO;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class RealExperimentRunner : RTEditor.MonoSingletonBase<RealExperimentRunner>, IICPStartEndListener
{
	public GameObject FragmentsRoot;

	private bool continueCoroutine = false;

	private string inputDirectory;

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
		this.OutputDirectory = outputDirectory;
		this.inputDirectory = inputDirectory;
		this.exporter = new FragmentsExporter(FragmentsRoot, WroteCurrentConfigurationToFile);

		RealExperimentLogger.Instance.CreateLogFile(outputDirectory);

		ProcessFragmentsAndStartRegistration();
	}

	private void WroteCurrentConfigurationToFile(WriteResult result)
	{
		RegisterNextPair();
	}

	private void ProcessFragmentsAndStartRegistration()
	{
		StartCoroutine(ImportFragments((finished) =>
		{
			if (finished)
			{
				CreateFragmentPairs();
				ExportFragments();
				RegisterNextPair();
			}
		}));
	}

	private IEnumerator ImportFragments(System.Action<bool> callback)
	{
		string[] objFiles = Directory.GetFiles(inputDirectory, "*.obj");
		yield return null;
		callback(false);

		continueCoroutine = false;
		IO.FragmentImporter importer = new IO.FragmentImporter(FragmentsRoot, ImportedFragment);
		foreach (string currentFile in objFiles)
		{
			importer.Import(currentFile);
			yield return new WaitUntil(() => this.continueCoroutine);
			callback(false);
		}
		continueCoroutine = false;

		yield return null;
		callback(true);
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
		List<GameObject> fragments = GetAllFragments();
		foreach (GameObject fragment in fragments)
		{
			Debug.Log(fragment.name);
		}
	}

	private List<GameObject> GetAllFragments()
	{
		List<GameObject> fragments = new List<GameObject>();
		foreach (Transform child in FragmentsRoot.transform)
			//Add only fragments, not the other children of 'Fragments'
			if (child.GetComponent<MeshFilter>() != null) fragments.Add(child.gameObject);

		return fragments;
	}

	private void RegisterNextPair()
	{
		throw new NotImplementedException();
	}

	private void ExportFragments()
	{
		this.exporter.ExportFragments(
		  directory: Path.Combine(OutputDirectory, counter.ToString())
		);
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
