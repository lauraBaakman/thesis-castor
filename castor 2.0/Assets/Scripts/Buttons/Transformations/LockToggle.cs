using UnityEngine;
using RTEditor;
using System.Collections.Generic;
using System.Linq;

namespace Buttons
{
    public class LockToggle : AbstractToggle, Fragments.ISelectionControllerListener
    {
        public GameObject SelectedFragments;

        public void OnNumberOfSelectedObjectsChanged(int currentCount)
        {
            ///The lock/unlocking functionality is only available for single objects
            Toggle.interactable = (currentCount == 1);

            if (currentCount == 1) SetToggleState();
        }

        public override void OnToggleValueChangedAction(bool isOn)
        {
            bool locked = !isOn;

            SelectedFragments.BroadcastMessage(
                methodName: "OnToggledLockedState",
                parameter: locked,
                options: SendMessageOptions.DontRequireReceiver
            );
        }

        protected override void OnEnableAction()
        {
            OnNumberOfSelectedObjectsChanged(EditorObjectSelection.Instance.NumberOfSelectedObjects);
        }

        private void SetToggleState()
        {
            //Only works if a single object is selected
            foreach (Transform child in SelectedFragments.transform)
            {
                bool unLocked = child.gameObject.GetComponent<Fragment.StateTracker>().State.UnLocked;
                OnToggleValueChanged(unLocked);
            }
        }
    }
}
