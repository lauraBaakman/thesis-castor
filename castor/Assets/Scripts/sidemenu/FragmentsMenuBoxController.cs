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
    public void Start()
    {
        if (Application.isEditor)
        {
            Debug.Log("Automatically loading some fragment for development.");
            OnSelect(Path.Combine(Application.dataPath, "Models/andrewCube.obj"));
        }

    }

    /// <summary>
    /// On the add fragment click show the dialog that allows the user to select a file to read the fragment from.
    /// </summary>
    public void OnAddFragmentClick()
    {
        FileBrowser.ShowLoadDialog(OnSelect, OnCancel);
    }

    private void OnCancel()
    {
        //No need to do anything if the file dialog is cancelled
    }

    private void OnSelect(string path)
    {

        string fragmentName = ExtractNameFromPath(path);

        //Create Fragment 3D View GameObject
            // 1. Read Mesh
            Mesh mesh = ObjImporter.ImportFile(path);

            // 2. Create Fragment
			Fragment fragmentData = new Fragment(mesh, name);
            
            // 3. Create Game Object for the Mesh
			GameObject fragmentGameObject = fragmentData.GetGameObject();
			fragmentGameObject.transform.parent = FragmentParentObject.transform;

			Debug.Log("Fragments are scaled with a factor 1000 for now.");
			fragmentGameObject.transform.localScale = new Vector3(1000, 1000, 1000);

        //Create Fragment List Element
		AddFragmentListElement(fragmentName);

        // Add Fragment to the Big Fragment List
        FractureFragments.GetInstance.AddFragment(fragmentData);
    }

    private void AddFragmentGameObject(Fragment fragment)
    {
        GameObject fragmentGameObject = fragment.GetGameObject();
        fragmentGameObject.transform.parent = FragmentParentObject.transform;

        //Scale the mesh
        Debug.Log("Fragments are scaled with a factor 1000 for now.");
        fragmentGameObject.transform.localScale = new Vector3(1000, 1000, 1000);
    }

    private static string ExtractNameFromPath(string path)
    {
        string name = Path.GetFileNameWithoutExtension(path);
        return name;
    }

    private void AddFragmentListElement(string fragmentName)
    {
        GameObject listElement = Instantiate(Resources.Load("FragmentListElement")) as GameObject;
        listElement.name = fragmentName + " list element";
        listElement.transform.SetParent(FragmentListView.transform);

        FragmentListElementController controller = listElement.GetComponent<FragmentListElementController>();
        controller.SetName(fragmentName);
    }
}
