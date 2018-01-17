using UnityEngine;

namespace Buttons
{
    public class Quit : AbstractButton
    {

        private void Update()
        {
            DetectKeyBoardShortCut();
        }

        public override void OnClick()
        {
            QuitApplication();
        }

        private void QuitApplication()
        {
            Application.Quit();
            if (Application.isEditor) Debug.Log("Pressed the quit button, however Application.Quit() does not work in editor mode.");
        }

        protected override void DetectKeyBoardShortCut()
        {
            if (Input.GetButton("Quit")) QuitApplication();
        }
    }
}