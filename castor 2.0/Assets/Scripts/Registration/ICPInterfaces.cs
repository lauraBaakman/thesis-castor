using System.Collections;

namespace Registration
{
    public interface IICPListener
    {
        void OnICPPointsSelected(ICPPointsSelectedMessage message);

        void OnICPCorrespondencesChanged(ICPCorrespondencesChanged message);

        void OnPreparetionStepCompleted();

        void OnStepCompleted();

        void OnICPTerminated(ICPTerminatedMessage message);
    }

    public interface IICPStartEndListener
    {
        void OnICPStarted();

        void OnICPTerminated(ICPTerminatedMessage message);
    }
}

