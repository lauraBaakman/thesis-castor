using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace DoubleConnectedEdgeList
{
    public class Vertex : IEquatable<Vertex>
    {
        public readonly Vector3 Position;

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

        public void AddIncidentEdge(HalfEdge edge)
        {
            if (!IncidentEdges.Contains(edge)) incidentEdges.Add(edge);
        }

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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return this.Equals(obj as Vertex);
        }

        public bool Equals(Vertex other)
        {
            Debug.Log("Vertex:Equals");
            return (
                this.Position.Equals(other.Position) &&
                NonRecursiveEqualsAuxilary(this.IncidentEdges, other.IncidentEdges)
            );
        }

        public bool NonRecursiveEquals(Vertex other)
        {
            Debug.Log("Vertex:NonRecursiveEquals");
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