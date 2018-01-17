using UnityEngine;

namespace Buttons
{
    public class Quit : MonoBehaviour
    {

        private void Update()
        {
            DetectKeyBoardShortCut();
        }

        public void OnClick()
        {
            QuitApplication();
        }

        private void QuitApplication()
        {
            Application.Quit();
            if (Application.isEditor) Debug.Log("Pressed the quit button, however Application.Quit() does not work in editor mode.");
        }

        private void DetectKeyBoardShortCut()
        {
            if (Input.GetButton("Quit")) QuitApplication();
        }
    }
}