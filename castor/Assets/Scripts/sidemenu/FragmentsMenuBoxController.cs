using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using SimpleFileBrowser;

public class FragmentsMenuBoxController : MonoBehaviour
{
    /// <summary>
    /// The list that gives an overview of all fragments in the current reduction.
    /// </summary>
    public GameObject FragmentListView;

	/// <summary>
	/// The gameobject that is the parent of all fragments.
	/// </summary>
	public FragmentsController FragmentsController;

    /// <summary>
    /// Start this instance.
    /// </summary>
    public void Start()
    {
        if (Application.isEditor)
        {
            Debug.Log("Automatically loading some fragment for development.");
            OnSelectFile(Path.Combine(Application.dataPath, "Models/andrewCube.obj"));
        }

    }

    /// <summary>
    /// On the add fragment click show the dialog that allows the user to select a file to read the fragment from.
    /// </summary>
    public void OnAddFragmentClick()
    {
        FileBrowser.ShowLoadDialog(OnSelectFile, OnCancelFileSelectionDialog);
    }

    private void OnCancelFileSelectionDialog()
    {
        //No need to do anything if the file dialog is cancelled
    }

    private void OnSelectFile(string path)
    {
		// Create Fragment Data Object
		Fragment fragment = Fragments.GetInstance.AddFragmentFromFile (path);

        //Create Fragment 3D View GameObject
		FragmentController fragmentController = FragmentsController.AddFragment(fragment);

        //Create Fragment List Element GameObject
		FragmentListElementController listElementController = AddFragmentToListView(fragment.Name, fragmentController);

		fragmentController.Populate (listElementController);
		listElementController.Populate (fragmentController);
    }

	private FragmentListElementController AddFragmentToListView(string fragmentName, FragmentController fragmentController){
		GameObject listElement = Instantiate(Resources.Load("FragmentListElement")) as GameObject;

		listElement.name = BuildListElementName(fragmentName);
		listElement.transform.SetParent(FragmentListView.transform);

		FragmentListElementController controller = listElement.GetComponent<FragmentListElementController>();
		controller.ChangeFragmentName(fragmentName);

		return controller;
	}

	private string BuildListElementName(string fragmentName){
		return fragmentName + " list element";
	}
}
