using UnityEngine;
using System;
using System.Collections.Generic;

namespace DoubleConnectedEdgeList
{
    /// <summary>
    /// Represents a HalfEdge in a double connected Edge List.
    /// </summary>
    public class HalfEdge : IEquatable<HalfEdge>, IComparable
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

        public float Length
        {
            get { return (this.Origin.Position - this.Destination.Position).magnitude; }
        }

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
            OriginDestinationComparer edge = new OriginDestinationComparer();
            Vertex.SimpleComparer vertex = new Vertex.SimpleComparer();
            Face.MeshIdxAndNormalComparer face = new Face.MeshIdxAndNormalComparer();

            int hash = 17;
            hash *= (31 + vertex.GetHashCode(Origin));
            hash *= (31 + edge.GetHashCode(Twin));
            hash *= (31 + edge.GetHashCode(Previous));
            hash *= (31 + edge.GetHashCode(Next));
            hash *= (31 + face.GetHashCode(IncidentFace));
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
            OriginDestinationComparer edgeComparer = new OriginDestinationComparer();
            Vertex.SimpleComparer vertexComparer = new Vertex.SimpleComparer();
            Face.MeshIdxAndNormalComparer faceComparer = new Face.MeshIdxAndNormalComparer();

            return (
                vertexComparer.Equals(this.Origin, other.Origin) &&
                edgeComparer.Equals(this.Twin, other.Twin) &&
                edgeComparer.Equals(this.Previous, other.Previous) &&
                edgeComparer.Equals(this.Next, other.Next) &&
                faceComparer.Equals(this.IncidentFace, other.IncidentFace)
            );
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (GetType() != obj.GetType()) throw new ArgumentException("Object is not an HalfEdge.");

            HalfEdge other = obj as HalfEdge;

            int comparison;

            comparison = this.Origin.CompareTo(other.Origin);
            if (comparison != 0) return comparison;

            comparison = CompareToHelper(this.Twin, other.Twin);
            if (comparison != 0) return comparison;

            comparison = CompareToHelper(this.Next, other.Next);
            if (comparison != 0) return comparison;

            comparison = CompareToHelper(this.Previous, other.Previous);
            if (comparison != 0) return comparison;

            return comparison;
        }

        private int CompareToHelper(HalfEdge thisEdge, HalfEdge otherEdge)
        {
            if (thisEdge == null) return 0;
            return thisEdge.Origin.CompareTo(otherEdge.Origin);
        }

        public class OriginDestinationComparer : IEqualityComparer<HalfEdge>
        {
            private Vertex.SimpleComparer vertexComparer;

            public OriginDestinationComparer()
            {
                vertexComparer = new Vertex.SimpleComparer();
            }

            public bool Equals(HalfEdge x, HalfEdge y)
            {
                if (x == null && y == null) return true;
                if (x == null || y == null) return false;

                return (
                    vertexComparer.Equals(x.Origin, y.Origin) &&
                    vertexComparer.Equals(x.Destination, y.Destination)
                );
            }

            public int GetHashCode(HalfEdge obj)
            {
                if (obj == null) return 0;

                int hash = 17;
                hash *= (31 + vertexComparer.GetHashCode(obj.Origin));
                hash *= (31 + vertexComparer.GetHashCode(obj.Destination));
                return hash;
            }
        }
    }
}

