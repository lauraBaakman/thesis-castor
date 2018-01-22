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
            Debug.Log("Time to focus!");   
        }
    }
}