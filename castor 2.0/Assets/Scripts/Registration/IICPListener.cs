using System.Collections;

namespace Registration
{
    public interface IICPListener
    {
        void OnICPPointsSelected(ICPPointsSelectedMessage message);

        void OnICPCorrespondencesChanged(ICPCorrespondencesDeterminedMessage message);

        IEnumerator OnICPFinished();
    }
}

