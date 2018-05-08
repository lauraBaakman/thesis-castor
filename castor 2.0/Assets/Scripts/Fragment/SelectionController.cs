using Registration.Messages;
using RTEditor;
using UnityEngine;

namespace Fragment
{
	public class SelectionController : MonoBehaviour, IFragmentStateElementToggled, RTEditor.IRTEditorEventListener, IICPStartEndListener
	{

		private GameObject SelectedFragments;
		private GameObject Fragments;

		private static string SelectedFragmentsGOName = "Selected Fragments";

		public bool Selectable
		{
			get { return selectable; }
			set
			{
				selectable = value;
				if (!selectable) DeselectFragment(allowUndoRedo: false);
			}
		}
		private bool selectable;

		private void Start()
		{
			Fragments = transform.root.gameObject;

			SelectedFragments = Fragments.FindChildByName(SelectedFragmentsGOName);
			Debug.Assert(SelectedFragments, "Could not find the gameobject with name " + SelectedFragmentsGOName);

			Selectable = true;
		}

		private void DeselectFragment(bool allowUndoRedo)
		{
			EditorObjectSelection.Instance.RemoveObjectFromSelection(gameObject, allowUndoRedo);
		}

		private void ObjectHasBeenSelected()
		{
			SendMessage(
				methodName: "OnToggleSelectionState",
				value: true,
				options: SendMessageOptions.RequireReceiver
			);
		}

		private void ObjectHasBeenDeselected()
		{
			SendMessage(
				methodName: "OnToggleSelectionState",
				value: false,
				//Breaks if we are deselecting the fragment before destroying it
				options: SendMessageOptions.DontRequireReceiver
			);
		}

		#region IFragmentStateElementToggled
		public void OnToggledLockedState(bool locked) { }

		public void OnToggleSelectionState(bool selected)
		{
			gameObject.transform.parent = selected ? SelectedFragments.transform : Fragments.transform;
		}
		#endregion

		#region IRTEditorEventListener
		public bool OnCanBeSelected(ObjectSelectEventArgs selectEventArgs)
		{
			return Selectable;
		}

		public void OnSelected(ObjectSelectEventArgs selectEventArgs)
		{
			ObjectHasBeenSelected();
		}

		public void OnDeselected(ObjectDeselectEventArgs deselectEventArgs)
		{
			ObjectHasBeenDeselected();
		}

		public void OnAlteredByTransformGizmo(Gizmo gizmo) { }
		#endregion

		#region IICPStartEndListener
		public void OnICPStarted()
		{
			Selectable = false;
		}

		public void OnICPTerminated(ICPTerminatedMessage message)
		{
			Selectable = true;
		}
		#endregion
	}

}

