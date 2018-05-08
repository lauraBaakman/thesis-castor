using UnityEngine;
using RTEditor;

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
			RTEditor.EditorObjectSelection.Instance.ClearSelection(allowUndoRedo: false);
		}

		private void DeleteFragment(GameObject fragment)
		{
			fragment.SetActive(false);
			fragment.DestroyAllChildren();
			Destroy(fragment, 2.0f);
		}

		protected override bool HasDetectedKeyBoardShortCut()
		{
			return (Input.GetKeyDown(KeyCode.Backspace) && InputHelper.IsAnyCtrlOrCommandKeyPressed()) || (Input.GetKeyDown(KeyCode.Delete));
		}

		public void OnNumberOfSelectedObjectsChanged(int currentCount)
		{
			this.Button.interactable = (currentCount >= 1);
		}
	}
}

