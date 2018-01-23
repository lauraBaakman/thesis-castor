using UnityEngine;
using System.Collections.Generic;
using System;

namespace Registration
{

    /// <summary>
    /// Correspondence, containing the two points that are considered paired in an 
    /// ICP iteration.
    /// </summary>
    public class Correspondence : IEquatable<Correspondence>
    {
        private readonly Vector3 ModelPoint;
        private readonly Vector3 StaticPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Correspondence"/> class. 
        /// </summary>
        /// <param name="modelPoint">The point of the correspondence from the model mesh..</param>
        /// <param name="staticPoint">The point of the correspondence from the static mesh.</param>
        public Correspondence( Vector3 staticPoint, Vector3 modelPoint )
        {
            ModelPoint = modelPoint;
            StaticPoint = staticPoint;
        }

        public Correspondence(DistanceNode node){
            ModelPoint = node.ModelPoint;
            StaticPoint = node.StaticPoint;
        }

        public override bool Equals( object obj )
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Correspondence other = (Correspondence)obj;
            return this.Equals(other);
        }

        public bool Equals( Correspondence other )
        {
            if (other == null) return false;

            return (
                this.StaticPoint.Equals(other.StaticPoint) &&
                this.ModelPoint.Equals(other.ModelPoint)
            );
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "Correspondence (" + ModelPoint + ", " + StaticPoint + ")";
        }
    }
}
