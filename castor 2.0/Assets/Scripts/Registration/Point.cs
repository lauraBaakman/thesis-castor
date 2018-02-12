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
         
            return this.position.CompareTo(other.position);
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

    public class PointWithNormal
        : Point, IEquatable<PointWithNormal>, IComparable<PointWithNormal>, IEquatable<Point>, IComparable<Point>
    {
        public Vector3 Normal
        {
            get { return normal; }
        }
        private Vector3 normal;

        public PointWithNormal(Vector3 position, Vector3 normal)
            : base(position)
        {
            this.normal = normal;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            return this.Equals(obj as PointWithNormal);
        }

        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();

            hashCode = hashCode * 71 + normal.GetHashCode();

            return hashCode;
        }

        public override string ToString()
        {
            return string.Format(
                "[PointWithNormal: Position={0}, Normal={1}]",
                Position, Normal
            );
        }

        public bool Equals(PointWithNormal other)
        {
            if (other == null) return false;

            return (
                this.position.Equals(other.position) &&
                this.normal.Equals(other.normal)
            );
        }

        public int CompareTo(PointWithNormal other)
        {
            if (other == null) return 1;

            int positonComparison = this.position.CompareTo(other.position);
            if (positonComparison != 0) return positonComparison;

            return this.normal.CompareTo(other.normal);
        }
    }
}