using System.Collections;

namespace Registration
{
    public interface IICPListener
    {
        void OnICPPointsSelected(ICPPointsSelectedMessage message);

        void OnICPCorrespondencesDetermined(ICPCorrespondencesDeterminedMessage message);

        IEnumerator OnICPFinished();
    }
}

