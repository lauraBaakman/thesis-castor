using UnityEngine;

namespace Buttons
{
    public class QuitButton : AbstractButton
    {
        protected override bool HasDetectedKeyBoardShortCut()
        {
            return Input.GetButtonDown("Quit");
        }

        protected override void ExecuteButtonAction()
        {
            if (Application.isEditor) QuitInEditorMode();
            else QuitInDeploymentMode();
        }

        private void QuitInEditorMode()
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }

        private void QuitInDeploymentMode()
        {
            Application.Quit();
        }
    }
}