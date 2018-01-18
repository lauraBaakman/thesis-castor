
using UnityEngine;

namespace Buttons
{
    public class HelpButton : AbstractButton
    {
        private string HelpPage = "https://laurabaakman.github.io/thesis-castor/";

        public override void OnClick()
        {
            OpenHelpWebPage();
        }

        private void OpenHelpWebPage(){
            Application.OpenURL(HelpPage);
        }

        protected override void DetectKeyBoardShortCut()
        {
            if (Input.GetButton("Help")) OpenHelpWebPage();
        }
    }

}
