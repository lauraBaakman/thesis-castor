using UnityEngine;
using System.Collections;

namespace Buttons
{
    public class DeleteFragmentButton : AbstractButton, Fragments.ISelectionControllerListener
    {
        public GameObject SelectedFragments;

        protected override void Awake()
        {
            base.Awake();

            this.Button.interactable = false;
        }

        protected override void ExecuteButtonAction()
        {
            DeleteSelectedFragments();
        }

        private void DeleteSelectedFragments()
        {
            foreach (Transform childTransform in SelectedFragments.transform)
            {
                DeleteFragment(childTransform.gameObject);
            }
            RTEditor.EditorObjectSelection.Instance.ClearSelection(allowUndoRedo:false);
        }

        private void DeleteFragment(GameObject fragment)
        {
            fragment.SetActive(false);
            Destroy(fragment, 5.0f);
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return Input.GetKeyDown(KeyCode.Backspace) && RTEditor.InputHelper.IsAnyCtrlOrCommandKeyPressed();
        }

        public void OnNumberOfSelectedObjectsChanged(int currentCount)
        {
            this.Button.interactable = (currentCount >= 1);
        }
    }
}

