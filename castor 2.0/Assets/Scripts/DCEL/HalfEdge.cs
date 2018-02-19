using UnityEngine;
using System;

namespace DoubleConnectedEdgeList
{
    /// <summary>
    /// Represents a HalfEdge in a double connected Edge List.
    /// </summary>
    public class HalfEdge : IEquatable<HalfEdge>
    {
        /// <summary>
        /// The origin of the half edge.
        /// </summary>
        public readonly Vertex Origin;

        /// <summary>
        /// Gets or sets the twin of the half edge, i.e. the edge that runs parallel to this edge, but in the other direction.
        /// </summary>
        /// <value>The twin.</value>
        public HalfEdge Twin
        {
            get { return twin; }
            set { twin = value; }
        }
        private HalfEdge twin = null;

        /// <summary>
        /// Gets or sets the previous half edge that one encounteres in CCW traversal of the incident face.
        /// </summary>
        /// <value>The previous half edge</value>
        public HalfEdge Previous
        {
            get { return previous; }
            set { previous = value; }
        }
        private HalfEdge previous = null;

        /// <summary>
        /// Gets or sets the next half edge that one encounteres in CCW traversal of the incident face.
        /// </summary>
        /// <value>The next half edge</value>
        public HalfEdge Next
        {
            get { return next; }
            set { next = value; }
        }
        private HalfEdge next = null;

        /// <summary>
        /// The face that lies to the left when traversing this half edge from origin to destination.
        /// </summary>
        /// <value>The incident face.</value>
        public Face IncidentFace
        {
            get { return incidentFace; }
            set { incidentFace = value; }
        }
        private Face incidentFace = null;

        /// <summary>
        /// The destination of this half edge.
        /// </summary>
        /// <value>The destination.</value>
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

        /// <summary>
        /// Serves as a hash function for a <see cref="T:DoubleConnectedEdgeList.HalfEdge"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
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

        /// <summary>
        /// Serves as a hash function for a <see cref="T:DoubleConnectedEdgeList.HalfEdge"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for computing the hashcode of <see cref="T:DoubleConnectedEdgeList.Vertex"/> or <see cref="T:DoubleConnectedEdgeList.Face"/>
        /// hash table.</returns>
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

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:DoubleConnectedEdgeList.HalfEdge"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:DoubleConnectedEdgeList.HalfEdge"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:DoubleConnectedEdgeList.HalfEdge"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return this.Equals(obj as HalfEdge);
        }

        /// <summary>
        /// Determines whether the specified <see cref="DoubleConnectedEdgeList.HalfEdge"/> is equal to the current <see cref="T:DoubleConnectedEdgeList.HalfEdge"/>.
        /// </summary>
        /// <param name="other">The <see cref="DoubleConnectedEdgeList.HalfEdge"/> to compare with the current <see cref="T:DoubleConnectedEdgeList.HalfEdge"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="DoubleConnectedEdgeList.HalfEdge"/> is equal to the current
        /// <see cref="T:DoubleConnectedEdgeList.HalfEdge"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(HalfEdge other)
        {
            return (
                this.Origin.NonRecursiveEquals(other.Origin) &&
                NonRecursiveEqualsAuxilary(this.Twin, other.Twin) &&
                NonRecursiveEqualsAuxilary(this.Previous, other.Previous) &&
                NonRecursiveEqualsAuxilary(this.Next, other.Next) &&
                NonRecursiveEqualsAuxilary(this.IncidentFace, other.IncidentFace)
            );
        }

        /// <summary>
        /// Determines whether the specified <see cref="DoubleConnectedEdgeList.HalfEdge"/> is equal to the current <see cref="T:DoubleConnectedEdgeList.HalfEdge"/> without invoking the Equals methods of <see cref="T:DoubleConnectedEdgeList.Vertex"/> without invoking Equals methods of <see cref="T:DoubleConnectedEdgeList.Vertex"/>, <see cref="T:DoubleConnectedEdgeList.HalfEdge"/> or <see cref="T:DoubleConnectedEdgeList.Face"/>.
        /// Note that this function does not consider the IncidentFace in its comparison.
        /// </summary>
        /// <param name="other">The <see cref="DoubleConnectedEdgeList.HalfEdge"/> to compare with the current <see cref="T:DoubleConnectedEdgeList.HalfEdge"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="DoubleConnectedEdgeList.HalfEdge"/> is equal to the current
        /// <see cref="T:DoubleConnectedEdgeList.HalfEdge"/>; otherwise, <c>false</c>.</returns>
        public bool NonRecursiveEquals(HalfEdge other)
        {
            if (other == null) return false;
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

