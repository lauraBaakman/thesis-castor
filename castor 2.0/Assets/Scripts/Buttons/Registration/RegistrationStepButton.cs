using UnityEngine;

namespace Buttons
{
    public class RegistrationStepButton : AbstractRegistrationButton
    {
        private bool nextOperationIsStep = false;

        protected override void ExecuteButtonAction()
        {
            Debug.Log("RegistrationStepButton:ExecuteButtonAction");
            if (registerer == null) return;

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
            
            if(Input.GetKeyDown(KeyCode.Space)){
                Debug.Log("Detected Space!");    
            }
            return Input.GetKeyDown(KeyCode.Space);
        }
    }

}

