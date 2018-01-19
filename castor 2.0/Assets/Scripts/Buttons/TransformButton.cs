using UnityEngine;
using UnityEngine.UI;

using RTEditor;

namespace Buttons
{
    public class TransformButton : AbstractButton, Fragments.ISelectionControllerListener
    {
        private Button Button;

        public RTEditor.GizmoType WidgetType;

        public string InputName;

        protected override void Awake()
        {
            base.Awake();

            Button = GetComponent<Button>();
        }

        public override void OnClick()
        {
            ToggleWidget();
        }

        public void OnEnable()
        {
            OnNumberOfSelectedObjectsChanged(RTEditor.EditorObjectSelection.Instance.NumberOfSelectedObjects);
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

        protected override void DetectKeyBoardShortCut()
        {
            if (Input.GetButtonDown(InputName)) ToggleWidget();
        }
    }
}

