using UnityEngine;
using UnityEngine.UI;

using RTEditor;

namespace Buttons
{
    public class TransformButton : AbstractButton, Fragments.ISelectionControllerListener
    {
        public GizmoType WidgetType;

        public string InputName;

        public void OnEnable()
        {
            OnNumberOfSelectedObjectsChanged(EditorObjectSelection.Instance.NumberOfSelectedObjects);
        }

        protected void ToggleWidget()
        {
            bool isActive = EditorGizmoSystem.Instance.IsGizmoActive(WidgetType);
            string method = isActive ? "DeactivateAllGizmoObjects" : "OnChangeActiveGizmo";
            EditorGizmoSystem.Instance.SendMessage(
                    methodName: method,
                    value: WidgetType,
                    options: SendMessageOptions.RequireReceiver
                );
        }

        public void OnNumberOfSelectedObjectsChanged(int currentCount)
        {
            Button.interactable = ShouldButtonBeInteractable(currentCount) ? true : false;
        }

        private bool ShouldButtonBeInteractable(int numSelectedObjects)
        {
            return numSelectedObjects >= 1;
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return Input.GetButtonDown(InputName) && InputHelper.IsNoModifierPressed();
        }

        protected override void ExecuteButtonAction()
        {
            ToggleWidget();
        }
    }
}


