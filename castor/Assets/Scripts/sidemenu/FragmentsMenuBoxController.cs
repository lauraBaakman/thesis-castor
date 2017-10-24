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
		//Create the empty game object
		string objname = Path.GetFileNameWithoutExtension (path);
		GameObject fragment = new GameObject (objname);
		fragment.transform.parent = parentObject.transform;

		//Read the mesh
		Mesh holderMesh = ObjImporter.ImportFile (path);

		//Add the components to the empty object to show the mesh
		MeshRenderer renderer = fragment.AddComponent<MeshRenderer> ();
		renderer.material = Material.Instantiate (material) as Material;

		MeshFilter filter = fragment.AddComponent<MeshFilter> ();
		filter.mesh = holderMesh;

		//Scale the mesh
		fragment.transform.localScale = new Vector3 (100, 100, 100);
	}
}
