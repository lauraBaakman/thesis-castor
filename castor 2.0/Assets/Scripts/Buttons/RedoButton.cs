using UnityEngine;
using RTEditor;

namespace Buttons
{
    public class RedoButton : AbstractButton
    {
        public override void OnClick()
        {
            Redo();
        }

        protected override void DetectKeyBoardShortCut()
        {
            if (IsEditorUndoCombinationPressed() ||
                IsDeploymentUndoCombinationPressed()
               ) Redo();
        }

        private bool IsEditorUndoCombinationPressed()
        {
            // We are running in deployment mode, not editor mode
            if (!Application.isEditor) return false;

            return (Input.GetButtonDown("Redo") &&
                    InputHelper.IsAnyCtrlOrCommandKeyPressed() &&
                    InputHelper.IsAnyShiftKeyPressed());
        }

        private bool IsDeploymentUndoCombinationPressed()
        {
            return (Input.GetButtonDown("Redo") &&
                    InputHelper.IsAnyCtrlOrCommandKeyPressed());
        }

        private void Redo()
        {
            EditorUndoRedoSystem.Instance.SendMessage(
                methodName: "OnRedo",
                options: SendMessageOptions.RequireReceiver
            );
        }
    }

}

