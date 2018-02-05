using UnityEngine;
using System;

namespace Registration
{

    /// <summary>
    /// Correspondence, containing the two points that are considered paired in an 
    /// ICP iteration.
    /// </summary>
    public class Correspondence : IEquatable<Correspondence>
    {
        private readonly Vector3 modelPoint;
        private readonly Vector3 staticPoint;

        public readonly Color Color;

        public Vector3 ModelPoint { get { return modelPoint; } }

        public Vector3 StaticPoint { get { return staticPoint; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Correspondence"/> class. 
        /// </summary>
        /// <param name="modelPoint">The point of the correspondence from the model mesh..</param>
        /// <param name="staticPoint">The point of the correspondence from the static mesh.</param>
        public Correspondence(Vector3 staticPoint, Vector3 modelPoint)
        {
            this.modelPoint = modelPoint;
            this.staticPoint = staticPoint;

            Color = UnityEngine.Random.ColorHSV(
                hueMin:0.0f, hueMax:1.0f, 
                saturationMin: 1.0f, saturationMax: 1.0f, 
                valueMin: 0.5f, valueMax: 0.5f
            );
        }

        public Correspondence(DistanceNode node)
        {
            this.modelPoint = node.ModelPoint;
            this.staticPoint = node.StaticPoint;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Correspondence other = (Correspondence)obj;
            return this.Equals(other);
        }

        public bool Equals(Correspondence other)
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
