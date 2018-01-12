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

    private Transform FragmentsTransform;
    private Transform SelectedFragmentsTransform;

    void Awake()
    {
        DefaultMaterial = Resources.Load("Materials/DefaultMaterial", typeof(Material)) as Material;

        if (DefaultMaterial == null)
        {
            Debug.Log("Oh no!");
        }

        gameObject.AddComponent<MeshFilter>();

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = DefaultMaterial;

        gameObject.AddComponent<SelectableFragmentController>();
    }

    void Start()
    {
        gameObject.GetComponent<MeshFilter>().mesh = Fragment.Mesh;
        gameObject.GetComponent<SelectableFragmentController>().Populate(gameObject);
        FragmentsTransform = gameObject.transform.root;
        SelectedFragmentsTransform = FragmentsTransform.Find("Selected Fragments");
        if (!SelectedFragmentsTransform) Debug.LogError("Could not find Selected Fragments object.");
    }

    public void Update()
    {

    }

    public void ToggleVisibility(bool toggle)
    {
        ToggleVisibilityLocally(toggle);

        //Deselect the object if it is selected and should be hidden.
        if (!toggle)
        {
            ToggleSelection(false);
        }
        ListElementController.ToggleVisibilityLocally(toggle);
    }

    public void ToggleSelectability(bool toggle)
    {
        gameObject.GetComponent<MeshCollider>().enabled = toggle;
        ListElementController.SendMessage(
            methodName: "ToggleSelectability",
            value: toggle
        );
    }

    public void ToggleVisibilityLocally(bool toggle)
    {
        gameObject.SetActive(toggle);
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

    public void ToggleSelection(bool toggle)
    {
        gameObject.GetComponent<SelectableFragmentController>().ToggleSelectionLocally(toggle);
        ListElementController.ToggleSelectionLocally(toggle);
        gameObject.transform.root.BroadcastMessage(
            methodName: "FragmentSelectionStateChanged",
            parameter: toggle,
            options: SendMessageOptions.DontRequireReceiver
        );
        ChangeParent(toggle);
    }

    private void ChangeParent(bool selected)
    {
        gameObject.transform.parent = selected ? SelectedFragmentsTransform : FragmentsTransform;
    }
}
