using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Registration
{
    /// <summary>
    /// Find the correspondences using the nearest point method. The method computes 
    /// the distances of all point pairs and creates correspondences starting from the 
    /// pair with the smallest distance until all points of at least one of the meshes 
    /// are involved in a correspondence.
    /// </summary>
    public class NearstPointCorrespondenceFinder : ICorrespondenceFinder
    {
        public List<Correspondence> Find( List<Vector3> staticPoints, List<Vector3> modelPoints )
        {
            List<Correspondence> correspondences = new List<Correspondence>();

            //TODO implement!

            return correspondences;
        }

        /// <summary>
        /// Create the list of elements, where each element contains the distance
        /// between two points of the each of the meshes.
        /// </summary>
        /// <returns>The list with distance nodes..</returns>
        /// <param name="staticPoints">Static points.</param>
        /// <param name="modelPoints">Model points.</param>
        public List<DistanceNode> CreateDistanceNodeList( List<Vector3> staticPoints, List<Vector3> modelPoints )
        {
            List<DistanceNode> nodes = new List<DistanceNode>();

            //TODO implement!

            return nodes;
        }
    }

    public class DistanceNode : IComparable<DistanceNode>, IEquatable<DistanceNode>
    {
        private readonly Vector3 staticPoint;
        public Vector3 StaticPoint {
            get {
                return staticPoint;
            }
        }

        private readonly Vector3 modelPoint;
        public Vector3 ModelPoint {
            get {
                return modelPoint;
            }
        }

        private readonly float distance;
        public float Distance {
            get {
                return distance;
            }
        }

        public DistanceNode( Vector3 staticPoint, Vector3 modelPoint, float distance )
        {
            this.staticPoint = staticPoint;
            this.modelPoint = modelPoint;
            this.distance = distance;
        }

        public int CompareTo( DistanceNode other )
        {
            if (other == null) return 1;

            return this.distance.CompareTo(other.distance);
        }

        public bool Equals( DistanceNode other )
        {
            if (other == null) return false;

            return (
                this.staticPoint.Equals(other.staticPoint) &&
                this.modelPoint.Equals(other.modelPoint) &&
                this.distance.Equals(other.distance)
            );
        }

        public override bool Equals( System.Object obj )
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            DistanceNode other = (DistanceNode)obj;
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return (int)(
                (this.staticPoint.x * this.staticPoint.y * this.staticPoint.z) +
                (this.modelPoint.x * this.modelPoint.y * this.modelPoint.z) +
                         distance
            );
        }
    }
}
