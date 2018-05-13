
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Registration;
using Registration.Messages;
using System.Globalization;
using System;

namespace Experiment
{
	public class ExperimentRunner : MonoBehaviour, IICPListener
	{
		private Configuration configuration;

		public static string ExperimentFragmentPrefabPath = "ExperimentFragment";
		private static string runDataFileName = "data.csv";

		private GameObject staticFragment;
		private List<RunExecuter.Run> runs;

		private List<Settings> ICPSettings;

		private IO.FragmentImporter fragmentImporter;
		private IO.FragmentExporter fragmentExporter;

		private Results results;

		private string outputDirectory;

		private StreamWriter runSetWriter;

		public void Init(Configuration configuration)
		{
			this.configuration = configuration;

			this.fragmentImporter = new IO.FragmentImporter(
				this.gameObject, FragmentReaderCallBack,
				copyVerticesToTexture: true,
				randomizeTransform: false
			);
			this.fragmentExporter = new IO.FragmentExporter(FragmentWriterCallBack);

			this.ICPSettings = new List<Settings>();

			SetUp();
		}

		private void FragmentReaderCallBack(IO.ReadResult result)
		{
			if (result.Failed)
			{
				Ticker.Receiver.Instance.SendMessage(
					methodName: "OnMessage",
					value: result.ToTickerMessage(),
					options: SendMessageOptions.RequireReceiver
				);
			}
		}

		private void FragmentWriterCallBack(IO.WriteResult result)
		{
			Ticker.Receiver.Instance.SendMessage(
				methodName: "OnMessage",
				value: result.ToTickerMessage(),
				options: SendMessageOptions.RequireReceiver
			);
		}

		private void SetUp()
		{
			HandleStaticFragment();

			GenerateICPSettings();

			this.runs = CollectRuns();
		}

		private void GenerateICPSettings()

		{
			ICPSettings.Add(
				new Settings(
					name: "igdIntersectionTermError",
					referenceTransform: this.gameObject.transform,
					transformFinder: new IGDTransformFinder(
						new IGDTransformFinder.Configuration(
							convergenceError: 0.001f,
							learningRate: 0.001f,
							maxNumIterations: 200,
							errorMetric: new Registration.Error.IntersectionTermError(0.5f, 0.5f)
						)
					)
				)
			);

			ICPSettings.Add(
				new Settings(
					name: "igdWheelerError",
					referenceTransform: this.gameObject.transform,
					transformFinder: new IGDTransformFinder(
						new IGDTransformFinder.Configuration(
							convergenceError: 0.001f,
							learningRate: 0.001f,
							maxNumIterations: 200,
							errorMetric: new Registration.Error.WheelerIterativeError()
						)
					)
				)
			);

			ICPSettings.Add(
				new Settings(
					name: "horn",
					referenceTransform: this.gameObject.transform,
					transformFinder: new HornTransformFinder()
				)
			);

			ICPSettings.Add(
				new Settings(
					name: "low",
					referenceTransform: this.gameObject.transform,
					transformFinder: new LowTransformFinder()
				)
			);
		}

		private List<RunExecuter.Run> CollectRuns()
		{
			return RunExecuter.Run.FromCSV(configuration.configurations);
		}

		private void CreateResultsDirectory(string path)
		{
			Directory.CreateDirectory(path);
		}

		private string CreateResultsDirectoryName(Settings ICPSetting)
		{
			string basePath = configuration.outputDirectory;
			string directoryName = string.Format("results_{0}", ICPSetting.name);
			string directory = Path.Combine(basePath, directoryName);
			return directory;
		}

		private void HandleStaticFragment()
		{
			this.staticFragment = Import(configuration.lockedFragmentFile);

			Lock(staticFragment);
		}

		private GameObject Import(string path)
		{
			return fragmentImporter.Import(path, prefabPath: ExperimentFragmentPrefabPath);
		}

		private void Lock(GameObject fragment)
		{
			fragment.SendMessage(
				methodName: "OnToggledLockedState",
				value: true,
				options: SendMessageOptions.DontRequireReceiver
			);
		}

		private void Write(GameObject fragment)
		{
			string path = System.IO.Path.Combine(
				path1: this.outputDirectory,
				path2: Path.GetFileName(configuration.lockedFragmentFile)
			);
			fragmentExporter.Export(fragment, path);
		}

		private bool CompletedAllRuns()
		{
			return results.Count == runs.Count;
		}

		private bool ResultsDirectoryExists()
		{
			return Directory.Exists(this.outputDirectory);
		}

		private void SetUpForSettings(Settings settings)
		{
			if (ResultsDirectoryExists()) SetUpForContinuation();
			else SetUpForFirstTime(settings);
		}

		private void SetUpForFirstTime(Settings settings)
		{
			CreateResultsDirectory(this.outputDirectory);

			Write(staticFragment);

			settings.ToJson(Path.Combine(this.outputDirectory, "settings.json"));

			this.results = new Results();

			SetUpRunSetWriter(continuation: false);
		}
		private void SetUpForContinuation()
		{
			string path = Path.Combine(this.outputDirectory, runDataFileName);

			this.results = Results.FromFile(path);

			if (this.results.Count == runs.Count)
			{
				Debug.Log("Performed alle experiments in " + this.outputDirectory);
				return;
			}

			SetUpRunSetWriter(continuation: true);
		}

		public IEnumerator<object> Execute()
		{
			RunExecuter executer = new RunExecuter(listener: this.gameObject, staticFragment: staticFragment,
												   fragmentImporter: fragmentImporter, fragmentExporter: fragmentExporter);

			RunExecuter.Run run;

			foreach (Settings ICPSetting in this.ICPSettings)
			{
				this.outputDirectory = CreateResultsDirectoryName(ICPSetting);
				yield return null;

				SetUpForSettings(ICPSetting);
				yield return null;

				for (int i = 0; i < runs.Count; i++)
				{
					run = runs[i];

					//This run has not yet been executed
					if (!results.HasResultFor(run.id))
					{
						run.ICPSettings = ICPSetting;
						executer.OutputDirectory = this.outputDirectory;

						runSetWriter.Write(string.Format("{0}, ", run.id));
						yield return null;

						StartCoroutine(executer.Execute(run));
						yield return new WaitUntil(executer.IsCurrentRunFinished);

						//Wait a while to give results the chance to be updated
						yield return new WaitForSeconds(2);
					}
					else Debug.Log(string.Format("Skipping {0}", run.id));
				}
			}
			Debug.Log("Finished!");
		}

		private void SetUpRunSetWriter(bool continuation = false)
		{
			runSetWriter = new StreamWriter(Path.Combine(this.outputDirectory, runDataFileName), append: continuation);
			if (!continuation)
			{
				runSetWriter.WriteLine(
					string.Format(
						"{0}, {1}, {2}, {3}",
						"id", "termination message", "termination error", "termination iteration"
					)
				);
			}
		}

		private void CleanUpRunSetWriter()
		{
			runSetWriter.Close();
			runSetWriter.Dispose();
		}

		#region ICPInterface
		public void OnPreparationStepCompleted(ICPPreparationStepCompletedMessage message) { }

		public void OnStepCompleted(ICPStepCompletedMessage message) { }

		public void OnICPTerminated(ICPTerminatedMessage message)
		{
			//This function is also called when we are not running an experiment for some reason
			if (runSetWriter == null) return;

			results.AddResult(message);

			string line = string.Format(
				"'{0}', {1}, {2}",
				message.Message,
				message.errorAtTermination.ToString("E10", CultureInfo.InvariantCulture),
				message.terminationIteration
			);
			runSetWriter.WriteLine(line);
			if (CompletedAllRuns()) CleanUpRunSetWriter();
		}
		#endregion

		[System.Serializable]
		public class Configuration
		{
			public string lockedFragmentFile;
			public string outputDirectory;
			public string configurations;
			public string id;

			public static Configuration FromJson(string path)
			{
				string json_string = File.ReadAllText(path);
				Configuration configuration = JsonUtility.FromJson<Configuration>(json_string);

				return configuration;
			}
		}
	}
}