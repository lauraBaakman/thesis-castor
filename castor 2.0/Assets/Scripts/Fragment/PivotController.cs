using UnityEngine;

namespace Fragment
{
	/// <summary>
	/// The fragments do not necessarily have their pivot at their center, we add
	/// an empty gameobject placed at the center of the bounds of the parent object
	/// and use that to rotate the fragment.
	/// 
	/// Source: https://forum.unity.com/threads/change-an-objects-pivot-point.22885/#post-199538
	/// </summary>
	public class PivotController : MonoBehaviour
	{
		private bool userSetPivot;
		private Vector3 pivot;

		void Start()
		{
			if (!userSetPivot)
			{
				Vector3 parentCenter = GetParentCenter(transform.parent.gameObject);
				PivotInLocalSpace = parentCenter;
			}
			else this.pivotInWorldSpace = this.pivot;
		}

		private void Update()
		{
			if (userSetPivot)
			{
				this.pivotInWorldSpace = this.pivot;
				userSetPivot = false;
			}
		}

		public void SetPivot(Vector3 worldSpacePivot)
		{
			this.userSetPivot = true;
			this.pivot = worldSpacePivot;
			this.pivotInWorldSpace = worldSpacePivot;
		}

		private Vector3 pivotInWorldSpace
		{
			set
			{
				Debug.Log("Setting PivotInWorldSpace to: " + value);
				this.transform.position = value;
			}
		}

		public Vector3 PivotInWorldSpace
		{
			get { return transform.position; }
		}

		private Vector3 PivotInLocalSpace
		{
			get { return transform.localPosition; }
			set
			{
				Debug.Log("Setting PivotInLocalSpace to: " + value);
				transform.localPosition = value;
			}
		}

		private Vector3 GetParentCenter(GameObject parent)
		{
			Renderer meshRenderer = parent.GetComponent<MeshRenderer>();
			Debug.Assert(meshRenderer, "The parent needs to have a MeshRenderer.");

			Vector3 center = meshRenderer.bounds.center;
			return center;
		}
	}
}

