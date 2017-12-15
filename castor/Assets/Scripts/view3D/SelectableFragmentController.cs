using UnityEngine;

[RequireComponent(typeof(FragmentController))]

public class SelectableFragmentController : MonoBehaviour
{
    GameObject Ghost;

    private void Awake()
    {
        Ghost = new GameObject();
    }

    void Start()
    {
    }

    void Update()
    {

    }

    public void Populate(GameObject selectable)
    {
        Ghost.name = selectable.name + " ghost";
        Ghost.transform.parent = selectable.transform;

        CopyParenComponentsToGhost(selectable);

        IncreaseGhostSize();
        SetGhostMaterial();
    }

    private void CopyParenComponentsToGhost(GameObject parent){
        Ghost.AddComponent<MeshFilter>(parent.GetComponent<MeshFilter>());

        MeshRenderer ghostMeshRender = Ghost.AddComponent<MeshRenderer>(parent.GetComponent<MeshRenderer>());
        ghostMeshRender.material = parent.GetComponent<MeshRenderer>().material;
    }

    private void IncreaseGhostSize(){
        // Increase size of child object
        Ghost.transform.localScale = new Vector3(2, 2, 2);
    }

    private void SetGhostMaterial(){
        // Set material of child obeject to transperent: change render mode and change alpha value of albedo. 
    }
}