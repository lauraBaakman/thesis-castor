using System.Collections;

namespace Registration
{
    public interface IICPListener
    {
        void OnICPPointsSelected(ICPPointsSelectedMessage message);

        void OnICPCorrespondencesChanged(ICPCorrespondencesChanged message);

        IEnumerator OnICPFinished();
    }
}

