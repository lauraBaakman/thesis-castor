using UnityEngine;
using System;

namespace Registration
{
    public class Point : IEquatable<Point>, IComparable<Point>
    {

        private Vector3 position;

        public Point(Vector3 position)
        {
            this.Position = position;
        }

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public int CompareTo(Point other)
        {
            throw new NotImplementedException();
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