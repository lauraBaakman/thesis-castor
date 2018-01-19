using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    public class TranslationButton : AbstractButton
    {

        Button Button;

        protected override void Awake()
        {
            base.Awake();

            Button = GetComponent<Button>();
            Button.interactable = false;
        }

        public override void OnClick()
        {
            ShowTranslationWidget();
        }

        private void ShowTranslationWidget()
        {
            RTEditor.EditorGizmoSystem.Instance.SendMessage(
                methodName: "OnChangeActiveGizmo",
                value: RTEditor.GizmoType.Translation,
                options: SendMessageOptions.RequireReceiver
            );
        }

        protected override void DetectKeyBoardShortCut()
        {
            if (Input.GetButton("Show Translation Widget")) ShowTranslationWidget();
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
