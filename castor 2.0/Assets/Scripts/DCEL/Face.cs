using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace DoubleConnectedEdgeList
{
    public class Face : IEquatable<Face>
    {

        public ReadOnlyCollection<HalfEdge> OuterComponents
        {
            get { return outerComponents.AsReadOnly(); }
        }
        private List<HalfEdge> outerComponents;

        public Face()
        {
            this.outerComponents = new List<HalfEdge>();
        }

        public void AddOuterComponent(HalfEdge edge)
        {
            if (!OuterComponents.Contains(edge)) outerComponents.Add(edge);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            foreach (HalfEdge edge in OuterComponents)
            {
                hash *= (31 + edge.NonRecursiveGetHashCode());
            }
            return hash;
        }

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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return this.Equals(obj as Face);
        }

        public bool Equals(Face other)
        {
            Debug.Log("Face:Equals");
            IEnumerable<HalfEdge> inThisButNotInOther = this.OuterComponents.Except(other.OuterComponents);
            IEnumerable<HalfEdge> inOtherButNotInThis = other.OuterComponents.Except(this.OuterComponents);

            return (
                !inThisButNotInOther.Any() &&
                !inOtherButNotInThis.Any()
            );
        }

        public bool NonRecursiveEquals(Face other)
        {
            Debug.Log("Face:NonRecursiveEquals");
            IEnumerable<HalfEdge> inThisButNotInOther = this.OuterComponents.Except(other.OuterComponents);
            IEnumerable<HalfEdge> inOtherButNotInThis = other.OuterComponents.Except(this.OuterComponents);

            return (
                !inThisButNotInOther.Any() &&
                !inOtherButNotInThis.Any()
            );
        }
    }
}