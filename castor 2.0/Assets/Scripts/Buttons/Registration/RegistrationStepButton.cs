using UnityEngine;

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
                registerer.Step();
                nextOperationIsStep = false;
            } else
            {
                registerer.PrepareStep();
                nextOperationIsStep = true;
            }
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }

}

