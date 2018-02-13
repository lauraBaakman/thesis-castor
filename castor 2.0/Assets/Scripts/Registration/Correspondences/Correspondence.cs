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
        private readonly Point modelPoint;
        private readonly Point staticPoint;

        public readonly Color Color;

        public Point ModelPoint { get { return modelPoint; } }

        public Point StaticPoint { get { return staticPoint; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Correspondence"/> class. 
        /// </summary>
        /// <param name="modelPoint">The point of the correspondence from the model mesh..</param>
        /// <param name="staticPoint">The point of the correspondence from the static mesh.</param>
        public Correspondence(Point staticPoint, Point modelPoint)
        {
            this.modelPoint = modelPoint;
            this.staticPoint = staticPoint;

            Color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }

        public Correspondence(DistanceNode node)
            : this(
                staticPoint: node.StaticPoint,
                modelPoint: node.ModelPoint
            )
        { }

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
