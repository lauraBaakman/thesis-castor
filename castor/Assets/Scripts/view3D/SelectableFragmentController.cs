using UnityEngine;

[RequireComponent(typeof(FragmentController))]

public class SelectableFragmentController : MonoBehaviour
{
    GameObject Ghost;

    private void Awake()
    {
        Ghost = new GameObject();
    }

    public void ToggleVisibility(bool toggle){
        Ghost.SetActive(toggle);
    }

    public void Populate(GameObject parent)
    {
        SelectionGhostController ghostController = Ghost.AddComponent<SelectionGhostController>();
        ghostController.Populate(parent);

        UpdateParent(parent);

        ToggleVisibility(false);
    }

    public void OnMouseDown()
    {
        Debug.Log("Mouse Down!");
    }

    public void UpdateParent(GameObject parent){
        parent.AddComponent<MeshCollider>();
    }
}