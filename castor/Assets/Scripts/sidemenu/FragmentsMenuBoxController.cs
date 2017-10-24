using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using SimpleFileBrowser;

public class FragmentsMenuBoxController : MonoBehaviour
{

	public GameObject BoneFragmentParentObject;
	public GameObject FragmentListView;

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
		Fragment fragment = addFragmentGameObject (path);

		addFragmentListElement (fragment);
	}

	private Fragment addFragmentGameObject (string path)
	{
		Fragment fragment = FractureFragments.Instance.AddFragmentFromFile (path);
		GameObject fragmentGameObject = fragment.GameObject ();
		fragmentGameObject.transform.parent = BoneFragmentParentObject.transform;

		//Scale the mesh
		Debug.Log ("Fragments are scaled with a factor 1000 for now.");
		fragmentGameObject.transform.localScale = new Vector3 (1000, 1000, 1000);		

		return fragment;
	}

	private void addFragmentListElement (Fragment fragment)
	{
		GameObject listElement = Instantiate (Resources.Load ("FragmentListElement")) as GameObject;
		listElement.name = fragment.name + " list element";
		listElement.transform.SetParent (FragmentListView.transform);		
	}
}
