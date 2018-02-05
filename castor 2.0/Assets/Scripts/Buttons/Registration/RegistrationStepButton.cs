using UnityEngine;
using System;

namespace Buttons
{
    namespace RegistrationButtons
    {
        public class RegistrationStepButton : AbstractRegistrationButton
        {
            private Action nextAction;

            protected override void Awake()
            {
                base.Awake();

                nextAction = PrepareStep;

                Button.interactable = false;
            }

            protected override void ExecuteButtonAction()
            {
                if (registerer == null) return;


                Button.interactable = false;

                nextAction();
            }

            private void ExecuteStep()
            {
                registerer.Step();
                nextAction = PrepareStep;
            }

            private void PrepareStep()
            {
                registerer.PrepareStep();
                nextAction = ExecuteStep;
            }

            protected override bool HasDetectedKeyBoardShortCut()
            {
                return Input.GetKeyDown(KeyCode.Space);
            }
        }
    }



}

