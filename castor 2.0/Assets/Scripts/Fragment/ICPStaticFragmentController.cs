using Registration.Messages;

namespace Fragment
{
    /// <summary>
    /// Pass ICP messages through to the children of this object.
    /// </summary>
    public class ICPStaticFragmentController : ICPAbstractFragmentController
    {
        public override void OnPreparationStepCompleted(ICPPreparationStepCompletedMessage message)
        {
            if (message.IsFirstPreparationStep()) SendMessageToListeners("OnPreparationStepCompleted", message);
        }

        public override void OnStepCompleted(ICPStepCompletedMessage message) { }
    }
}

