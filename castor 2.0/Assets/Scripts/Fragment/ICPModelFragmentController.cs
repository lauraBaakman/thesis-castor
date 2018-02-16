using UnityEngine;
using System.Collections;
using Registration;
using System.Collections.Generic;

namespace Fragment
{
    /// <summary>
    /// Pass ICP messages through to the children of this object.
    /// </summary>
    public class ICPModelFragmentController : MonoBehaviour, IICPListener
    {
        public List<GameObject> listeners = new List<GameObject>();

        #region ICPListener
        public void OnICPCorrespondencesChanged(ICPCorrespondencesChanged message)
        {
            SendMessageToListeners("OnICPCorrespondencesChanged", message, SendMessageOptions.RequireReceiver);
        }

        public void OnICPPointsSelected(ICPPointsSelectedMessage message)
        {
            SendMessageToListeners("OnICPPointsSelected", message, SendMessageOptions.RequireReceiver);
        }

        public void OnICPTerminated(ICPTerminatedMessage message)
        {
            SendMessageToListeners("OnICPTerminated", message, SendMessageOptions.RequireReceiver);
        }

        public void OnPreparetionStepCompleted()
        {
            SendMessageToListeners("OnPreparetionStepCompleted", null, SendMessageOptions.RequireReceiver);
        }

        public void OnStepCompleted()
        {
            SendMessageToListeners("OnStepCompleted", null, SendMessageOptions.RequireReceiver);
        }
        #endregion ICPListener

        private void SendMessageToListeners(string methodName, object message, SendMessageOptions option)
        {
            foreach (GameObject listener in listeners)
            {
                listener.SendMessage(methodName, message, option);
            }
        }
    }

}

