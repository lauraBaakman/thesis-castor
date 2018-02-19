using UnityEngine;
using System;

namespace DoubleConnectedEdgeList
{
    public class HalfEdge : IEquatable<HalfEdge>
    {
        public readonly Vertex Origin;

        public HalfEdge Twin
        {
            get { return twin; }
            set { twin = value; }
        }
        private HalfEdge twin = null;

        public bool HasTwin
        {
            get { return this.twin != null; }
        }

        public Vertex Destination
        {
            get { return HasTwin ? Twin.Origin : null; }
        }

        public HalfEdge(Vertex origin)
        {
            this.Origin = origin;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash *= (31 + Origin.NonRecursiveGetHashCode());
            if (HasTwin) hash *= (31 + Twin.NonRecursiveGetHashCode());
            return hash;
        }

        public int NonRecursiveGetHashCode()
        {
            int hash = 17;
            hash *= (31 + Origin.Position.GetHashCode());
            if (HasTwin) hash *= (31 + Twin.Origin.Position.GetHashCode());
            return hash;
        }

        public override string ToString()
        {
            return string.Format("[HalfEdge: origin={0}, destination={1}]", Origin, Destination);
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
            return (
                this.Origin.NonRecursiveEquals(other.Origin) &&
                NonRecursiveEqualsAuxilary(this.Twin, other.Twin)
            );
        }

        public bool NonRecursiveEquals(HalfEdge other)
        {
            if (other == null) return false;
            Debug.Log("HalfEdge:NonRecursiveEquals");
            return (
                this.Origin.Position.Equals(other.Origin.Position) &&
                NonRecursiveEqualsAuxilary(this.Twin, other.Twin)
            );
        }

        private bool NonRecursiveEqualsAuxilary(HalfEdge thisEdge, HalfEdge otherEdge)
        {
            if (thisEdge == null && otherEdge == null) return true;
            if (thisEdge == null || otherEdge == null) return false;
            bool originEqual = thisEdge.Origin.Position.Equals(otherEdge.Origin.Position);
            bool destinationEqual = NonRecursiveEqualsAuxilary(thisEdge.Destination, otherEdge.Destination);
            return originEqual && destinationEqual;
        }

        private bool NonRecursiveEqualsAuxilary(Vertex thisVertex, Vertex otherVertex)
        {
            if (thisVertex == null && otherVertex == null) return true;
            if (thisVertex == null || otherVertex == null) return false;
            return thisVertex.Position.Equals(otherVertex.Position);
        }
    }
}