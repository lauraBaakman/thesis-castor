using UnityEngine;

[RequireComponent(typeof(FragmentController))]

public class SelectableFragmentController : MonoBehaviour
{
    private GameObject EdgeMeshGameObject;

    private Material DefaultMaterial;
    private float LineWidth = 5.0f;

    private void Awake()
    {
        DefaultMaterial = new Material(Shader.Find("Standard"));

        EdgeMeshGameObject = new GameObject(name + " edges");
        EdgeMeshGameObject.transform.parent = gameObject.transform;

        MeshRenderer meshRenderer = EdgeMeshGameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = DefaultMaterial;

        EdgeMeshGameObject.AddComponent<MeshFilter>();
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void Populate(Mesh objectMesh){
        EdgeMeshGameObject.GetComponent<MeshFilter>().mesh = BuildEdgeMesh(objectMesh);
    }

    private Mesh BuildEdgeMesh(Mesh fragmentMesh)
    {
        Debug.Log("Build Edge Mesh!");
        return null;
    }

}