using UnityEngine;
using System;

namespace Utils
{
    public class Vector4D : IEquatable<Vector4D>
    {
        public double x;
        public double y;
        public double z;
        public double w;

        #region constructors
        public Vector4D(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.z = z;
        }

        public Vector4D()
            : this(0, 0, 0, 0)
        { }

        public Vector4D(Vector4 vector)
            : this(vector.x, vector.y, vector.z, vector.w)
        { }

        public Vector4D(Vector4D vector)
            : this(vector.x, vector.y, vector.z, vector.w)
        { }
        #endregion

        #region IEquatable
        public bool Equals(Vector4D other)
        {
            return (EqualsWithMargin(this.x, other.x) &&
                    EqualsWithMargin(this.y, other.y) &&
                    EqualsWithMargin(this.z, other.z) &&
                    EqualsWithMargin(this.w, other.w));
        }

        private bool EqualsWithMargin(double self, double other)
        {
            double margin = Math.Abs((self + other) * .000005);
            return Math.Abs(self - other) <= margin;
        }
        #endregion

        #region overrides
        public override bool Equals(object obj)
        {
            if (!(obj is Vector4D)) return false;

            return this.Equals(obj as Vector4D);
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
        }

        public override string ToString()
        {
            return string.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", new object[] { this.x, this.y, this.z, this.w });
        }
        #endregion

        #region methods
        public double SqrMagnitude()
        {
            return Vector4D.Dot(this, this);
        }

        public double Magnitude()
        {
            return Math.Sqrt(Dot(this, this));
        }

        public double Dot(Vector4D rhs)
        {
            return Vector4D.Dot(this, rhs);
        }
        #endregion

        #region indexer
        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.x;
                    case 1:
                        return this.y;
                    case 2:
                        return this.z;
                    case 3:
                        return this.w;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector4D index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.x = value;
                        break;
                    case 1:
                        this.y = value;
                        break;
                    case 2:
                        this.z = value;
                        break;
                    case 3:
                        this.w = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector4D index!");
                }
            }
        }
        #endregion

        #region static methods
        public static double Dot(Vector4D a, Vector4D b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        public static double Magnitude(Vector4D a)
        {
            return Math.Sqrt(Dot(a, a));
        }

        public static double SqrMagnitude(Vector4D a)
        {
            return Vector4D.Dot(a, a);
        }
        #endregion

        #region operators
        public static Vector4D operator +(Vector4D a, Vector4D b)
        {
            return new Vector4D(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        public static Vector4D operator /(Vector4D a, double d)
        {
            return new Vector4D(a.x / d, a.y / d, a.z / d, a.w / d);
        }

        public static Vector4D operator *(Vector4D a, double d)
        {
            return new Vector4D(a.x * d, a.y * d, a.z * d, a.w * d);
        }

        public static Vector4D operator *(double d, Vector4D a)
        {
            return new Vector4D(a.x * d, a.y * d, a.z * d, a.w * d);
        }

        public static Vector4D operator -(Vector4D a, Vector4D b)
        {
            return new Vector4D(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        public static Vector4D operator -(Vector4D a)
        {
            return new Vector4D(-a.x, -a.y, -a.z, -a.w);
        }

        public static bool operator ==(Vector4D lhs, Vector4D rhs)
        {
            return (lhs - rhs).SqrMagnitude() < 9.99999944E-11f;
        }

        public static bool operator !=(Vector4D lhs, Vector4D rhs)
        {
            return !(lhs == rhs);
        }
        #endregion
    }
}