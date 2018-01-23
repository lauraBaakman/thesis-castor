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
    }

    public class DistanceNode : IComparable<DistanceNode>
    {
        private Vector3 staticPoint;
        public Vector3 StaticPoint {
            get {
                return staticPoint;
            }
        }

        private Vector3 modelPoint;
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
    }
}
