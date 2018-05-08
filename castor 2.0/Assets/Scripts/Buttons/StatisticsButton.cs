﻿using System.Collections.Generic;
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

		private List<StatisticsComputer.Run> runs;

		public StatisticsComputer statisticsComputer;

		protected override void Awake()
		{
			base.Awake();
			initialPath = Application.isEditor ? "/Users/laura/Repositories/thesis-experiment/simulated" : null;
		}

		protected override void ExecuteButtonAction()
		{
			Reset();
			RetrieveExperimentResultFolder();
		}

		protected override bool HasDetectedKeyBoardShortCut()
		{
			return Input.GetButtonDown("Statistics");
		}

		private void Reset()
		{
			directory = "";
			runs = null;
		}

		private void RetrieveExperimentResultFolder()
		{
			//FileBrowser.ShowLoadDialog(
			//    onSuccess: (path) => StartCoroutine(ProcessExperimentResultsFolder(path)),
			//    onCancel: () => { },
			//    folderMode: true,
			//    initialPath: initialPath,
			//    title: "Select a results directory.",
			//    loadButtonText: "Select"
			//);
			Debug.Log("Temporarily using a fixed directory");
			StartCoroutine(ProcessExperimentResultsFolder("/Users/laura/Repositories/thesis-experiment/simulated/test_data_small/results_05-07_14-18-38-387"));
		}

		private IEnumerator<object> ProcessExperimentResultsFolder(string inputDirectory)
		{
			StartCoroutine(FindObjectIDs(inputDirectory));
			yield return new WaitWhile(() => runs == null);

			statisticsComputer.Init();

			foreach (StatisticsComputer.Run run in runs)
			{
				StartCoroutine(statisticsComputer.Compute(run));
				yield return new WaitUntil(() => statisticsComputer.Done);
			}

			WriteNewCSVDataFile();
		}

		private void WriteNewCSVDataFile()
		{
			throw new NotImplementedException();
		}

		private IEnumerator<object> FindObjectIDs(string inputDirectory)
		{
			this.directory = inputDirectory;
			yield return null;

			string csvDataFile = GetCSVDataFile();
			yield return null;

			List<Dictionary<string, object>> csvData = new CSVFileReader().Read(csvDataFile);
			yield return null;

			List<StatisticsComputer.Run> localRuns = new List<StatisticsComputer.Run>(csvData.Count);
			foreach (Dictionary<string, object> row in csvData)
			{
				localRuns.Add(ExtractRun(row));
				yield return null;
			}

			this.runs = localRuns;
		}

		private string GetCSVDataFile()
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

		private StatisticsComputer.Run ExtractRun(Dictionary<string, object> csvRow)
		{
			string id = csvRow["id"] as string;
			string path = Path.Combine(directory, String.Format("{0}.obj", id));
			return new StatisticsComputer.Run(objPath: path);
		}
	}
}