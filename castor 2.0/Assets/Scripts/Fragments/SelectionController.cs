using UnityEngine;
using System.Collections.Generic;

namespace Fragments
{
	public class SelectionController : MonoBehaviour
	{
		public List<GameObject> Listeners;

		/// <summary>
		/// Is called when list of children of this object changes.
		/// </summary>
		private void OnTransformChildrenChanged()
		{
			NotifyListeners();
		}

		private void NotifyListeners()
		{
			foreach (GameObject listener in Listeners)
			{
				listener.SendMessage(
					methodName: "OnNumberOfSelectedObjectsChanged",
					value: transform.childCount,
					options: SendMessageOptions.DontRequireReceiver
				);
			}
		}

	}

	public interface ISelectionControllerListener
	{
		void OnNumberOfSelectedObjectsChanged(int currentCount);
	}
}

