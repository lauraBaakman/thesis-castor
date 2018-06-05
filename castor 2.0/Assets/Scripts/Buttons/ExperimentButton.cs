using System;
using SimpleFileBrowser;
using UnityEngine;
using Experiment;
using System.IO;

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

		public void ProcessExperimentConfigurationFile(string path)
		{
			Ticker.Receiver.Instance.SendMessage(
				methodName: "OnMessage",
				value: new Ticker.Message.InfoMessage("Starting experiment with the configuration from " + path),
				options: SendMessageOptions.RequireReceiver
			);
			ExperimentRunner.Configuration configuration;
			try { configuration = ExperimentRunner.Configuration.FromJson(path); }
			catch (Exception e)
			{
				Ticker.Receiver.Instance.SendMessage(
					methodName: "OnMessage",
					value: new Ticker.Message.ErrorMessage("Could not read the file " + path + "\n\t error: " + e.Message),
					options: SendMessageOptions.RequireReceiver
				);
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
