using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buttons
{
    public class RegistrationPlayButton : AbstractRegistrationButton
    {
        protected override void Awake()
        {
            base.Awake();

            Button.interactable = false;
        }

        protected override void ExecuteButtonAction()
        {
            if (registerer == null) return;
            while (!registerer.HasTerminated)
            {
                registerer.PrepareStep();
                registerer.Step();
            }
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            // Has no keyboard short cut
            return false;
        }
    }
}

