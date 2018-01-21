
using UnityEngine;

namespace Buttons
{
    public class HelpButton : AbstractButton
    {
        private string HelpPage = "https://laurabaakman.github.io/thesis-castor/";

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return Input.GetButtonDown("Help");
        }

        protected override void ExecuteButtonAction()
        {
            Application.OpenURL(HelpPage);
        }
    }

}
