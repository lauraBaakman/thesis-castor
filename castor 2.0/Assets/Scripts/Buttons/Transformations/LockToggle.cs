using UnityEngine;
using RTEditor;
using System.Collections.Generic;

namespace Buttons
{
    public class LockToggle : AbstractToggle, Fragments.ISelectionControllerListener
    {
        public void OnNumberOfSelectedObjectsChanged(int currentCount)
        {
            ///The lock/unlocking functionality is only available for single objects
            Toggle.interactable = (currentCount == 1);

            if (currentCount == 1) SetToggleState();
        }

        public override void OnToggleValueChangedAction(bool isOn)
        {
            bool locked = !isOn;

            HashSet<GameObject> selectedObjects = EditorObjectSelection.Instance.SelectedGameObjects;
            foreach (GameObject selectedObject in selectedObjects)
            {
                selectedObject.SendMessage(
                    methodName: "OnToggledLockedState",
                    value: locked,
                    options: SendMessageOptions.RequireReceiver
                );
            }
        }

        protected override void OnEnableAction()
        {
            OnNumberOfSelectedObjectsChanged(EditorObjectSelection.Instance.NumberOfSelectedObjects);
        }
    }
}
