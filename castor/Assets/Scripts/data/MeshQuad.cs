using UnityEngine;

//Source: http://www.everyday3d.com/blog/index.php/2010/03/15/3-ways-to-draw-3d-lines-in-unity3d/
public class MeshQuad
{
    public const float DEFAULTLINEWIDTH = 4.0f;

    public Vector3[] Vertices;
    public int[] TriangleIndices;

    public MeshQuad()
    {
        Vertices = new Vector3[4];
        TriangleIndices = new int[6];
    }

    static MeshQuad FromLine(Vector3 start, Vector3 end, float lineWidth = DEFAULTLINEWIDTH) {
        MeshQuad quad = new MeshQuad();

        Vector3 planeNormal = Vector3.Cross(start, end);

        Vector3 side = Vector3.Cross(planeNormal, end - start);
        side.Normalize();

        quad.Vertices[0] = start + side * (lineWidth / 2.0f);
        quad.Vertices[1] = start + side * (lineWidth / -2.0f);
        quad.Vertices[2] = end + side * (lineWidth / 2.0f);
        quad.Vertices[3] = end + side * (lineWidth / -2.0f);

        quad.TriangleIndices[0] = 0;
        quad.TriangleIndices[1] = 1;
        quad.TriangleIndices[2] = 2;

        quad.TriangleIndices[3] = 1;
        quad.TriangleIndices[4] = 3;
        quad.TriangleIndices[5] = 2;

        return quad;
    }
}
