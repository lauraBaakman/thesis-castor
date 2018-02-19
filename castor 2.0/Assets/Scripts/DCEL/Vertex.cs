using System;
using UnityEngine;

namespace DoubleConnectedEdgeList
{
    public class Vertex : IEquatable<Vertex>
    {
        public Vector3 Position
        {
            get { return position; }
        }
        private Vector3 position;

        public Vertex(Vector3 position)
        {
            this.position = position;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + position.GetHashCode();
            return hash;
        }

        public int NonRecursiveGetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + position.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return string.Format("[Vertex: position={0}]", position);
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
            return this.position.Equals(other.position);
        }

        public bool NonRecursiveEquals(Vertex other)
        {
            Debug.Log("Vertex:NonRecursiveEquals");
            return this.position.Equals(other.position);
        }
    }
}