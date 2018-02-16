using UnityEngine;
using System.Collections;
using Registration;

namespace Fragment
{
    public class ICPPointController : MonoBehaviour, IICPListener
    {
        #region Correspondences
        public void OnICPCorrespondencesChanged(ICPCorrespondencesChanged message) { }
        #endregion

        #region Points
        public void OnICPPointsSelected(ICPPointsSelectedMessage message)
        {
            Debug.Log("Received Points!");
        }
        #endregion


        #region Progress
        public void OnICPTerminated(ICPTerminatedMessage message) { }

        public void OnPreparetionStepCompleted() { }

        public void OnStepCompleted() { }
        #endregion
    }

}

