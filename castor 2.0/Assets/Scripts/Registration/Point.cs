using UnityEngine;
using System;

namespace Registration
{
    public class Point : IEquatable<Point>, IComparable<Point>
    {
        public Vector3 Position
        {
            get { return position; }
        }
        protected Vector3 position;

        public Point(Vector3 position)
        {
            this.position = position;
        }

        public int CompareTo(Point other)
        {
            if (other == null) return 1;

            int positionMagnitudeComparison = this.position.CompareTo(other.position);
            if (positionMagnitudeComparison != 0) return positionMagnitudeComparison;

            int xComparison = this.position.x.CompareTo(other.position.x);
            if (xComparison != 0) return xComparison;

            int yComparison = this.position.y.CompareTo(other.position.y);
            if (yComparison != 0) return yComparison;

            int zComparison = this.position.z.CompareTo(other.position.z);
            return zComparison;
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
                this.position.Equals(other.position)
            );
        }

        public override int GetHashCode()
        {
            int hashCode = 67;

            hashCode = hashCode * 71 + position.GetHashCode();

            return hashCode;
        }

        public override string ToString()
        {
            return string.Format(
                "[Point: Position={0}]",
                Position
            );
        }
    }
}