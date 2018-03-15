using UnityEngine;

public static class MeshExtensions
{
    public static bool ToFile(this Mesh mesh, string path)
    {
        Debug.Log("Returning True, should be implemented!");

        return true;
    }

    public static Mesh Transform(this Mesh mesh, Matrix4x4 transformation)
    {
        Utils.MeshTransformer transformer = new Utils.MeshTransformer(transformation);
        transformer.Transform(mesh);

        return mesh;
    }

    public static bool Equals(this Mesh thisMesh, Mesh otherMesh)
    {
        Debug.Log("Returning false, should be implemented Equals!");
        return false;
    }

    public static int GetHashCode(this Mesh mesh)
    {
        Debug.Log("TO be implemented");
        return 0;
    }
}