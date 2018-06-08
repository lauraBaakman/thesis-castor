namespace Registration
{
	namespace Messages
	{
		public interface IICPListener
		{
			void OnPreparationStepCompleted(ICPPreparationStepCompletedMessage message);

			void OnStepCompleted(ICPStepCompletedMessage message);

			void OnICPTerminated(ICPTerminatedMessage message);
		}

		public interface IICPStartEndListener
		{
			void OnICPStarted(ICPStartedMessage message);

			void OnICPTerminated(ICPTerminatedMessage message);
		}
	}

}

