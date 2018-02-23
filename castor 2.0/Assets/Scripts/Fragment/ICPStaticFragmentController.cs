using UnityEngine;
using Registration;
using System.Collections.Generic;

namespace Fragment
{
    /// <summary>
    /// Pass ICP messages through to the children of this object.
    /// </summary>
    public class ICPStaticFragmentController : MonoBehaviour, IICPListener
    {
        public bool Active = false;
        public List<GameObject> Listeners = new List<GameObject>();

        #region ICPListener
        public void OnICPCorrespondencesChanged(ICPCorrespondencesChanged message) { }

        public void OnICPPointsSelected(ICPPointsSelectedMessage message)
        {
            SendMessageToListeners("OnICPPointsSelected", message, SendMessageOptions.RequireReceiver);
        }

        public void OnICPTerminated(ICPTerminatedMessage message)
        {
            SendMessageToListeners("OnICPTerminated", message, SendMessageOptions.RequireReceiver);
        }

        public void OnPreparationStepCompleted(ICPPreparationStepCompletedMessage message)
        {
            //The static fragment does not care about the completion of the prepartion step.
        }

        public void OnStepCompleted() { }
        #endregion ICPListener

        private void SendMessageToListeners(string methodName, object message, SendMessageOptions option)
        {
            //only notify the listeners if the controller is active
            if (!Active) return;
            foreach (GameObject listener in Listeners)
            {
                listener.SendMessage(methodName, message, option);
            }
        }
    }
}

