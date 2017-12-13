using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentsController : MonoBehaviour {

	private static Material DefaultMaterial;

	private FragmentController SelectedFragment;

	void Start () {
		DefaultMaterial = new Material (Shader.Find ("Standard"));
	}

	/// <summary>
	/// Adds a fragment to the visible fragments.
	/// </summary>
	/// <param name="fragment">Fragment to add.</param>
	public FragmentController AddFragment(Fragment fragment){

		GameObject gameObject = new GameObject (fragment.Name);	

		this.AttatchMesh (gameObject, fragment.Mesh);
		FragmentController controller = this.AttatchController (gameObject, fragment);

		gameObject.transform.parent = this.gameObject.transform;

		// Temporarily: Scale mesh
		Debug.Log("Fragments are scaled with a factor 1000 for now.");
		gameObject.transform.localScale = new Vector3(1000, 1000, 1000);

		return controller;
	}

	private void AttatchMesh(GameObject gameObject, Mesh mesh){
		AttatchMeshRenderer(gameObject);
		AttatchMeshFilter(gameObject, mesh);
	}

	private void AttatchMeshRenderer(GameObject gameObject){
		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer> ();
		renderer.material = DefaultMaterial;
	}

	private void AttatchMeshFilter(GameObject gameObject, Mesh mesh){
		MeshFilter filter = gameObject.AddComponent<MeshFilter> ();
		filter.mesh = mesh;			
	}

	private FragmentController AttatchController(GameObject gameObject, Fragment fragment){
		FragmentController controller = gameObject.AddComponent<FragmentController> ();
		controller.Fragment = fragment;		

		return controller;
	}

	/// <summary>
	/// Toggles the fragments selection. 
	/// </summary>
	/// <param name="fragment">The Fragment whose selection should be toggled.</param>
	/// <param name="toggle">If set to <c>true</c> the object is marked as selected, otherwise it is marked as not selected.</param>
	public void ToggleFragmentSelection(FragmentController fragment, bool toggle){
		if (toggle) {
			SelectedFragment = fragment;
		} else {
			SelectedFragment.Deselect ();
			SelectedFragment = null;
		}
	}
}
