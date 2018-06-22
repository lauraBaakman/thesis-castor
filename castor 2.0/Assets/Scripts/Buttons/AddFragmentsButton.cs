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

		private bool recievedReadFragmentNotification = false;

		private void Start()
		{
			importer = new IO.FragmentsImporter(
				fragmentParent: FragmentsRoot,
				callBack: NotifyUser,
				randomizeTransform: false, copyVerticesToTexture: false
			);
		}

		private void NotifyUser(IO.ReadResult result)
		{
			Ticker.Receiver.Instance.SendMessage(
				methodName: "OnMessage",
				value: result.ToTickerMessage(),
				options: SendMessageOptions.RequireReceiver
			);
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
				initialPath: Application.isEditor ? "/Users/laura/Repositories/thesis-experiment/real/subsampled" : null
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
		}
	}
}