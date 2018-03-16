using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace DoubleConnectedEdgeList
{
    /// <summary>
    /// Represents a Face in double connected edge list.
    /// </summary>
    public class Face : IEquatable<Face>, IComparable
    {
        private static float notComputed = float.NaN;

        /// <summary>
        /// The half edges on the boundary of this face in arbitrary order. 
        /// </summary>
        /// <value>The outer components.</value>
        public ReadOnlyCollection<HalfEdge> OuterComponents
        {
            get { return outerComponents.AsReadOnly(); }
        }
        private List<HalfEdge> outerComponents;

        public readonly int MeshIdx;

        /// <summary>
        /// Gets the area of the face, if the face is triangular
        /// 
        /// Not this method assumes that the list of outercomponents is complete.
        /// </summary>
        /// <value>The area.</value>
        public float Area
        {
            get
            {
                if (!IsTriangular()) throw new InvalidOperationException("The area property is not supported for non-triangluar faces.");
                if (area.Equals(notComputed)) area = computeTriangleArea();
                return area;
            }
        }
        float area = notComputed;

        public Vector3 Normal
        {
            get { return normal; }
            internal set { normal = value; }
        }
        private Vector3 normal;

        private float computeTriangleArea()
        {
            float edgeSum = 0.0f;
            foreach (HalfEdge edge in OuterComponents)
            {
                edgeSum += edge.Length;
            }
            return (edgeSum * 0.5f);
        }

        public Face(int meshIdx)
        {
            this.MeshIdx = meshIdx;
            this.outerComponents = new List<HalfEdge>();
        }

        public Face(int meshIdx, Vector3 normal)
            : this(meshIdx)
        {
            this.normal = normal;
        }

        /// <summary>
        /// Is the face the triangular.
        /// 
        /// Note this method assumes that the list of outercomponents is complete!
        /// </summary>
        /// <returns><c>true</c>, if the face is triangular, <c>false</c> otherwise.</returns>
        public bool IsTriangular()
        {
            return (outerComponents.Count == 3);
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
            HalfEdge.SimpleComparer edgeComparer = new HalfEdge.SimpleComparer();

            int hash = 17;
            hash *= (31 + MeshIdx.GetHashCode());
            foreach (HalfEdge edge in OuterComponents)
            {
                hash *= (31 + edgeComparer.GetHashCode(edge));
            }
            return hash;
        }

        public override string ToString()
        {
            return string.Format("[Face [{0}]]", MeshIdx);
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
            return (
                this.MeshIdx.Equals(other.MeshIdx) &&
                this.OuterComponents.UnorderedElementsAreEqual(other.OuterComponents)
            );
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (GetType() != obj.GetType()) throw new ArgumentException("Object is not a Face.");

            Face face = obj as Face;

            return this.MeshIdx.CompareTo(face.MeshIdx);
        }

        public class SimpleComparer : IEqualityComparer<Face>
        {
            public bool Equals(Face x, Face y)
            {
                if (x == null && y == null) return true;
                if (x == null || y == null) return false;

                return (
                    x.MeshIdx.Equals(y.MeshIdx)
                );
            }

            public int GetHashCode(Face obj)
            {
                if (obj == null) return 0;

                return obj.MeshIdx.GetHashCode();
            }
        }

        public class KeyValueComparer<K> : IEqualityComparer<KeyValuePair<K, Face>> where K : IEquatable<K>
        {
            public bool Equals(KeyValuePair<K, Face> x, KeyValuePair<K, Face> y)
            {
                return (
                    x.Key.Equals(y.Key) &&
                    x.Value.Equals(y.Value)
                );
            }

            public int GetHashCode(KeyValuePair<K, Face> obj)
            {
                int hash = 17;
                hash *= (31 + obj.Key.GetHashCode());
                hash *= (31 + obj.Value.GetHashCode());
                return hash;
            }
        }
    }
}