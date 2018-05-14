using UnityEngine;

namespace Buttons
{
	public class TestExpectedTransformButton : AbstractButton
	{
		protected override bool HasDetectedKeyBoardShortCut()
		{
			return false;
		}

		protected override void ExecuteButtonAction()
		{
			Debug.Log("HI");
		}
	}
}