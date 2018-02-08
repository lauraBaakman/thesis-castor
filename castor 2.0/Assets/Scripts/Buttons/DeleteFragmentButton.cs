using UnityEngine;
using System.Collections;

namespace Buttons
{
    public class DeleteFragmentButton : AbstractButton, Fragments.ISelectionControllerListener
    {
        protected override void ExecuteButtonAction()
        {
            Debug.Log("Delete fragment!");
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return Input.GetKeyDown(KeyCode.Backspace) && RTEditor.InputHelper.IsAnyCtrlOrCommandKeyPressed();
        }

        public void OnNumberOfSelectedObjectsChanged(int currentCount)
        {
            Debug.Log("# Selected fragments changed!");
        }
    }
}

