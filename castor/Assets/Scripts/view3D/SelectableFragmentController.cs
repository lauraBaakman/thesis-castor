using UnityEngine;

[RequireComponent(typeof(FragmentController))]

public class SelectableFragmentController : MonoBehaviour
{
    GameObject Ghost;

    private void Awake()
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
        Debug.Log("Not Selected Object: Mouse Down!");
    }

    public void UpdateParent(GameObject parent){
        parent.AddComponent<MeshCollider>();
    }
}