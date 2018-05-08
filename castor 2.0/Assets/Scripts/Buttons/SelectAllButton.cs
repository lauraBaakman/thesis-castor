using UnityEngine;

namespace Buttons
{
	public class SelectAllButton : AbstractButton
	{
		protected override void ExecuteButtonAction()
		{
			RTEditor.EditorObjectSelection.Instance.SendMessage(
				methodName: "OnSelectAll",
				options: SendMessageOptions.RequireReceiver
			);
		}

		protected override bool HasDetectedKeyBoardShortCut()
		{
			return Input.GetButtonDown("Select All") && RTEditor.InputHelper.IsAnyCtrlOrCommandKeyPressed();
		}
	}
}

