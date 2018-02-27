using Registration.Messages;

namespace Fragment
{
    /// <summary>
    /// Pass ICP messages through to the children of this object.
    /// </summary>
    public class ICPStaticFragmentController : ICPAbstractFragmentController
    {
        private static int firstIteration = 1;

        public override void OnPreparationStepCompleted(ICPPreparationStepCompletedMessage message)
        {
            if (IsFirstPreparationStep(message)) SendMessageToListeners("OnPreparationStepCompleted", message);
        }

        private bool IsFirstPreparationStep(ICPPreparationStepCompletedMessage message)
        {
            return message.IterationIndex == firstIteration;
        }

        public override void OnStepCompleted(ICPStepCompletedMessage message) { }
    }
}

