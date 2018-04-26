using UnityEngine;
namespace Buttons
{
    namespace RegistrationButtons
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
                registerer.RunUntilTermination();
            }

            protected override bool HasDetectedKeyBoardShortCut()
            {
                return Input.GetKeyDown(KeyCode.Return);
            }
        }
    }

}

