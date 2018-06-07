
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Registration;
using Registration.Messages;
using System.Globalization;
using System;

namespace Experiment
{
	public class ExperimentRunner : MonoBehaviour, IICPListener, IICPStartEndListener
	{
		private Configuration configuration;

		public GameObject Listener;
		public static string ExperimentFragmentPrefabPath = "ExperimentFragment";
		private static string runDataFileName = "data.csv";

		private GameObject staticFragment;
		private List<RunExecuter.Run> runs;

		private List<Settings> ICPSettings;

		private IO.FragmentImporter fragmentImporter;
		private IO.FragmentExporter fragmentExporter;

		private ICPStartedMessage ICPStartedMessage;

		private Results results;

		//Make sure the algorithm does not continue until the results are written to file
		private bool finishedWriting = false;

		private string outputDirectory;

		/// <summary>
		/// Build the path where for the results.csv file.
		/// </summary>
		/// <value>The run data path.</value>
		private string RunDataPath
		{
			get { return Path.Combine(this.outputDirectory, runDataFileName); }
		}

		/// <summary>
		/// Initialze the experiment runnen for the passed configuration.
		/// </summary>
		/// <param name="configuration">Configuration.</param>
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

		/// <summary>
		/// Call back for the fragment reader, sends a message to the ticker 
		/// upon failure.
		/// </summary>
		/// <param name="result">Result.</param>
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

		/// <summary>
		/// Call back for the fragmen writer, sends a message to the ticker 
		/// upon failure.
		/// </summary>
		/// <param name="result">Result.</param>
		private void FragmentWriterCallBack(IO.WriteResult result)
		{
			Ticker.Receiver.Instance.SendMessage(
				methodName: "OnMessage",
				value: result.ToTickerMessage(),
				options: SendMessageOptions.RequireReceiver
			);
		}

		/// <summary>
		/// Set up of the Experiment runner for the specific experiment.
		/// </summary>
		private void SetUp()
		{
			HandleStaticFragment();

			GenerateICPSettings();

			this.runs = CollectRuns();
		}

		/// <summary>
		/// Generates the different ICP settings that should be tested in the experiment.
		/// </summary>
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

		/// <summary>
		/// Collect als the model fragment files form the configuration.
		/// </summary>
		/// <returns>The runs.</returns>
		private List<RunExecuter.Run> CollectRuns()
		{
			return RunExecuter.Run.FromCSV(configuration);
		}

		/// <summary>
		/// Create the directory where the results should be stored.
		/// </summary>
		/// <param name="path">Path.</param>
		private void CreateResultsDirectory(string path)
		{
			Directory.CreateDirectory(path);
		}

		/// <summary>
		/// Creates the name of the results directory, this name depends on the
		/// ICP settings whose results are stored in that directory.
		/// </summary>
		/// <returns>The results directory name.</returns>
		/// <param name="ICPSetting">ICPS etting.</param>
		private string CreateResultsDirectoryName(Settings ICPSetting)
		{
			string basePath = configuration.OutputDirectory;
			string directoryName = string.Format("results_{0}", ICPSetting.name);
			string directory = Path.Combine(basePath, directoryName);
			return directory;
		}


		/// <summary>
		/// Load the static fragment and lock it to make sure that ICP treats it
		/// as the static fragment.
		/// </summary>
		private void HandleStaticFragment()
		{
			this.staticFragment = Import(configuration.LockedFragmentFile);
			Lock(staticFragment);
		}

		/// <summary>
		/// Import a game object that is stored in an obj file at the specified 
		/// string.
		/// </summary>
		/// <returns>The import.</returns>
		/// <param name="path">Path.</param>
		private GameObject Import(string path)
		{
			return fragmentImporter.Import(path, prefabPath: ExperimentFragmentPrefabPath);
		}

		/// <summary>
		/// Lock the specified fragment.
		/// </summary>
		/// <param name="fragment">Fragment.</param>
		private void Lock(GameObject fragment)
		{
			fragment.SendMessage(
				methodName: "OnToggledLockedState",
				value: true,
				options: SendMessageOptions.DontRequireReceiver
			);
		}

		/// <summary>
		/// Write the specified fragment ot file. The path is determiend based on 
		/// the name of the fragment.
		/// </summary>
		/// <param name="fragment">Fragment.</param>
		private void Write(GameObject fragment)
		{
			string path = System.IO.Path.Combine(
				path1: this.outputDirectory,
				path2: Path.GetFileName(configuration.LockedFragmentFile)
			);
			fragmentExporter.Export(fragment, path);
		}

		/// <summary>
		/// Returns true if all runs, i.e. if ICP has run for every combination 
		/// of a modelfragment and an ICP configuration.
		/// </summary>
		/// <returns><c>true</c>, if all runs was completeded, <c>false</c> otherwise.</returns>
		private bool CompletedAllRuns()
		{
			return results.Count == runs.Count;
		}

		/// <summary>
		/// Returns true if the results directory already exists.
		/// </summary>
		/// <returns><c>true</c>, if directory exists was resultsed, <c>false</c> otherwise.</returns>
		private bool ResultsDirectoryExists()
		{
			return Directory.Exists(this.outputDirectory);
		}

		/// <summary>
		/// Handles the set up of the experiment for a specific ICP configuration.
		/// </summary>
		/// <param name="settings">Settings.</param>
		private void SetUpForSettings(Settings settings)
		{
			if (ResultsDirectoryExists()) SetUpForContinuation();
			else SetUpForFirstTime(settings);

			WriteTimeStampToRunDataFile();
		}

		/// <summary>
		/// This function handles the set up if this is the first time that we 
		/// run this ICP configuration with these data.
		/// </summary>
		/// <param name="settings">Settings.</param>
		private void SetUpForFirstTime(Settings settings)
		{
			CreateResultsDirectory(this.outputDirectory);

			Write(staticFragment);

			settings.ToJson(Path.Combine(this.outputDirectory, "settings.json"));

			this.results = new Results();

			WriteHeaderToFile();
		}

		/// <summary>
		/// This function handles the set up if we are continuing the experiment, 
		/// i.e. the result direcotry already existst and has a csv file with 
		/// termination errors for some of the fragments.
		/// </summary>
		private void SetUpForContinuation()
		{
			this.results = Results.FromFile(RunDataPath);
		}

		/// <summary>
		/// Execute the experiment.
		/// </summary>
		/// <returns>The execute.</returns>
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
						yield return null;

						StartCoroutine(executer.Execute(run));
						yield return new WaitUntil(executer.IsCurrentRunFinished);

						//Wait until the results are written to file
						yield return new WaitUntil(() => this.finishedWriting);
						this.finishedWriting = false;
					}
					else Debug.Log(string.Format("Skipping {0}", run.id));
				}
			}
			Listener.SendMessage(
				methodName: "OnExperimentFinished",
						options: SendMessageOptions.RequireReceiver);
		}

		/// <summary>
		/// Writes the time stamp to run data file.
		/// </summary>
		private void WriteTimeStampToRunDataFile()
		{
			string timestamp = string.Format(
				"# Written by CAstOR on {0}",
				System.DateTime.Now.ToLocalTime().ToString()
			);
			WriteToRunDataFile(timestamp, append: true);
		}

		/// <summary>
		/// Writes the header to file.
		/// </summary>
		private void WriteHeaderToFile()
		{
			WriteToRunDataFile(
				string.Format(
					"'{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
					"id", "initial error", "error termination threshold",
					"termination message", "termination error", "termination iteration"
				),
				append: false
			);
		}

		/// <summary>
		/// Writes to run data to file.
		/// </summary>
		/// <param name="line">Line.</param>
		/// <param name="append">If set to <c>true</c> append.</param>
		private void WriteToRunDataFile(string line, bool append)
		{
			//Close the stream after every write to handle a not so gracious shut down
			using (StreamWriter writer = new StreamWriter(RunDataPath, append: append))
			{
				writer.WriteLine(line);
			}
		}

		#region ICPInterface
		/// <summary>
		/// Listener that is triggered when the preparation step of ICP has been
		///  completed. No need to do anything.
		/// </summary>
		/// <param name="message">Message.</param>
		public void OnPreparationStepCompleted(ICPPreparationStepCompletedMessage message) { }

		/// <summary>
		/// Listener that is triggered when a ICP step has been completed. No 
		/// need to do anything.
		/// </summary>
		/// <param name="message">Message.</param>
		public void OnStepCompleted(ICPStepCompletedMessage message) { }

		/// <summary>
		/// Listener that is triggerd when the ICP algorithm has completed its 
		/// current run. I.e. given the current settings it has registered the 
		/// modelfragment to the static fragment.
		/// 
		/// If we are actually running the experiment store the results and write 
		/// the results to the results file.
		/// </summary>
		/// <param name="message">Message.</param>
		public void OnICPTerminated(ICPTerminatedMessage message)
		{
			//This function is also called when we are not running an experiment
			if (results == null) return;

			results.AddResult(message);

			string line = string.Format(
				"{0}, {1}, {2}, '{3}', {4}, {5}",
				message.modelFragmentName,
				this.ICPStartedMessage.InitialError.ToString("E10", CultureInfo.InvariantCulture),
				this.ICPStartedMessage.TerminationThreshold.ToString("E10", CultureInfo.InvariantCulture),
				message.Message,
				message.errorAtTermination.ToString("E10", CultureInfo.InvariantCulture),
				message.terminationIteration
			);
			WriteToRunDataFile(line, append: true);
			this.finishedWriting = true;
		}

		public void OnICPStarted(ICPStartedMessage message)
		{
			this.ICPStartedMessage = message;
		}
		#endregion

		public class Configuration
		{
			public readonly string LockedFragmentFile;
			public readonly string OutputDirectory;
			public readonly string ConfigurationsFile;
			public readonly string ID;
			public readonly string WorkingDirectory;

			public Configuration(_Configuration jsonConfiguration, string working_directory)
			{
				this.WorkingDirectory = working_directory;

				this.LockedFragmentFile = RelativePathToAbsolute(jsonConfiguration.lockedFragmentFile);
				this.OutputDirectory = RelativePathToAbsolute(jsonConfiguration.outputDirectory);
				this.ConfigurationsFile = RelativePathToAbsolute(jsonConfiguration.configurations);
				this.ID = jsonConfiguration.id;
			}

			/// <summary>
			/// Given a relative path return the absolute path.
			/// </summary>
			/// <returns>The absolute path.</returns>
			/// <param name="relativePath">The relative path.</param>
			public string RelativePathToAbsolute(string relativePath)
			{
				return Path.GetFullPath(Path.Combine(this.WorkingDirectory, relativePath));
			}

			/// <summary>
			/// Read a configuration from json.
			/// </summary>
			/// <returns>The configuation.</returns>
			/// <param name="path">The path of the json file</param>
			public static Configuration FromJson(string path)
			{
				_Configuration json_config = _Configuration.FromJson(path);
				return new Configuration(json_config, working_directory: Path.GetDirectoryName(path));
			}
		}

		[System.Serializable]
		public class _Configuration
		{
			public string lockedFragmentFile;
			public string outputDirectory;
			public string configurations;
			public string id;

			public static _Configuration FromJson(string path)
			{
				string json_string = File.ReadAllText(path);
				_Configuration configuration = JsonUtility.FromJson<_Configuration>(json_string);

				return configuration;
			}
		}
	}
}