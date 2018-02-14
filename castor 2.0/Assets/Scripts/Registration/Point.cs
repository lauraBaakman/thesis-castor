using UnityEngine;
using System;

namespace Registration
{
    public class Point : IEquatable<Point>, IComparable<Point>
    {
        private static Color DefaultColor = Color.white;

        public Vector3 Position
        {
            get { return position; }
        }
        private Vector3 position;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        private Color color;

        public Vector3 Normal
        {
            get
            {
                if (HasNormal()) return normal;
                throw new Exception("The normal of this Point has not been set.");
            }
        }
        private Vector3 normal;

        public Point(Vector3 position, Vector3 normal)
        {
            this.position = position;
            this.Color = DefaultColor;
            this.normal = normal;
        }

        public Point(Vector3 position)
            : this(position, normal: new Vector3())
        { }

        public bool HasNormal()
        {
            return this.normal != new Vector3();
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

            return (
                this.position.Equals(other.position) &&
                this.normal.Equals(other.normal)
            );
        }

        public override int GetHashCode()
        {
            int hashCode = 67;

            hashCode = hashCode * 71 + position.GetHashCode();
            if (HasNormal()) hashCode = hashCode * 71 + normal.GetHashCode();

            return hashCode;
        }

        public override string ToString()
        {
            if (HasNormal())
            {
                return string.Format(
                     "[Point: Position={0}, Normal={1}]", Position, Normal
                 );
            }
            return string.Format("[Point: Position={0}, no normal]", Position);
        }
    }

}