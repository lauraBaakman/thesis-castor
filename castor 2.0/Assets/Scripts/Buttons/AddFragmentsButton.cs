using System;
using UnityEngine;
using IO;
using System.IO;
using System.Collections.Generic;

namespace Buttons
{
	public class AddFragmentsButton : AbstractButton
	{
		public GameObject FragmentsRoot;

		private FragmentsImporter importer;
		private string defaultOutputPath = "/Users/laura/Repositories/thesis-experiment/real/results/initial";

		private bool recievedReadFragmentNotification = false;

		private void Start()
		{
			importer = new IO.FragmentsImporter(
				fragmentParent: FragmentsRoot,
				callBack: NotifyUser,
				randomizeTransform: true, copyVerticesToTexture: false
			);
		}

		private void NotifyUser(IO.ReadResult result)
		{
			Ticker.Receiver.Instance.SendMessage(
				methodName: "OnMessage",
				value: result.ToTickerMessage(),
				options: SendMessageOptions.RequireReceiver
			);

			RealExperimentLogger.Instance.Log(result);

			recievedReadFragmentNotification = true;
		}

		protected override bool HasDetectedKeyBoardShortCut()
		{
			return false;
		}

		protected override void ExecuteButtonAction()
		{
			SimpleFileBrowser.FileBrowser.ShowLoadDialog(
				onSuccess: ImportFragmentsInDirectory,
				folderMode: true,
				onCancel: () => { },
				initialPath: Application.isEditor ? "/Users/laura/Repositories/thesis-experiment/real/_3_subsampled" : null
			);
		}

		private void ImportFragmentsInDirectory(string path)
		{
			RealExperimentLogger.Instance.SetInputDirectory(path);

			string[] objFiles = Directory.GetFiles(path, "*.obj");
			StartCoroutine(ImportFragments(objFiles));
		}

		private IEnumerator<object> ImportFragments(string[] objFiles)
		{
			foreach (string currentFile in objFiles)
			{
				importer.Import(currentFile);
				yield return new WaitUntil(() => this.recievedReadFragmentNotification);
			}
			recievedReadFragmentNotification = false;
			ExportRandomlyTransformedFragments();
		}

		private void ExportRandomlyTransformedFragments()
		{
			IO.FragmentsExporter exporter = new IO.FragmentsExporter(FragmentsRoot, DoNothing);

			if (!Directory.Exists(defaultOutputPath)) Directory.CreateDirectory(defaultOutputPath);
			exporter.ExportFragments(defaultOutputPath);
		}

		private void DoNothing(IO.WriteResult result) { }
	}
}