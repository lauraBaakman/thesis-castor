using UnityEngine;

public class FragmentsController : MonoBehaviour {

	private static Material DefaultMaterial;

	void Start () {
		
	}

    private void Awake()
    {
        DefaultMaterial = new Material(Shader.Find("Standard"));
    }

    /// <summary>
    /// Adds a fragment to the visible fragments.
    /// </summary>
    /// <param name="fragment">Fragment to add.</param>
    public FragmentController AddFragment(Fragment fragment){

		GameObject fragmentGameObject = new GameObject (fragment.Name);	

        this.AttatchMesh (fragmentGameObject, fragment.Mesh);
        FragmentController controller = this.AttatchController (fragmentGameObject, fragment);

        fragmentGameObject.transform.parent = gameObject.transform;

		// Temporarily: Scale mesh
		Debug.Log("Fragments are scaled with a factor 1000 for now.");
        fragmentGameObject.transform.localScale = new Vector3(1000, 1000, 1000);

		return controller;
	}

    private void AttatchMesh(GameObject fragmentGameObject, Mesh mesh){
        AttatchMeshRenderer(fragmentGameObject);
		AttatchMeshFilter(fragmentGameObject, mesh);
	}

    private void AttatchMeshRenderer(GameObject fragmentGameObject){
        MeshRenderer meshRenderer = fragmentGameObject.AddComponent<MeshRenderer> ();
		meshRenderer.material = DefaultMaterial;
	}

    private void AttatchMeshFilter(GameObject fragmentGameObject, Mesh mesh){
		MeshFilter filter = fragmentGameObject.AddComponent<MeshFilter> ();
		filter.mesh = mesh;			
	}

    private FragmentController AttatchController(GameObject fragmentGameObject, Fragment fragment){
		FragmentController controller = fragmentGameObject.AddComponent<FragmentController> ();
		controller.Fragment = fragment;		

		return controller;
	}
}
