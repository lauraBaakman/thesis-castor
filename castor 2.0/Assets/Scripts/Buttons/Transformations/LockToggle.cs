using UnityEngine;
using RTEditor;
using Fragments;

namespace Buttons
{
	public class LockToggle : AbstractToggle, Fragments.ISelectionControllerListener
	{
		public GameObject SelectedFragments;

		private LockController lockController;

		private static string inputName = "Toggle Lock";

		public override void Awake()
		{
			base.Awake();

			lockController = SelectedFragments.GetComponent<LockController>();
		}

		public void OnNumberOfSelectedObjectsChanged(int currentCount)
		{
			Toggle.interactable = (currentCount >= 1);
			if (currentCount >= 1) SetToggleState();
		}

		protected override bool HasDetectedKeyBoardShortCut()
		{
			return Input.GetButtonDown(inputName) && InputHelper.IsNoModifierPressed();
		}

		protected override void OnEnableAction()
		{
			OnNumberOfSelectedObjectsChanged(EditorObjectSelection.Instance.NumberOfSelectedObjects);
		}

		internal override void ExecuteToggleAction(bool isOn)
		{
			//The toggle is on if the object is locked.
			SelectedFragments.BroadcastMessage(
				methodName: "OnToggledLockedState",
				parameter: isOn,
				options: SendMessageOptions.DontRequireReceiver
			);
		}

		private void SetToggleState()
		{
			//If any fragment is selected the button shoud be on, i.e. locked.
			SetToggleSprites(lockController.AreAnySelectedFragmentsLocked());
		}
	}
}
