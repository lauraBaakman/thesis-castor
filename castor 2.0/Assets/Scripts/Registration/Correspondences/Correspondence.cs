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
        private static Color DefaultColor = Color.white;

        private readonly Point modelPoint;
        private readonly Point staticPoint;

        public Point ModelPoint { get { return modelPoint; } }

        public Point StaticPoint { get { return staticPoint; } }

        public Color Color
        {
            get { return color; }
        }
        private Color color;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Correspondence"/> class. 
        /// </summary>
        /// <param name="modelPoint">The point of the correspondence from the model mesh..</param>
        /// <param name="staticPoint">The point of the correspondence from the static mesh.</param>
        public Correspondence(Point staticPoint, Point modelPoint)
        {
            this.modelPoint = modelPoint;
            this.staticPoint = staticPoint;

            this.color = staticPoint.Color;

            Activate();
        }

        public Correspondence(DistanceNode node)
            : this(
                staticPoint: node.StaticPoint,
                modelPoint: node.ModelPoint
            )
        { }

        /// <summary>
        /// Deactivate this correspondence, i.e. indicate that it will not be used for ICP.
        /// </summary>
        public void Deactivate()
        {
            modelPoint.ResetColor();
            ResetColor();
        }

        /// <summary>
        /// Activate this correspondence, i.e. indicate that it will not be used for ICP.
        /// </summary>
        public void Activate()
        {
            modelPoint.Color = staticPoint.Color;
            color = staticPoint.Color;
        }

        public Point GetPoint(Fragment.ICPFragmentType type)
        {
            switch (type)
            {
                case Fragment.ICPFragmentType.Model:
                    return ModelPoint;
                case Fragment.ICPFragmentType.Static:
                    return StaticPoint;
                default:
                    throw new ArgumentException("Invalid enum type.");
            }
        }

        private void ResetColor()
        {
            this.color = DefaultColor;
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
