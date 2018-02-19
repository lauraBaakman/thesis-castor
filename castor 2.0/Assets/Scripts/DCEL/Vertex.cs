using System;
using UnityEngine;

namespace DoubleConnectedEdgeList
{
    public class Vertex : IEquatable<Vertex>
    {
        public readonly Vector3 Position;

        public Vertex(Vector3 position)
        {
            this.Position = position;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + Position.GetHashCode();
            return hash;
        }

        public int NonRecursiveGetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + Position.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return string.Format("[Vertex: position={0}]", Position);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return this.Equals(obj as Vertex);
        }

        public bool Equals(Vertex other)
        {
            Debug.Log("Vertex:Equals");
            return this.Position.Equals(other.Position);
        }

        public bool NonRecursiveEquals(Vertex other)
        {
            Debug.Log("Vertex:NonRecursiveEquals");
            return this.Position.Equals(other.Position);
        }
    }
}