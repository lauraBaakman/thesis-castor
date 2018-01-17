using UnityEngine;

namespace Buttons
{
    public class QuitButton : AbstractButton
    {

        public override void OnClick()
        {
            Quit();
        }

        private void Quit()
        {
            Application.Quit();
            if (Application.isEditor) Debug.Log("We should quit the application, however Application.Quit() does not work in editor mode.");
        }

        protected override void DetectKeyBoardShortCut()
        {
            if (Input.GetButton("Quit")) Quit();
        }
    }
}