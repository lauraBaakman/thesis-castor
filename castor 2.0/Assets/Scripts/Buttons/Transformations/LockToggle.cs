using UnityEngine;
using RTEditor;

namespace Buttons
{
    public class LockToggle : AbstractToggle, Fragments.ISelectionControllerListener
    {
        public void OnNumberOfSelectedObjectsChanged(int currentCount)
        {
            ///The lock/unlocking functionality is only available for single objects
            Toggle.interactable = (currentCount == 1);
        }

        public override void OnToggleValueChangedAction(bool isOn)
        {
            Debug.Log("LockToggle: OnToggleValueChangedAction");
        }

        protected override void OnEnableAction()
        {
            OnNumberOfSelectedObjectsChanged(EditorObjectSelection.Instance.NumberOfSelectedObjects);
        }
    }
}
