using UnityEngine;

public class SelectableFragmentController : MonoBehaviour
{

    private static Material DefaultMaterial;
    private GameObject EdgeMeshGameObject;

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

    private Mesh BuildEdgeMesh(Mesh objectMesh)
    {
        Debug.Log("Build Edge Mesh!");
        return null;
    }
}
