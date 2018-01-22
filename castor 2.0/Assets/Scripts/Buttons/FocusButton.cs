using UnityEngine;
using System.Collections;

namespace Buttons
{
    public class FocusButton : AbstractButton
    {
        protected override bool HasDetectedKeyBoardShortCut()
        {
            return Input.GetButtonDown("Focus");
        }

        protected override void ExecuteButtonAction()
        {
            RTEditor.EditorCamera.Instance.SendMessage(
                methodName: "OnFocus",
                options: SendMessageOptions.RequireReceiver
            );
        }
    }
}