namespace Registration
{
    namespace Messages
    {
        public interface IICPListener
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

}

