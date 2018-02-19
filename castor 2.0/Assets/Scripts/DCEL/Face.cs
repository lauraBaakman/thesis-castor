﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace DoubleConnectedEdgeList
{
    /// <summary>
    /// Represents a Face in double connected edge list.
    /// </summary>
    public class Face : IEquatable<Face>
    {
        /// <summary>
        /// The half edges on the boundary of this face in arbitrary order. 
        /// </summary>
        /// <value>The outer components.</value>
        public ReadOnlyCollection<HalfEdge> OuterComponents
        {
            get { return outerComponents.AsReadOnly(); }
        }
        private List<HalfEdge> outerComponents;

        public Face()
        {
            this.outerComponents = new List<HalfEdge>();
        }

        /// <summary>
        /// Adds an edge to the outer component of this face, if that face is not already part of the outer component.
        /// </summary>
        /// <param name="edge">Edge that is part of the outer component of this face.</param>
        public void AddOuterComponent(HalfEdge edge)
        {
            if (!OuterComponents.Contains(edge)) outerComponents.Add(edge);
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="T:DoubleConnectedEdgeList.Face"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            int hash = 17;
            foreach (HalfEdge edge in OuterComponents)
            {
                hash *= (31 + edge.NonRecursiveGetHashCode());
            }
            return hash;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="T:DoubleConnectedEdgeList.Face"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in GetHashCode of <see cref="T:DoubleConnectedEdgeList.Face"/>, <see cref="T:DoubleConnectedEdgeList.Vertex"/>, <see cref="T:DoubleConnectedEdgeList.HalfEdge"/></returns>
        public int NonRecursiveGetHashCode()
        {
            int hash = 17;
            foreach (HalfEdge edge in OuterComponents)
            {
                hash *= (31 + edge.Origin.Position.GetHashCode());
                if (edge.HasDestination) hash *= (31 + edge.Destination.Position.GetHashCode());
            }
            return hash;
        }

        public override string ToString()
        {
            return string.Format("[Face]");
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:DoubleConnectedEdgeList.Face"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:DoubleConnectedEdgeList.Face"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:DoubleConnectedEdgeList.Face"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return this.Equals(obj as Face);
        }

        /// <summary>
        /// Determines whether the specified <see cref="DoubleConnectedEdgeList.Face"/> is equal to the current <see cref="T:DoubleConnectedEdgeList.Face"/>.
        /// </summary>
        /// <param name="other">The <see cref="DoubleConnectedEdgeList.Face"/> to compare with the current <see cref="T:DoubleConnectedEdgeList.Face"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="DoubleConnectedEdgeList.Face"/> is equal to the current
        /// <see cref="T:DoubleConnectedEdgeList.Face"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(Face other)
        {
            IEnumerable<HalfEdge> inThisButNotInOther = this.OuterComponents.Except(other.OuterComponents);
            IEnumerable<HalfEdge> inOtherButNotInThis = other.OuterComponents.Except(this.OuterComponents);

            return (
                !inThisButNotInOther.Any() &&
                !inOtherButNotInThis.Any()
            );
        }

        /// <summary>
        /// Determines whether the specified <see cref="DoubleConnectedEdgeList.Face"/> is equal to the current <see cref="T:DoubleConnectedEdgeList.Face"/> without invoking the equals of <see cref="T:DoubleConnectedEdgeList.Vertex"/>, <see cref="T:DoubleConnectedEdgeList.HalfEdge"/> or <see cref="T:DoubleConnectedEdgeList.Face"/>.
        /// </summary>
        /// <param name="other">The <see cref="DoubleConnectedEdgeList.Face"/> to compare with the current <see cref="T:DoubleConnectedEdgeList.Face"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="DoubleConnectedEdgeList.Face"/> is equal to the current
        /// <see cref="T:DoubleConnectedEdgeList.Face"/>; otherwise, <c>false</c>.</returns>
        public bool NonRecursiveEquals(Face other)
        {
            IEnumerable<HalfEdge> inThisButNotInOther = this.OuterComponents.Except(other.OuterComponents, new SimpleHalfEdgeComparer());
            IEnumerable<HalfEdge> inOtherButNotInThis = other.OuterComponents.Except(this.OuterComponents, new SimpleHalfEdgeComparer());

            return (
                !inThisButNotInOther.Any() &&
                !inOtherButNotInThis.Any()
            );
        }

        internal class SimpleHalfEdgeComparer : IEqualityComparer<HalfEdge>
        {
            public bool Equals(HalfEdge x, HalfEdge y)
            {
                if (x == null && y == null) return true;
                if (x == null || y == null) return false;
                bool originEqual = x.Origin.Position.Equals(y.Origin.Position);
                bool destinationEqual = CompareVertex(x.Destination, y.Destination);

                return originEqual && destinationEqual;
            }

            private bool CompareVertex(Vertex thisVertex, Vertex otherVertex)
            {
                if (thisVertex == null && otherVertex == null) return true;
                if (thisVertex == null || otherVertex == null) return false;
                return thisVertex.Position.Equals(otherVertex.Position);
            }

            public int GetHashCode(HalfEdge obj)
            {
                return obj.NonRecursiveGetHashCode();
            }
        }

    }
}