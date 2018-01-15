using UnityEngine;

/// <summary>
/// Selectable fragment controller, component to attatch to fragments that 
/// should be selectable.
/// </summary>
[RequireComponent(typeof(FragmentController))]
public class SelectableFragmentController : MonoBehaviour
{
    /// <summary>
    /// The object that is used to indicate the the fragment is selected.
    /// </summary>
    GameObject Ghost;

    void Awake()
    {
        Ghost = new GameObject();
    }

    public void ToggleSelectionLocally(bool toggle){
        Ghost.SetActive(toggle);
    }

    public void Populate(GameObject parent)
    {
        SelectionGhostController ghostController = Ghost.AddComponent<SelectionGhostController>();
        ghostController.Populate(parent);

        UpdateParent(parent);

        ToggleSelectionLocally(false);
    }

    public void OnMouseDown()
    {
        GetComponent<FragmentController>().ToggleSelection(true);
    }

    public void UpdateParent(GameObject parent){
        parent.AddComponent<MeshCollider>();
    }
}