using UnityEngine;
using System;

public static class MeshExtensions
{
    public static bool ToFile(this Mesh mesh, string path)
    {
        Debug.Log("Returning True, but should be implemented!");

        return true;
    }

    public static Mesh Transform(this Mesh mesh, Matrix4x4 transformation)
    {
        Utils.MeshTransformer transformer = new Utils.MeshTransformer(transformation);
        transformer.Transform(mesh);

        return mesh;
    }
}