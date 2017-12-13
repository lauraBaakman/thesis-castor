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
//			GameObject fragmentGameObject = fragmentData.GetGameObject();
			GameObject fragmentGameObject = new GameObject (fragmentName);
				
				// 3a. Attatch MeshRender Component
				MeshRenderer renderer = fragmentGameObject.AddComponent<MeshRenderer> ();
				renderer.material = new Material (Shader.Find ("Standard"));
				
				// 3b. Attatch MeshFilter Component
				MeshFilter filter = fragmentGameObject.AddComponent<MeshFilter> ();
				filter.mesh = mesh;		

				// 3c. Attatch FragmentComponent
				FragmentController fragmentController = fragmentGameObject.AddComponent<FragmentController> ();
				fragmentController.Fragment = fragmentData;

			// 4. Set Parent
			fragmentGameObject.transform.parent = FragmentParentObject.transform;

			// 5. Scale mesh
			Debug.Log("Fragments are scaled with a factor 1000 for now.");
			fragmentGameObject.transform.localScale = new Vector3(1000, 1000, 1000);

        //Create Fragment List Element
		AddFragmentToListView(fragmentName);

        FractureFragments.GetInstance.AddFragment(fragmentData);
    }

	private void AddFragmentToListView(string fragmentName){
		GameObject listElement = Instantiate(Resources.Load("FragmentListElement")) as GameObject;

		listElement.name = BuildListElementName(fragmentName);
		listElement.transform.SetParent(FragmentListView.transform);

		AttatchListElementController (listElement, fragmentName);
	}

	private string BuildListElementName(string fragmentName){
		return fragmentName + " list element";
	}

	private void AttatchListElementController(GameObject listElement, string fragmentName){
		FragmentListElementController controller = listElement.GetComponent<FragmentListElementController>();
		controller.ChangeFragmentName(fragmentName);				
	}

}
