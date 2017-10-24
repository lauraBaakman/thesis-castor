using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using SimpleFileBrowser;

public class FragmentsMenuBoxController : MonoBehaviour
{

	public GameObject parentObject;
	public Material material;

	public void Start ()
	{
		if (Application.isEditor) {
			Debug.Log ("Automatically loading some fragment for development.");
			onSelect (Path.Combine (Application.dataPath, "Models/andrewCube.obj"));
		}

	}

	public void onAddFragmentClick ()
	{
		FileBrowser.ShowLoadDialog (onSelect, onCancel);
	}

	private void onCancel ()
	{
		//No need to do anything if the file dialog is cancelled
	}

	private void onSelect (string path)
	{
		Fragment fragment = Fragment.FromFile (path);

		//Create the empty game object
		GameObject fragmentGO = new GameObject (fragment.name);
		fragmentGO.transform.parent = parentObject.transform;

		//Add the components to the empty object to show the mesh
		MeshRenderer renderer = fragmentGO.AddComponent<MeshRenderer> ();
		renderer.material = Material.Instantiate (material) as Material;

		MeshFilter filter = fragmentGO.AddComponent<MeshFilter> ();
		filter.mesh = fragment.mesh;

		//Scale the mesh
		fragmentGO.transform.localScale = new Vector3 (100, 100, 100);
	}
}
