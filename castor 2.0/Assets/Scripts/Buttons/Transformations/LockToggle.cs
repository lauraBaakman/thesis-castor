using UnityEngine;
using RTEditor;
using System.Collections.Generic;
using System.Linq;

namespace Buttons
{
    public class LockToggle : AbstractToggle, Fragments.ISelectionControllerListener
    {
        public GameObject SelectedFragments;
        private static string inputName = "Toggle Lock";

        public void OnNumberOfSelectedObjectsChanged(int currentCount)
        {
            ///The lock/unlocking functionality is only available for single objects
            Toggle.interactable = (currentCount == 1);

            if (currentCount == 1) SetToggleState();
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
            //Only works if a single object is selected
            foreach (Transform child in SelectedFragments.transform)
            {
                //The toggle is on if the object is locked.
                bool locked = child.gameObject.GetComponent<Fragment.StateTracker>().State.Locked;
                OnToggleValueChanged(locked);
            }
        }
    }
}
