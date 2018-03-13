using UnityEngine;
using UnityEngine.UI;

using RTEditor;

namespace Buttons
{
    public class TransformSpaceToggle : AbstractToggle, Fragments.ISelectionControllerListener
    {
        protected override void OnEnableAction()
        {
            OnNumberOfSelectedObjectsChanged(EditorObjectSelection.Instance.NumberOfSelectedObjects);
        }

        /// <summary>
        /// Toggling the worldspace only makes sense if objects are selected.
        /// </summary>
        /// <param name="currentCount">Current number of selected objects.</param>
        public void OnNumberOfSelectedObjectsChanged(int currentCount)
        {
            Toggle.interactable = (currentCount >= 1);
        }

        public override void OnToggleValueChangedAction(bool isOn)
        {
            EditorGizmoSystem.Instance.SendMessage(
                methodName: "OnChangeTransformSpace",
                value: isOn ? TransformSpace.Global : TransformSpace.Local,
                options: SendMessageOptions.RequireReceiver
            );
        }
    }
}

