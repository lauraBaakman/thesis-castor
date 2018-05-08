using UnityEngine;
using RTEditor;

namespace Buttons
{
	public class RedoButton : AbstractButton
	{
		protected override bool HasDetectedKeyBoardShortCut()
		{
			if (Application.isEditor) return IsEditorUndoCombinationPressed();
			else return IsDeploymentUndoCombinationPressed();
		}

		private bool IsEditorUndoCombinationPressed()
		{
			return (Input.GetButtonDown("Redo") &&
					InputHelper.IsAnyCtrlOrCommandKeyPressed() &&
					InputHelper.IsAnyShiftKeyPressed());
		}

		private bool IsDeploymentUndoCombinationPressed()
		{
			return (Input.GetButtonDown("Redo") &&
					InputHelper.IsAnyCtrlOrCommandKeyPressed());
		}

		protected override void ExecuteButtonAction()
		{
			EditorUndoRedoSystem.Instance.SendMessage(
				methodName: "OnRedo",
				options: SendMessageOptions.RequireReceiver
			);
		}
	}

}

