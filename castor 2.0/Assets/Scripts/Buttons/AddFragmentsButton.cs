using System;
using UnityEngine;
using IO;
using System.IO;

namespace Buttons
{
	public class AddFragmentsButton : AbstractButton
	{
		public GameObject FragmentsRoot;

		private FragmentsImporter importer;


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
			SendMessage(
				methodName: "OnSendMessageToTicker",
				value: result.ToTickerMessage(),
				options: SendMessageOptions.RequireReceiver
			);
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
			string[] objFiles = Directory.GetFiles(path, "*.obj");

			foreach (string currentFile in objFiles)
			{
				importer.Import(currentFile);
			}
		}
	}
}