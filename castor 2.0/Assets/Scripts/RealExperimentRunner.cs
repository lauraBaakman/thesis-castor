using Registration.Messages;
using UnityEngine;
using System;
using IO;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using Utils;
using Registration;

public class RealExperimentRunner : RTEditor.MonoSingletonBase<RealExperimentRunner>, IICPStartEndListener
{
	public GameObject FragmentsRoot;

	private bool continueCoroutine = false;

	private string inputDirectory;

	//Maximum number of attempts to register two fragments to each other.
	private static int maxAttempts = 3;

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

	private RegistrationPair currentPair;

	private FragmentsExporter exporter;

	private Settings settings;

	private Queue<RegistrationPair> pairs = new Queue<RegistrationPair>();

	private int counter = 0;

	public void Run(string inputDirectory, string outputDirectory, string ICPMethod)
	{
		this.OutputDirectory = outputDirectory;
		this.inputDirectory = inputDirectory;
		this.exporter = new FragmentsExporter(
			FragmentsRoot,
			fragmentCallBack: DoNothing,
			allCallBack: WroteCurrentRegistrationToFile
		);
		this.settings = Settings.SettingsFromName(
			name: ICPMethod,
			referenceTransform: FragmentsRoot.transform,
			sampler: "ndosubsampling"
		);

		RealExperimentLogger.Instance.CreateLogFile(outputDirectory);
		RealExperimentLogger.Instance.Log("Settings: " + settings.ToJson());

		ProcessFragmentsAndStartRegistration();
	}

	private void DoNothing(object something) { }

	private void ProcessFragmentsAndStartRegistration()
	{
		StartCoroutine(ImportFragments((finished) =>
		{
			if (finished)
			{
				CreateFragmentPairs();
				ExportFragments();
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
			importer.Import(currentFile,
							prefabPath: "ExperimentFragment");
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
		for (int first = 0; first < fragments.Count; first++)
		{
			for (int second = 0; second < fragments.Count; second++)
			{
				if (first == second) continue;

				pairs.Enqueue(
					new RegistrationPair(
						first: fragments[first],
						second: fragments[second]
					)
				);
			}
		}
	}

	private List<GameObject> GetAllFragments()
	{
		List<GameObject> fragments = new List<GameObject>();
		foreach (Transform child in FragmentsRoot.transform)
			//Add only fragments, not the other children of 'Fragments'
			if (child.GetComponent<MeshFilter>() != null) fragments.Add(child.gameObject);

		fragments.Sort(new SortFragmentOnVertexCount());

		return fragments;
	}

	private IEnumerator RegisterNextPair()
	{
		if (this.pairs.Count == 0) Terminate();

		try
		{
			this.currentPair = this.pairs.Dequeue();
			this.currentPair.AttemptedRegistration();
		}
		catch (InvalidOperationException)
		{
			//No more pairs to consider
			Terminate();
		}

		Debug.Log(
			string.Format(
				"{0} registering {1} to {2}",
				DateTime.Now.ToString(),
				this.currentPair.ModelFragment.name,
				this.currentPair.StaticFragment.name
			)
		);

		ICPRegisterer registerer = new ICPRegisterer(
			staticFragment: this.currentPair.StaticFragment,
			modelFragment: this.currentPair.ModelFragment,
			settings: this.settings
		);
		registerer.AddListener(this.gameObject);
		yield return null;

		while (!registerer.HasTerminated)
		{
			registerer.PrepareStep();
			yield return null;

			registerer.Step();
			yield return null;
		}
	}

	private void ExportFragments()
	{
		this.exporter.ExportFragments(
		  directory: Path.Combine(OutputDirectory, counter.ToString())
		);
		counter++;
	}

	private void WroteCurrentRegistrationToFile()
	{
		StartCoroutine(RegisterNextPair());
	}

	private void Terminate()
	{
		CLI.Instance.OnCommandFinished();
	}

	public void OnICPStarted(ICPStartedMessage message) { }

	public void OnICPTerminated(ICPTerminatedMessage message)
	{
		if (currentPair.AttemptedRegistrationsCount < maxAttempts)
		{
			this.pairs.Enqueue(this.currentPair);
		}
		ExportFragments();
	}
}

public class RegistrationPair : Pair<GameObject, GameObject>
{
	private int attemptedRegistrationsCount = 0;

	public RegistrationPair(GameObject first, GameObject second)
		: base(first, second)
	{ }

	public GameObject StaticFragment { get { return this.First; } }

	public GameObject ModelFragment { get { return this.Second; } }

	public int AttemptedRegistrationsCount { get { return attemptedRegistrationsCount; } }

	public void AttemptedRegistration()
	{
		attemptedRegistrationsCount += 1;
	}
}

class SortFragmentOnVertexCount : IComparer<GameObject>
{
	public int Compare(GameObject x, GameObject y)
	{
		int xCount = x.GetComponent<MeshFilter>().mesh.vertexCount;
		int yCount = y.GetComponent<MeshFilter>().mesh.vertexCount;

		return xCount.CompareTo(yCount);
	}
}
