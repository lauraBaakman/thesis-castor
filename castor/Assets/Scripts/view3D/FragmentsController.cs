using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentsController : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
		
	}

	public void AddFragment(Fragment fragment){
		//TODO store material in private static

		GameObject gameObject = new GameObject (fragment.Name);	

		this.AttatchMesh (gameObject, fragment.Mesh);
		this.AttatchController (gameObject, fragment);

		gameObject.transform.parent = this.gameObject.transform;

		// Temporarily: Scale mesh
		Debug.Log("Fragments are scaled with a factor 1000 for now.");
		gameObject.transform.localScale = new Vector3(1000, 1000, 1000);
	}

	private void AttatchMesh(GameObject gameObject, Mesh mesh){
		AttatchMeshRenderer(gameObject);
		AttatchMeshFilter(gameObject, mesh);
	}

	private void AttatchMeshRenderer(GameObject gameObject){
		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer> ();
		renderer.material = new Material (Shader.Find ("Standard"));		
	}

	private void AttatchMeshFilter(GameObject gameObject, Mesh mesh){
		MeshFilter filter = gameObject.AddComponent<MeshFilter> ();
		filter.mesh = mesh;			
	}

	private void AttatchController(GameObject gameObject, Fragment fragment){
		FragmentController fragmentController = gameObject.AddComponent<FragmentController> ();
		fragmentController.Fragment = fragment;		
	}
}
