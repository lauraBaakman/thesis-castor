using UnityEngine;
using System;

namespace DoubleConnectedEdgeList
{
    public class HalfEdge : IEquatable<HalfEdge>
    {
        public readonly Vertex origin;

        public HalfEdge(Vertex origin)
        {
            this.origin = origin;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + origin.NonRecursiveGetHashCode();
            return hash;
        }

        public int NonRecursiveGetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + origin.NonRecursiveGetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return string.Format("[HalfEdge: origin={0}]", origin);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return this.Equals(obj as HalfEdge);
        }

        public bool Equals(HalfEdge other)
        {
            Debug.Log("HalfEdge:Equals");
            return this.origin.NonRecursiveEquals(other.origin);
        }

        public bool NonRecursiveEquals(HalfEdge other)
        {
            Debug.Log("HalfEdge:NonRecursiveEquals");
            return this.origin.Position.Equals(other.origin.Position);
        }
    }
}