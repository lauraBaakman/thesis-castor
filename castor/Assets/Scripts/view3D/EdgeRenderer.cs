using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeRenderer : MonoBehaviour
{

    private Fragment Fragment;
    private Mesh Mesh;
    private Color edgeColor;

    void Start()
    {
        edgeColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        RenderEdges();

        Graphics.DrawMesh(Mesh, new Vector3(50, 50, 50), Quaternion.identity, new Material(Shader.Find("Standard")), 0);
    }

    public void Populate(Fragment fragment)
    {
        Fragment = fragment;
        Mesh = Fragment.Mesh;
    }

    private void RenderEdges()
    {
        int[] vertexIndices = Mesh.triangles;

        for (int i = 0; i < vertexIndices.Length; i += 3)
        {
            try
            {
                RenderTriangleEdges(Mesh.vertices[i + 0], Mesh.vertices[i + 1], Mesh.vertices[i + 2]);
            }
            catch {
                break;
            }
        }
    }

    private void RenderTriangleEdges(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2)
    {
        Debug.Log(vertex0 + " " + vertex1 + " " + vertex2);
    }
}
