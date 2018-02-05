using UnityEngine;
using System;

namespace Buttons
{
    public class RegistrationStepButton : AbstractRegistrationButton
    {
        private bool nextOperationIsStep = false;

        protected override void Awake()
        {
            base.Awake();

            Button.interactable = false;
        }

        protected override void ExecuteButtonAction()
        {
            if (registerer == null) return;


            Button.interactable = false;

            if (nextOperationIsStep)
            {
                ExecuteStep();
            } else
            {
                PrepareStep();
            }
        }

        private void ExecuteStep()
        {
            registerer.Step();
            nextOperationIsStep = false;
        }

        private void PrepareStep()
        {
            registerer.PrepareStep();
            nextOperationIsStep = true;
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }

}

