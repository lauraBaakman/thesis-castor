using UnityEngine;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using DoubleConnectedEdgeList;

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

        public HalfEdge Previous
        {
            get { return previous; }
            set { previous = value; }
        }
        private HalfEdge previous = null;

        public HalfEdge Next
        {
            get { return next; }
            set { next = value; }
        }
        private HalfEdge next = null;

        public Face IncidentFace
        {
            get { return incidentFace; }
            set { incidentFace = value; }
        }
        private Face incidentFace = null;

        public Vertex Destination { get { return HasTwin ? Twin.Origin : null; } }

        public bool HasDestination { get { return HasTwin; } }

        public bool HasTwin { get { return this.twin != null; } }

        public bool HasPrevious { get { return this.previous != null; } }

        public bool HasNext { get { return this.next != null; } }

        public bool HasIncidentFace { get { return this.incidentFace != null; } }

        public HalfEdge(Vertex origin)
        {
            this.Origin = origin;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash *= (31 + Origin.NonRecursiveGetHashCode());
            if (HasTwin) hash *= (31 + Twin.NonRecursiveGetHashCode());
            if (HasPrevious) hash *= (31 + Previous.NonRecursiveGetHashCode());
            if (HasNext) hash *= (31 + Next.NonRecursiveGetHashCode());
            if (HasIncidentFace) hash *= (31 + IncidentFace.NonRecursiveGetHashCode());
            return hash;
        }

        public int NonRecursiveGetHashCode()
        {
            int hash = 17;
            hash *= (31 + Origin.Position.GetHashCode());
            if (HasDestination) hash *= (31 + Destination.Position.GetHashCode());
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
                NonRecursiveEqualsAuxilary(this.Twin, other.Twin) &&
                NonRecursiveEqualsAuxilary(this.Previous, other.Previous) &&
                NonRecursiveEqualsAuxilary(this.Next, other.Next) &&
                NonRecursiveEqualsAuxilary(this.IncidentFace, other.IncidentFace)
            );
        }

        public bool NonRecursiveEquals(HalfEdge other)
        {
            if (other == null) return false;
            Debug.Log("HalfEdge:NonRecursiveEquals");
            return (
                this.Origin.Position.Equals(other.Origin.Position) &&
                NonRecursiveEqualsAuxilary(this.Twin, other.Twin) &&
                NonRecursiveEqualsAuxilary(this.Previous, other.Previous) &&
                NonRecursiveEqualsAuxilary(this.Next, other.Next)
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

        private bool NonRecursiveEqualsAuxilary(Face thisFace, Face otherFace)
        {
            if (thisFace == null && otherFace == null) return true;
            if (thisFace == null || otherFace == null) return false;
            return thisFace.NonRecursiveEquals(otherFace);
        }
    }
}

