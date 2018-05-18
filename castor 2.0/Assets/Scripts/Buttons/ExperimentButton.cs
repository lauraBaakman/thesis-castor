using System;
using SimpleFileBrowser;
using UnityEngine;
using Experiment;

namespace Buttons
{
	public class ExperimentButton : AbstractButton
	{
		public AbstractButton DeleteButton;
		public AbstractButton SelectAllButton;

		public GameObject ICPFragmentParent;

		private static string initialPath;

		private void Start()
		{
			initialPath = Application.isEditor ? "/Users/laura/Repositories/thesis-experiment/simulated" : null;
		}

		protected override void ExecuteButtonAction()
		{
			RetrieveExperimentInputData();
		}

		protected override bool HasDetectedKeyBoardShortCut()
		{
			return Input.GetButtonDown("Experiment");
		}

		private void RetrieveExperimentInputData()
		{
			FileBrowser.ShowLoadDialog(
				onSuccess: ProcessExperimentConfigurationFile,
				onCancel: () => { },
				folderMode: false,
				initialPath: initialPath,
				title: "Select the configuration file (config.json) that was generated with the obj files.",
				loadButtonText: "Select"
			);
		}

		private void ProcessExperimentConfigurationFile(string path)
		{
			ExperimentRunner.Configuration configuration;
			try { configuration = ExperimentRunner.Configuration.FromJson(path); }
			catch (Exception e)
			{
				Debug.LogError("Could not read the file " + path + "\n\t error: " + e.Message);
				return;
			}

			ClearScene();

			ExperimentRunner experiment = ICPFragmentParent.GetComponent<ExperimentRunner>();
			experiment.Init(configuration);

			StartCoroutine(experiment.Execute());
		}

		private void ClearScene()
		{
			SelectAllButton.OnExecuteButtonAction();
			DeleteButton.OnExecuteButtonAction();
		}
	}
}
