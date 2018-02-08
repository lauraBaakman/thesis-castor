using UnityEngine;
using System.Collections;

namespace Buttons
{
    public class DeleteFragmentButton : AbstractButton
    {
        protected override void ExecuteButtonAction()
        {
            Debug.Log("Delete fragment!");
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return Input.GetKeyDown(KeyCode.Backspace) && RTEditor.InputHelper.IsAnyCtrlOrCommandKeyPressed();
        }
    }
}

