using System;
using UnityEngine;
using IO;

namespace Buttons
{
	public class AddFragmentButton : AbstractButton
	{
		public GameObject FragmentsRoot;

		private FragmentsImporter importer;


		private void Start()
		{
			importer = new IO.FragmentsImporter(
				fragmentParent: FragmentsRoot,
				callBack: NotifyUser,
				randomizeTransform: false, copyVerticesToTexture: true
			);

			if (Application.isEditor) LoadTestFragments();
		}


		private void LoadTestFragments()
		{
			importer.Import("/Users/laura/Repositories/thesis-experiment/simulated/test_data_small/obj/9528401f-a2c9-3502-a795-afc276db60ef.obj");
			importer.Import("/Users/laura/Repositories/thesis-experiment/simulated/test_data_small/obj/locked_fragment_f90e89025f4cad9d1da70ddfd85c2368.obj");
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
			if (Application.isEditor) return IsEditorAddFragmentCombinationPressed();
			else return IsDeploymentAddFragmentCombinationPressed();
		}

		private bool IsDeploymentAddFragmentCombinationPressed()
		{
			return Input.GetButtonDown("Add Fragment") && RTEditor.InputHelper.IsAnyCtrlOrCommandKeyPressed();
		}

		private bool IsEditorAddFragmentCombinationPressed()
		{
			return IsDeploymentAddFragmentCombinationPressed() && RTEditor.InputHelper.IsAnyShiftKeyPressed();
		}

		protected override void ExecuteButtonAction()
		{
			importer.Import();
		}
	}
}