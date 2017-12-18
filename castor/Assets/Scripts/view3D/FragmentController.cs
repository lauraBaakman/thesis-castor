using UnityEngine;

public class FragmentController : MonoBehaviour
{
    /// <summary>
    /// Gets or sets the fragment.
    /// </summary>
    /// <value>The fragment.</value>
    public Fragment Fragment { get; set; }

    public static Material DefaultMaterial;

    private FragmentListElementController ListElementController;

    private void Awake()
    {
        DefaultMaterial = new Material(Shader.Find("Standard"));

        gameObject.AddComponent<MeshFilter>();

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = DefaultMaterial;

        gameObject.AddComponent<SelectableFragmentController>();

        // Temporarily: Scale mesh
        Debug.Log("Fragments are scaled with a factor 1000 for now.");
        gameObject.transform.localScale = new Vector3(1000, 1000, 1000);
    }

    void Start()
    {
        gameObject.GetComponent<MeshFilter>().mesh = Fragment.Mesh;
        gameObject.GetComponent<SelectableFragmentController>().Populate(gameObject);
    }

    public void Update()
    {

    }

    /// <summary>
    /// Deletes the fragment associated with this controller from the reduction.
    /// </summary>
    public void DeleteFragment()
    {
        bool succes = Fragments.GetInstance.RemoveFragment(Fragment);

        if (!succes)
        {
            Debug.Log("Could not delete the fragment with name: " + Fragment.Name);
            return;
        }

        ListElementController.Delete();

        Destroy(gameObject);
    }

    /// <summary>
    /// Populate the FragmentController.
    /// </summary>
    /// <param name="listElementController">List element controller associated with the Fragment controlled by this FragmentController.</param>
    public void Populate(Fragment fragment, FragmentListElementController listElementController)
    {
        this.Fragment = fragment;
        this.ListElementController = listElementController;
    }

    public void ToggleSelection(bool toggle){
        gameObject.GetComponent<SelectableFragmentController>().ToggleSelectionLocally(toggle);  
        ListElementController.ToggleSelectionLocally(toggle);
    }
}
