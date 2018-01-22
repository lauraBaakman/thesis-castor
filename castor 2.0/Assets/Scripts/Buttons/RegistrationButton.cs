using UnityEngine;

namespace Buttons
{
    public class RegistrationButton : AbstractButton
    {
        protected override void ExecuteButtonAction()
        {
            Debug.Log("Register");
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return
                RTEditor.InputHelper.IsAnyCtrlOrCommandKeyPressed() &&
                Input.GetButtonDown("Register");
        }
    }

}
