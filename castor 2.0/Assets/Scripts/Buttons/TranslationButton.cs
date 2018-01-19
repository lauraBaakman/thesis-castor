using UnityEngine;

namespace Buttons
{
    public class TranslationButton : AbstractButton
    {

        public override void OnClick()
        {
            ShowTranslationWidget();
        }

        private void ShowTranslationWidget()
        {
            Debug.Log("Time to show the translation widget");
        }

        protected override void DetectKeyBoardShortCut()
        {
            if (Input.GetButton("Show Translation Widget")) ShowTranslationWidget();
        }
    }

}
