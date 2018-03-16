using DoubleConnectedEdgeList;
using Registration;
using UnityEngine;

namespace Tests
{
    public static class Auxilaries
    {
        static System.Random random = new System.Random();

        public static Vector3 RandomPosition()
        {
            return new Vector3(Random.value, Random.value, Random.value);
        }

        public static Vertex RandomVertex()
        {
            return new Vertex(RandomPosition(), random.Next());
        }

        public static HalfEdge RandomHalfEdge()
        {
            return new HalfEdge(RandomVertex());
        }

        public static Face RandomFace()
        {
            return new Face(random.Next(), RandomNormal());
        }

        public static Point RandomPoint()
        {
            return new Point(RandomPosition(), RandomNormal());
        }

        public static Vector3 RandomNormal()
        {
            Vector3 normal = RandomPosition();
            normal.Normalize();
            return normal;
        }

        public static Correspondence RandomCorrespondence()
        {
            return new Correspondence(
                staticPoint: RandomPoint(),
                modelPoint: RandomPoint()
            );
        }
    }
}