﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace DoubleConnectedEdgeList
{
    /// <summary>
    /// Represents a vertex in a double connected edge list.
    /// </summary>
    public class Vertex : IEquatable<Vertex>
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
            if (!IncidentEdges.Contains(edge)) incidentEdges.Add(edge);
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="T:DoubleConnectedEdgeList.Vertex"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            int hash = 17;
            hash *= (31 + Position.GetHashCode());
            foreach (HalfEdge edge in IncidentEdges)
            {
                hash *= (31 + edge.NonRecursiveGetHashCode());
            }
            return hash;
        }

        /// <summary>
        /// Computes the hashcode of the Vertx without invoking GetHashCode methods of <see cref="T:DoubleConnectedEdgeList.Vertex"/>, <see cref="T:DoubleConnectedEdgeList.HalfEdge"/> or <see cref="T:DoubleConnectedEdgeList.Face"/>.
        /// </summary>
        /// <returns>The recursive get hash code.</returns>
        public int NonRecursiveGetHashCode()
        {
            int hash = 17;
            hash *= (31 + Position.GetHashCode());
            foreach (HalfEdge edge in IncidentEdges)
            {
                hash *= (31 + edge.Origin.Position.GetHashCode());
                if (edge.HasDestination) hash *= (31 + edge.Destination.Position.GetHashCode());
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
                NonRecursiveEqualsAuxilary(this.IncidentEdges, other.IncidentEdges)
            );
        }


        /// <summary>
        /// Determines whether the specified <see cref="DoubleConnectedEdgeList.Vertex"/> is equal to the current <see cref="T:DoubleConnectedEdgeList.Vertex"/> without invoking Equals methods of <see cref="T:DoubleConnectedEdgeList.Vertex"/>, <see cref="T:DoubleConnectedEdgeList.HalfEdge"/> or <see cref="T:DoubleConnectedEdgeList.Face"/>.
        /// </summary>
        /// <returns>The recursive get hash code.</returns>
        public bool NonRecursiveEquals(Vertex other)
        {
            return (
                this.Position.Equals(other.Position) &&
                NonRecursiveEqualsAuxilary(this.IncidentEdges, other.IncidentEdges)
            );
        }

        private bool NonRecursiveEqualsAuxilary(ReadOnlyCollection<HalfEdge> thisEdges, ReadOnlyCollection<HalfEdge> otherEdges)
        {
            IEnumerable<HalfEdge> inThisButNotInOther = thisEdges.Except(otherEdges);
            IEnumerable<HalfEdge> inOtherButNotInThis = otherEdges.Except(thisEdges);

            return (
                !inThisButNotInOther.Any() &&
                !inOtherButNotInThis.Any()
            );
        }
    }
}