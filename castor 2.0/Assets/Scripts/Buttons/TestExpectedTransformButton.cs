using UnityEngine;
using System;

namespace Buttons
{
	public class TestExpectedTransformButton : AbstractButton
	{
		public Quaternion ExpectedRotation;
		public Vector3 ExpectedTranslation;

		public GameObject Fragments;

		public GameObject MoveObject;

		protected override bool HasDetectedKeyBoardShortCut()
		{
			return false;
		}

		protected override void ExecuteButtonAction()
		{
			FindMoveObject();
			ApplyTransformation();
		}

		private void FindMoveObject()
		{
			foreach (Transform childTransform in this.Fragments.transform)
			{
				if (childTransform.name.StartsWith("move_object", StringComparison.CurrentCulture))
				{
					this.MoveObject = childTransform.gameObject;
					break;
				}
			}
		}

		private void ApplyTransformation()
		{
			throw new NotImplementedException();
		}
	}
}