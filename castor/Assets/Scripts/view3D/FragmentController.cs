using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentController : MonoBehaviour
{
    /// <summary>
    /// Gets or sets the fragment.
    /// </summary>
    /// <value>The fragment.</value>
    public Fragment Fragment { get; set; }

    private FragmentListElementController ListElementController;

    private static Material DefaultMaterial;

    public bool Selected { get; set; }

    private void Awake()
    {
        DefaultMaterial = new Material(Shader.Find("Standard"));
    }

    void Start()
    {
        EdgeRenderer edgeRenderer = gameObject.AddComponent<EdgeRenderer>();
        edgeRenderer.Populate(Fragment);

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = DefaultMaterial;

        MeshFilter filter = gameObject.AddComponent<MeshFilter> ();
        filter.mesh = Fragment.Mesh;         

        // Temporarily: Scale mesh
        Debug.Log("Fragments are scaled with a factor 1000 for now.");
        gameObject.transform.localScale = new Vector3(1000, 1000, 1000);
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
}
