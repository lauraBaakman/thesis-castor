using UnityEngine;

public class FragmentsController : MonoBehaviour
{

    void Start()
    {

    }

    private void Awake()
    {
    }

    /// <summary>
    /// Adds a fragment to the visible fragments.
    /// </summary>
    /// <param name="fragment">Fragment to add.</param>
    public FragmentController AddFragment(Fragment fragment)
    {

        GameObject fragmentGameObject = new GameObject(fragment.Name);

        FragmentController controller = fragmentGameObject.AddComponent<FragmentController>();
        fragmentGameObject.transform.parent = gameObject.transform;

        return controller;
    }
}
