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
        Debug.Log("SelectableFragmentController Start!");   
    }

    void Update()
    {

    }

    public void Populate(GameObject selectable){
        Ghost.name = selectable.name + " ghost";
        Ghost.transform.parent = selectable.transform;

        //Copy MeshFilter
        //Copy MeshRenderer

        // Increase size of child object

        // Set material of child obeject to transperent: change render mode and change alpha value of albedo. 
    }
}