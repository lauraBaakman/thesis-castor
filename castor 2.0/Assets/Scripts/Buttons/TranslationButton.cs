using UnityEngine;

namespace Buttons
{
    public class TranslationButton : AbstractButton
    {

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
    }
}
