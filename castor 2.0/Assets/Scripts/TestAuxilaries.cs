using DoubleConnectedEdgeList;
using UnityEngine;

public static class TestAux
{
    static System.Random random = new System.Random();

    public static Vector3 RandomPosition()
    {
        return new Vector3(Random.value, Random.value, Random.value);
    }

    public static Vertex RandomVertex()
    {
        return new Vertex(RandomPosition());
    }

    public static HalfEdge RandomHalfEdge()
    {
        return new HalfEdge(RandomVertex());
    }

    public static Face RandomFace()
    {
        return new Face(random.Next());
    }

    public static Registration.Point RandomPoint()
    {
        return new Registration.Point(RandomPosition(), RandomNormal());
    }

    public static Vector3 RandomNormal()
    {
        Vector3 normal = RandomPosition();
        normal.Normalize();
        return normal;
    }
}

