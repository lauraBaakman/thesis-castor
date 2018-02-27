using Registration.Messages;

namespace Buttons
{
    namespace RegistrationButtons
    {
        public class StopButton : AbstractRegistrationButton
        {
            protected override void Awake()
            {
                base.Awake();

                Button.interactable = false;
            }

            protected override void ExecuteButtonAction()
            {
                if (registerer == null) return;

                if (!registerer.HasTerminated) registerer.Terminate(
                    ICPTerminatedMessage.TerminationReason.UserTerminated);
            }

            protected override bool HasDetectedKeyBoardShortCut()
            {
                // Has no keyboard short cut
                return false;
            }
        }
    }
}

