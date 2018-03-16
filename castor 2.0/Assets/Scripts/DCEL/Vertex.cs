using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace DoubleConnectedEdgeList
{
    /// <summary>
    /// Represents a vertex in a double connected edge list.
    /// </summary>
    public class Vertex : IEquatable<Vertex>, IComparable
    {
        /// <summary>
        /// The postion of the vertex.
        /// </summary>
        public readonly Vector3 Position;

        /// <summary>
        /// The edges that have their origin at this vertex.
        /// </summary>
        /// <value>The incident edges.</value>
        public ReadOnlyCollection<HalfEdge> IncidentEdges
        {
            get { return incidentEdges.AsReadOnly(); }
        }

        private List<HalfEdge> incidentEdges;

        public Vertex(Vector3 position)
        {
            this.Position = position;
            this.incidentEdges = new List<HalfEdge>();
        }

        /// <summary>
        /// Adds the incident edge to the vertex, if it has not already been added.
        /// </summary>
        /// <param name="edge">An incident edge.</param>
        public void AddIncidentEdge(HalfEdge edge)
        {
            if (!incidentEdges.Contains(edge))
            {
                incidentEdges.Add(edge);
            }
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="T:DoubleConnectedEdgeList.Vertex"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            HalfEdge.OriginDestinationComparer edgeComparer = new HalfEdge.OriginDestinationComparer();

            int hash = 17;
            hash *= (31 + Position.GetHashCode());
            foreach (HalfEdge edge in IncidentEdges)
            {
                hash *= (31 + edgeComparer.GetHashCode(edge));
            }
            return hash;
        }

        public override string ToString()
        {
            return string.Format("[Vertex: position={0}]", Position);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:DoubleConnectedEdgeList.Vertex"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:DoubleConnectedEdgeList.Vertex"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:DoubleConnectedEdgeList.Vertex"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return this.Equals(obj as Vertex);
        }

        /// <summary>
        /// Determines whether the specified <see cref="DoubleConnectedEdgeList.Vertex"/> is equal to the current <see cref="T:DoubleConnectedEdgeList.Vertex"/>.
        /// </summary>
        /// <param name="other">The <see cref="DoubleConnectedEdgeList.Vertex"/> to compare with the current <see cref="T:DoubleConnectedEdgeList.Vertex"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="DoubleConnectedEdgeList.Vertex"/> is equal to the current
        /// <see cref="T:DoubleConnectedEdgeList.Vertex"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(Vertex other)
        {
            return (
                this.Position.Equals(other.Position) &&
                this.IncidentEdges.UnorderedElementsAreEqual(other.IncidentEdges)
            );
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (GetType() != obj.GetType()) throw new ArgumentException("Object is not a Vertex.");

            Vertex vertex = obj as Vertex;

            return this.Position.CompareTo(vertex.Position);
        }

        public class PositionComparer : IEqualityComparer<Vertex>
        {
            public bool Equals(Vertex x, Vertex y)
            {
                if (x == null && y == null) return true;
                if (x == null || y == null) return false;

                return x.Position.Equals(y.Position);
            }

            public int GetHashCode(Vertex obj)
            {
                if (obj == null) return 0;

                return obj.Position.GetHashCode();
            }
        }

        public class KeyValueComparer : IEqualityComparer<KeyValuePair<Vector3, Vertex>>
        {
            public bool Equals(KeyValuePair<Vector3, Vertex> x, KeyValuePair<Vector3, Vertex> y)
            {
                return (
                    x.Key.Equals(y.Key) &&
                    x.Value.Equals(y.Value)
                );
            }

            public int GetHashCode(KeyValuePair<Vector3, Vertex> obj)
            {
                int hash = 17;
                hash *= (31 + obj.Key.GetHashCode());
                hash *= (31 + obj.Value.GetHashCode());
                return hash;
            }
        }
    }
}