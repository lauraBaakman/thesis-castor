using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Buttons
{
    public class ExperimentButton : AbstractButton
    {
        protected override void ExecuteButtonAction()
        {
            Debug.Log("Time to execute the experiment button action!");
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return Input.GetButtonDown("Experiment");
        }
    }
}
