using UnityEngine;
using UnityEngine.UI;

using RTEditor;

namespace Buttons
{
    public class TranslationButton : AbstractButton
    {
        private Button Button;
        private RTEditor.GizmoType GizmoType = GizmoType.Translation;

        protected override void Awake()
        {
            base.Awake();

            Button = GetComponent<Button>();
            Button.interactable = false;
        }

        public override void OnClick()
        {
            ToggleWidget();
        }

        private void ToggleWidget()
        {
            bool isActive = EditorGizmoSystem.Instance.IsGizmoActive(GizmoType);
            string method = isActive ? "DeactivateAllGizmoObjects" : "OnChangeActiveGizmo";
            EditorGizmoSystem.Instance.SendMessage(
                    methodName: method,
                    value: GizmoType,
                    options: SendMessageOptions.RequireReceiver
                );
        }

        protected override void DetectKeyBoardShortCut()
        {
            if (Input.GetButtonDown("Show Translation Widget")) ToggleWidget();
        }

        public void OnNumberOfSelectedObjectsChanged(int currentCount)
        {
            Button.interactable = ShouldButtonBeInteractable(currentCount) ? true : false;
        }

        private bool ShouldButtonBeInteractable(int numSelectedObjects)
        {
            return numSelectedObjects >= 1;
        }
    }
}
 