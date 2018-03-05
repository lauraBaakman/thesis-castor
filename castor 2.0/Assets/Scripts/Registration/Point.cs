using UnityEngine;
using System;

namespace Registration
{
    public class Point : IEquatable<Point>, IComparable<Point>
    {
        private static Color DefaultColor = Color.white;
        private static readonly Vector3 NoNormal = new Vector3();

        #region position
        public Vector3 Position
        {
            get { return position; }
        }
        private Vector3 position;
        #endregion

        #region color
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        private Color color;
        #endregion

        #region normal
        public Vector3 Normal
        {
            get
            {
                if (hasNormal()) return normal;
                throw new Exception("The normal of this Point has not been set.");
            }
        }
        private Vector3 normal;

        public bool HasNormal
        {
            get { return hasNormal(); }
        }
        #endregion

        public Point(Vector3 position, Vector3 normal)
        {
            this.position = position;
            this.Color = new Color().Random(0f, 1f, 1f, 1f, 0.5f, 1f);
            this.normal = normal;
        }

        public Point(Vector3 position)
            : this(position, normal: NoNormal)
        { }

        private Point(Vector3 position, Vector3 normal, Color color)
        {
            this.position = position;
            this.normal = normal;
            this.color = color;
        }

        private bool hasNormal()
        {
            return this.normal != NoNormal;
        }

        /// <summary>
        /// Resets the color of the point to the default color.
        /// </summary>
        public void ResetColor()
        {
            this.Color = DefaultColor;
        }

        public int CompareTo(Point other)
        {
            if (other == null) return 1;

            int positionComparison = this.position.CompareTo(other.position);
            if (positionComparison != 0) return positionComparison;

            return this.normal.CompareTo(other.normal);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            return this.Equals(obj as Point);
        }

        public bool Equals(Point other)
        {
            if (other == null) return false;

            bool positionEqual = (this.Position == other.Position);
            bool normalEqual = (this.normal == other.normal);

            return (positionEqual && normalEqual);
        }

        public override int GetHashCode()
        {
            int hashCode = 67;

            hashCode = hashCode * 71 + position.GetHashCode();
            if (hasNormal()) hashCode = hashCode * 71 + normal.GetHashCode();

            return hashCode;
        }

        public override string ToString()
        {
            if (hasNormal())
            {
                return string.Format(
                     "[Point: Position={0}, Normal={1}]", Position, Normal
                 );
            }
            return string.Format("[Point: Position={0}, no normal]", Position);
        }

        /// <summary>
        /// Convert the point to a ray in world space
        /// </summary>
        /// <returns>The ray starting at thhe position of this point in the direction of its normal.</returns>
        /// <param name="localTransform">The local transform of the point.</param>
        public Ray ToWorldSpaceRay(Transform localTransform)
        {
            Vector3 worldSpacePosition = localTransform.TransformPoint(Position);
            Vector3 worldSpaceDirection = localTransform.TransformDirection(Normal);

            worldSpaceDirection.Normalize();

            return new Ray(
                origin: worldSpacePosition,
                direction: worldSpaceDirection
            );
        }

        public Vector4 ToHomogeneousVector4()
        {
            return new Vector4(Position.x, Position.y, Position.z, 1.0f);
        }

        /// <summary>
        /// Transform this point from sourceTransform to destinationTransform.
        /// </summary>
        /// <returns>The other transform.</returns>
        /// <param name="sourceTransform">The current transform of the point.</param>
        /// <param name="destinationTransform">The destination transform.</param>
        public Point ChangeTransform(Transform sourceTransform, Transform destinationTransform)
        {
            return new Point(
                position: Position.ChangeTransformOfPosition(sourceTransform, destinationTransform),
                normal: HasNormal ? Normal.ChangeTransformOfDirection(sourceTransform, destinationTransform) : NoNormal
            );
        }

        public Point ApplyTransform(Matrix4x4 transformationMatrix)
        {
            Vector3 transformedPosition = transformationMatrix.MultiplyPoint(this.Position);
            return new Point(transformedPosition, this.normal, this.Color);
        }
    }
}