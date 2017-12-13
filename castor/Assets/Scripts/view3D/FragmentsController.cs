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

        FragmentController controller = fragmentGameObject.AddComponent<FragmentController> (); 
        fragmentGameObject.transform.parent = gameObject.transform;

		return controller;
	}
}
