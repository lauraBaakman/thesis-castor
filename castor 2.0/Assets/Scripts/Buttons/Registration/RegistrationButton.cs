using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
	namespace RegistrationButtons
	{
		public class RegistrationButton : AbstractButton, Fragments.ISelectionControllerListener
		{
			private int RequiredNumberOfSelectedFragments = 2;

			public GraphicalUI.ICPGUIController ICPGUIController
			{
				set { icpGUIController = value; }
			}
			private GraphicalUI.ICPGUIController icpGUIController;

			public void OnEnable()
			{
				OnNumberOfSelectedObjectsChanged(RTEditor.EditorObjectSelection.Instance.NumberOfSelectedObjects);
			}

			/// <summary>
			/// Called once the number of selected objects has changed, the button should only be active when two objects are selected.
			/// </summary>
			/// <param name="currentCount">Current nmber of selected fragments.</param>
			public void OnNumberOfSelectedObjectsChanged(int currentCount)
			{
				Button.interactable = (currentCount == RequiredNumberOfSelectedFragments);
			}

			protected override void ExecuteButtonAction()
			{
				icpGUIController.InitializeICP();
			}

			protected override bool HasDetectedKeyBoardShortCut()
			{
				return RTEditor.InputHelper.IsAnyCtrlOrCommandKeyPressed() && Input.GetButtonDown("Register");
			}
		}
	}
}
