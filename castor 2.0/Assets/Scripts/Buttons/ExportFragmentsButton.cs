using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buttons
{
    public class ExportFragmentsButton : AbstractButton
    {
        protected override void ExecuteButtonAction()
        {
            Debug.Log("Time to do some exporting!");
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return false;
        }
    }
}

