using UnityEngine;
using System;
using Fragment;

namespace Buttons
{
	public class TestExpectedTransformButton : AbstractButton
	{
		public Vector3 ExpectedRotationEulerUnity;
		public Vector3 PivotUnity;
		//public Vector3 ExpectedTranslationUnity;
		//public Vector3 AppliedRotationBlender;

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
				if (!childTransform.name.StartsWith("lock", StringComparison.CurrentCulture) &&
					!childTransform.name.StartsWith("Selected", StringComparison.CurrentCulture) &&
					!childTransform.name.StartsWith("ICP", StringComparison.CurrentCulture)
				   )
				{
					this.MoveObject = childTransform.gameObject;
					break;
				}
			}
		}

		private Quaternion BlenderToUnityRotation(Vector3 eulerAngles)
		{
			//https://answers.unity.com/questions/977027/problem-with-rotation-order-of-euler-angles-when-i.html
			//https://gamedev.stackexchange.com/questions/140579/euler-right-handed-to-quaternion-left-handed-conversion-in-unity         
			Quaternion xRot = Quaternion.AngleAxis(eulerAngles.x, Vector3.right);
			Quaternion yRot = Quaternion.AngleAxis(-eulerAngles.y, Vector3.forward);
			Quaternion zRot = Quaternion.AngleAxis(-eulerAngles.z, Vector3.down);
			return zRot * xRot * yRot;
		}

		private void ApplyTransformation()
		{
			//Quaternion q = BlenderToUnityRotation(AppliedRotationBlender);
			PivotController pivotController = MoveObject.GetComponentInChildren<PivotController>();
			pivotController.PivotInWorldSpace = PivotUnity;

			TransformController transformController = MoveObject.GetComponent<TransformController>();
			transformController.RotateFragment(Quaternion.Euler(ExpectedRotationEulerUnity), Fragments.transform);
		}
	}
}