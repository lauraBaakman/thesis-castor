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
		GameObject fragment = Fragment.FromFile (path).GameObject ();
		fragment.transform.parent = parentObject.transform;

		//Scale the mesh
		fragment.transform.localScale = new Vector3 (100, 100, 100);
	}
}
