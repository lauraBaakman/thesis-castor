using UnityEngine;

public static class MeshExtensions
{
    public static bool ToFile(this Mesh mesh, string path)
    {
        return IO.FragmentFile.Write(mesh, path);
    }

    public static Mesh Transform(this Mesh mesh, Matrix4x4 transformation)
    {
        Utils.MeshTransformer transformer = new Utils.MeshTransformer(transformation);
        transformer.Transform(mesh);

        return mesh;
    }

    public static bool MeshEquals(this Mesh thisMesh, Mesh otherMesh)
    {
        return (
            Vector3ArrayEqual(thisMesh.vertices, otherMesh.vertices) &&
            Vector3ArrayEqual(thisMesh.normals, otherMesh.normals) &&
            thisMesh.triangles.OrderedElementsAreEqual(otherMesh.triangles)
        );
    }

    private static bool Vector3ArrayEqual(Vector3[] thisVertices, Vector3[] otherVertices)
    {
        if (thisVertices.Length != otherVertices.Length) return false;

        for (int i = 0; i < thisVertices.Length; i++)
        {
            if (thisVertices[i] != otherVertices[i]) return false;
        }
        return true;
    }

    private static int Vector3ArrayGetHashCode(Vector3[] list)
    {
        int hash = 17, extra = 0;
        foreach (Vector3 element in list)
        {
            hash *= (31 + element.GetHashCode() + (extra++));
        }
        return hash;
    }
}