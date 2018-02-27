using System.Collections;

namespace Registration
{
    namespace Messages
    {
        void OnPreparationStepCompleted(ICPPreparationStepCompletedMessage message);

        void OnStepCompleted();

        void OnICPTerminated(ICPTerminatedMessage message);
    }

    public interface IICPStartEndListener
    {
        void OnICPStarted();

        void OnICPTerminated(ICPTerminatedMessage message);
    }
}

