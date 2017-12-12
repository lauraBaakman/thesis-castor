using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using SimpleFileBrowser;

public class FragmentsMenuBoxController : MonoBehaviour
{

	/// <summary>
	/// The bone fragment parent object.
	/// </summary>
    public GameObject FragmentParentObject;
	/// <summary>
	/// The fragment list view.
	/// </summary>
	public GameObject FragmentListView;

	/// <summary>
	/// Start this instance.
	/// </summary>
	public void Start ()
	{
		if (Application.isEditor) {
			Debug.Log ("Automatically loading some fragment for development.");
			OnSelect (Path.Combine (Application.dataPath, "Models/andrewCube.obj"));
		}

	}

	/// <summary>
	/// On the add fragment click show the dialog that allows the user to select a file to read the fragment from.
	/// </summary>
	public void OnAddFragmentClick ()
	{
		FileBrowser.ShowLoadDialog (OnSelect, OnCancel);
	}

	private void OnCancel ()
	{
		//No need to do anything if the file dialog is cancelled
	}

	private void OnSelect (string path)
	{
		Fragment fragment = AddFragmentGameObject (path);

		AddFragmentListElement (fragment);
	}

	private Fragment AddFragmentGameObject (string path)
	{
		Fragment fragment = FractureFragments.GetInstance.AddFragmentFromFile (path);
		GameObject fragmentGameObject = fragment.GetGameObject ();
		fragmentGameObject.transform.parent = FragmentParentObject.transform;

		//Scale the mesh
		Debug.Log ("Fragments are scaled with a factor 1000 for now.");
		fragmentGameObject.transform.localScale = new Vector3 (1000, 1000, 1000);		

		return fragment;
	}

	private void AddFragmentListElement (Fragment fragment)
	{
		GameObject listElement = Instantiate (Resources.Load ("FragmentListElement")) as GameObject;
		listElement.name = fragment.Name + " list element";
		listElement.transform.SetParent (FragmentListView.transform);		

		FragmentListElementController controller = listElement.GetComponent<FragmentListElementController> ();
		controller.SetName (fragment.Name);
	}
}
