using System.Collections.Generic;
using SimpleFileBrowser;
using UnityEngine;
using IO;
using System.IO;
using System;

namespace Buttons
{
	public class StatisticsButton : AbstractButton
	{
		private static string initialPath;

		private string directory;

		private List<StatisticsComputer.RunResult> runs;

		public StatisticsComputer statisticsComputer;

		private Dictionary<string, Dictionary<string, object>> ExperimentCSVData;

		protected override void Awake()
		{
			base.Awake();
			initialPath = Application.isEditor ? "/Users/laura/Repositories/thesis-experiment/simulated" : null;
		}

		protected override void ExecuteButtonAction()
		{
			Reset();
			RetrieveOverviewCSV();
		}

		protected override bool HasDetectedKeyBoardShortCut()
		{
			return Input.GetButtonDown("Statistics");
		}

		private void Reset()
		{
			directory = "";
			runs = null;
			ExperimentCSVData = null;
		}

		private void RetrieveOverviewCSV()
		{
			FileBrowser.ShowLoadDialog(
				onSuccess: RetrieveExperimentResultFolder,
				onCancel: () => { },
				folderMode: false,
				initialPath: initialPath,
				title: "Select a data set overview csv.",
				loadButtonText: "Select"
			);
		}

		private void RetrieveExperimentResultFolder(string datasetCSVpath)
		{
			FileBrowser.ShowLoadDialog(
				onSuccess: (path) =>
				StartCoroutine(
					ProcessExperimentResultsFolder(
						datasetCSVpath: datasetCSVpath,
						resultsDirectory: path
					)
				),
				onCancel: () => { },
				folderMode: true,
				initialPath: Path.GetDirectoryName(datasetCSVpath),
				title: "Select a results directory.",
				loadButtonText: "Select"
			);
		}

		private IEnumerator<object> ProcessExperimentResultsFolder(string datasetCSVpath, string resultsDirectory)
		{
			ReadDataSetCSV(datasetCSVpath);
			yield return null;

			StartCoroutine(RetrieveRuns(resultsDirectory));
			yield return new WaitWhile(() => runs == null);

			foreach (StatisticsComputer.RunResult run in runs)
			{
				StartCoroutine(statisticsComputer.Compute(run));
				yield return new WaitUntil(() => statisticsComputer.Done);
				Ticker.Receiver.Instance.SendMessage(
					methodName: "OnMessage",
					value: new Ticker.Message.InfoMessage("Finished analyzing " + run.objPath),
					options: SendMessageOptions.RequireReceiver
				);
			}

			WriteProcessedResultCSVDataFile(this.runs);
			Ticker.Receiver.Instance.SendMessage(
				methodName: "OnMessage",
				value: new Ticker.Message.InfoMessage("Finished analyzing results in " + resultsDirectory),
				options: SendMessageOptions.RequireReceiver
			);
		}

		public void CLIProcessExperimentResultsFolder(string datasetCSVpath, string resultsDirectory, GameObject listener)
		{
			ReadDataSetCSV(datasetCSVpath);

			CLIRetrieveRuns(resultsDirectory);

			foreach (StatisticsComputer.RunResult run in runs)
			{
				statisticsComputer.Compute(run);
				Ticker.Receiver.Instance.SendMessage(
					methodName: "OnMessage",
					value: new Ticker.Message.InfoMessage(DateTime.Now.ToString() + " finished analyzing " + run.objPath),
					options: SendMessageOptions.RequireReceiver
				);
			}

			WriteProcessedResultCSVDataFile(this.runs);

			Ticker.Receiver.Instance.SendMessage(
				methodName: "OnMessage",
				value: new Ticker.Message.InfoMessage(DateTime.Now.ToString() + " finished analyzing results in " + resultsDirectory),
				options: SendMessageOptions.RequireReceiver
			);
			listener.SendMessage(
				methodName: "OnCommandFinished",
				value: null,
				options: SendMessageOptions.RequireReceiver);
		}

		private void WriteProcessedResultCSVDataFile(List<StatisticsComputer.RunResult> results)
		{
			//Convert resutls to a list of dictionaries
			List<Dictionary<string, object>> dictionaries = new List<Dictionary<string, object>>();
			foreach (StatisticsComputer.RunResult result in results) dictionaries.Add(result.ToDictionary());

			//Write to csv file
			string path = Path.Combine(this.directory, "results.csv");
			CSVWriter writer = new CSVWriter(path);
			writer.Write(dictionaries);
		}

		private void CLIRetrieveRuns(string inputDirectory)
		{
			this.directory = inputDirectory;

			string csvDataFile = GetResultsCSVDataFile();

			List<Dictionary<string, object>> ResultsCSVData = new CSVReader().Read(csvDataFile);

			List<StatisticsComputer.RunResult> localRuns = new List<StatisticsComputer.RunResult>(ResultsCSVData.Count);
			foreach (Dictionary<string, object> row in ResultsCSVData)
			{
				localRuns.Add(ExtractRun(row));
			}
			this.runs = localRuns;
		}

		private IEnumerator<object> RetrieveRuns(string inputDirectory)
		{
			this.directory = inputDirectory;
			yield return null;

			string csvDataFile = GetResultsCSVDataFile();
			yield return null;

			List<Dictionary<string, object>> ResultsCSVData = new CSVReader().Read(csvDataFile);
			yield return null;

			List<StatisticsComputer.RunResult> localRuns = new List<StatisticsComputer.RunResult>(ResultsCSVData.Count);
			foreach (Dictionary<string, object> row in ResultsCSVData)
			{
				localRuns.Add(ExtractRun(row));
				yield return null;
			}
			this.runs = localRuns;
			yield return null;
		}

		private void ReadDataSetCSV(string path)
		{
			List<Dictionary<string, object>> rows = new CSVReader().Read(path);
			ExperimentCSVData = new Dictionary<string, Dictionary<string, object>>();

			foreach (Dictionary<string, object> row in rows)
			{
				ExperimentCSVData.Add((string)row["uuid"], row);
			}
		}

		private string GetResultsCSVDataFile()
		{
			string[] csvFiles = System.IO.Directory.GetFiles(directory, "*.csv");
			ValidateCSVFiles(csvFiles);
			return csvFiles[0];
		}

		private void ValidateCSVFiles(string[] csvFiles)
		{
			if (csvFiles.Length != 1)
			{
				Debug.LogError(
					string.Format(
						"Unexpectedly found {0} csv file(s), expected exactly one file.",
						csvFiles.Length
					)
				);
			}
		}

		private StatisticsComputer.RunResult ExtractRun(Dictionary<string, object> csvRow)
		{
			StatisticsComputer.RunData runData = ExtractRunData(csvRow);
			string path = Path.Combine(directory, String.Format("{0}.obj", runData.id));

			Dictionary<string, object> experimentDataRow = ExperimentCSVData[runData.id];

			return new StatisticsComputer.RunResult(
				objPath: path,
				expectedRotation: ExtractExpectedRotation(experimentDataRow),
				expectedTranslation: ExtractExpectedTranslation(experimentDataRow),
				runData: runData
			);
		}

		private StatisticsComputer.RunData ExtractRunData(Dictionary<string, object> csvRow)
		{
			//id, termination message, termination error, termination iteration
			string id = csvRow["id"] as string;
			string terminationMessage = csvRow["termination message"] as string;
			float terminationError = (float)csvRow["termination error"];
			int terminationIteration = (int)csvRow["termination iteration"];
			return new StatisticsComputer.RunData(
				id: id,
				terminationError: terminationError,
				terminationMessage: terminationMessage,
				terminationIteration: terminationIteration
			);
		}

		private Vector3 ExtractExpectedTranslation(Dictionary<string, object> row)
		{
			float x = (float)row["expected translation x"];
			float y = (float)row["expected translation y"];
			float z = (float)row["expected translation z"];
			return new Vector3(x, y, z);
		}

		private Quaternion ExtractExpectedRotation(Dictionary<string, object> row)
		{
			float x = (float)row["expected rotation quaternion x"];
			float y = (float)row["expected rotation quaternion y"];
			float z = (float)row["expected rotation quaternion z"];
			float w = (float)row["expected rotation quaternion w"];
			return new Quaternion(x: x, y: y, z: z, w: w);
		}
	}
}