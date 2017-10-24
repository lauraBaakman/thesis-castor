using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using SimpleFileBrowser;

public class FragmentsMenuBoxController : MonoBehaviour
{

	public GameObject parentObject;
	public GameObject fragmentListElementPrefab;

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
		GameObject fragment = addFragmentGameObject (path);

		GameObject listElement = Instantiate (Resources.Load ("FragmentListElement")) as GameObject;
	}

	private GameObject addFragmentGameObject (string path)
	{
		Fragment fragment = FractureFragments.Instance.AddFragmentFromFile (path);
		GameObject fragmentGameObject = fragment.GameObject ();
		fragmentGameObject.transform.parent = parentObject.transform;

		//Scale the mesh
		Debug.Log ("Fragments are scaled with a factor 1000 for now.");
		fragmentGameObject.transform.localScale = new Vector3 (1000, 1000, 1000);		

		return fragmentGameObject;
	}
}
