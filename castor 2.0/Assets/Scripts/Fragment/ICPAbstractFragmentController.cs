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

		public virtual void OnStepCompleted(ICPStepCompletedMessage message)
		{
			SendMessageToListeners("OnStepCompleted", message);
		}

		protected void SendMessageToListeners(string methodName, object message, SendMessageOptions option = SendMessageOptions.DontRequireReceiver)
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

