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

		void Start()
		{
			Vector3 parentCenter = GetParentCenter(transform.parent.gameObject);
			PivotInLocalSpace = parentCenter;
		}

		public Vector3 PivotInWorldSpace
		{
			get { return transform.position; }
			set { transform.position = value; }
		}

		private Vector3 PivotInLocalSpace
		{
			get { return transform.localPosition; }
			set { transform.localPosition = value; }
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

