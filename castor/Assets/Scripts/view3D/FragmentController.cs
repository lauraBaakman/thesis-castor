using UnityEngine;

/// <summary>
/// Controlers the Unity representation of a Fragment.
/// </summary>
public class FragmentController : MonoBehaviour
{
    /// <summary>
    /// Gets or sets the fragment.
    /// </summary>
    /// <value>The fragment.</value>
    public Fragment Fragment { get; set; }

    /// <summary>
    /// The default material of any loaded fragment.
    /// </summary>
    public static Material DefaultMaterial;

    /// <summary>
    /// The list element controller associated with this fragment.
    /// </summary>
    private FragmentListElementController ListElementController;

    private Transform FragmentsTransform;
    private Transform SelectedFragmentsTransform;

    void Awake()
    {
        LoadDefaultMaterial();

        gameObject.AddComponent<MeshFilter>();

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = DefaultMaterial;

        gameObject.AddComponent<SelectableFragmentController>();
    }

    private void LoadDefaultMaterial(){
        DefaultMaterial = Resources.Load("Materials/DefaultMaterial", typeof(Material)) as Material;
        if (DefaultMaterial == null) Debug.LogError("Could not load the material: " + DefaultMaterial + ".");
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

    /// <summary>
    /// Toggles the visibility of the Fragment.
    /// </summary>
    /// <param name="toggle">If set to <c>true</c> the fragment is visible.</param>
    public void ToggleVisibility(bool toggle)
    {
        ToggleVisibilityLocally(toggle);

        //Deselect the object if it is selected and should be hidden.
        if (!toggle) ToggleSelection(false);

        ListElementController.ToggleVisibilityLocally(toggle);
    }

    /// <summary>
    /// Toggles the selectability of the Fragment
    /// </summary>
    /// <param name="toggle">If set to <c>true</c> the fragment can be selected.</param>
    public void ToggleSelectability(bool toggle)
    {
        gameObject.GetComponent<MeshCollider>().enabled = toggle;
        ListElementController.SendMessage(
            methodName: "ToggleSelectability",
            value: toggle
        );
    }

    /// <summary>
    /// Toggles the visibility of the Fragment as it relates to the 
    /// FragmentController, it does not handle the visibility of the Fragment 
    /// in the UI list.
    /// </summary>
    /// <param name="toggle">If set to <c>true</c> toggle.</param>
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

    /// <summary>
    /// Toggles the selection of the fragment.
    /// </summary>
    /// <param name="toggle">If set to <c>true</c> the object is selected.</param>
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
