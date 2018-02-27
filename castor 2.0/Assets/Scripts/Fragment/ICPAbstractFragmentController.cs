using UnityEngine;
using Registration.Messages;
using System.Collections.Generic;

namespace Fragment
{
    public abstract class ICPAbstractFragmentController : MonoBehaviour, IICPListener
    {
        public bool Active = false;
        public List<GameObject> Listeners = new List<GameObject>();

        public virtual void OnICPTerminated(ICPTerminatedMessage message)
        {
            SendMessageToListeners("OnICPTerminated", message);
        }

        public virtual void OnPreparationStepCompleted(ICPPreparationStepCompletedMessage message)
        {
            SendMessageToListeners("OnPreparationStepCompleted", message);
        }

        public virtual void OnStepCompleted()
        {
            SendMessageToListeners("OnStepCompleted", null);
        }

        protected void SendMessageToListeners(string methodName, object message, SendMessageOptions option = SendMessageOptions.RequireReceiver)
        {
            //only notify the listeners if the controller is active
            if (!Active) return;

            foreach (GameObject listener in Listeners)
            {
                Debug.Assert(
                    listener.GetComponent<IICPListener>() != null,
                    "listeners of ICPModelFragmentController need to implement the IICPListener interface."
                );

                listener.SendMessage(methodName, message, option);
            }
        }
    }
}

