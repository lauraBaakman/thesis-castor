﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentsController : MonoBehaviour {

	private static Material DefaultMaterial;

	void Start () {
		DefaultMaterial = new Material (Shader.Find ("Standard"));
	}

	void Update () {
		
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
}
