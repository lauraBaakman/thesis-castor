using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleFileBrowser;

public class FragmentsMenuBoxController : MonoBehaviour
{

	public GameObject objectToAddTo;
	public Material material;

	public void Start ()
	{
		onSelect ("/Users/laura/Repositories/castor/castor/Assets/Models/andrewCube.obj");
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
		Mesh holderMesh = ObjImporter.ImportFile (path);
		ObjImporter.AverageVertices (holderMesh);

		MeshRenderer renderer = objectToAddTo.AddComponent<MeshRenderer> ();
		renderer.material = Material.Instantiate (material) as Material;

		MeshFilter filter = objectToAddTo.AddComponent<MeshFilter> ();
		filter.mesh = holderMesh;
	}
}
