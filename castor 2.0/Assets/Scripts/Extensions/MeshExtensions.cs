using UnityEngine;

public static class MeshExtensions
{
    public static IO.WriteResult ToFile(this Mesh mesh, string path)
    {
        return IO.ObjFile.Write(mesh, path);
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

    public static void SetWindingOrderToCCW(this Mesh mesh)
    {
        int idxA, idxB, idxC;
        Utils.TriangleUtils.WindingOrder windingOrder;

        int[] triangles = mesh.triangles;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            idxA = triangles[i + 0];
            idxB = triangles[i + 1];
            idxC = triangles[i + 2];

            windingOrder = Utils.TriangleUtils.DetermineWindingOrder(mesh.vertices[idxA], mesh.vertices[idxB], mesh.vertices[idxC]);
            if (windingOrder == Utils.TriangleUtils.WindingOrder.ClockWise)
            {
                //Swap the first and second vertex
                triangles[i + 0] = idxB;
                triangles[i + 1] = idxA;
            }
        }
        mesh.triangles = triangles;
    }
}