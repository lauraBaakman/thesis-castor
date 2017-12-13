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

		string fragmentName = Path.GetFileNameWithoutExtension(path);

        //Create Fragment 3D View GameObject
            // 1. Read Mesh
            Mesh mesh = ObjImporter.ImportFile(path);

            // 2. Create Fragment
			Fragment fragmentData = new Fragment(mesh, name);
            
            // 3. Create Game Object for the Mesh
			// 4. Set Parent
			fragmentGameObject.transform.parent = FragmentParentObject.transform;

			// 5. Scale mesh
			Debug.Log("Fragments are scaled with a factor 1000 for now.");
			fragmentGameObject.transform.localScale = new Vector3(1000, 1000, 1000);

        //Create Fragment List Element
			// 1. Create Game Object for List Element
			GameObject listElement = Instantiate(Resources.Load("FragmentListElement")) as GameObject;
			
			// 2. Set Name
			listElement.name = fragmentName + " list element";
			
			// 3. Set Parent
			listElement.transform.SetParent(FragmentListView.transform);

			// 4. Create Controller
			FragmentListElementController controller = listElement.GetComponent<FragmentListElementController>();
			
			// 5. Set name of Controller
			controller.SetName(fragmentName);

        // Add Fragment to the Big Fragment List
        FractureFragments.GetInstance.AddFragment(fragmentData);
    }
}
