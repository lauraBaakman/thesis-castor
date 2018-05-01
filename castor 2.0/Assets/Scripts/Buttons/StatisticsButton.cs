using UnityEngine;

namespace Buttons
{
    public class StatisticsButton : AbstractButton
    {
        protected override void ExecuteButtonAction()
        {
            Debug.Log("StatisticsButton:ExecuteButtonAction");
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return Input.GetButtonDown("Statistics");
        }
    }
}