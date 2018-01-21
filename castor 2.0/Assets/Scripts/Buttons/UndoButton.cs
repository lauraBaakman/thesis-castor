using UnityEngine;
using RTEditor;

namespace Buttons
{
    public class UndoButton : AbstractButton
    {
        public override void OnClick()
        {
            Undo();
        }

        protected override void DetectKeyBoardShortCut()
        {
            if (IsEditorUndoCombinationPressed() ||
                IsDeploymentUndoCombinationPressed()
               ) Undo();
        }

        private bool IsEditorUndoCombinationPressed()
        {
            // We are running in deployment mode, not editor mode
            if (!Application.isEditor) return false;

            return (Input.GetButtonDown("Undo") &&
                    InputHelper.IsAnyCtrlOrCommandKeyPressed() &&
                    InputHelper.IsAnyShiftKeyPressed());
        }

        private bool IsDeploymentUndoCombinationPressed()
        {
            return (Input.GetButtonDown("Undo") &&
                    InputHelper.IsAnyCtrlOrCommandKeyPressed());
        }

        private void Undo()
        {
            Debug.Log("Undo!");
        }
    }
}

