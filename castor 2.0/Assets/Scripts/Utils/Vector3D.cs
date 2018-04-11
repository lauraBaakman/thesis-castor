using System;

namespace Utils
{
    /// <summary>
    /// Represents a Vector3D using doubles, instead of floats. Note that for 
    /// this object only the used methods are implemented. Expected operators, 
    /// such as Vector3D() + Vector3D() might be missing.
    /// </summary>
    public class Vector3D : IEquatable<Vector3D>
    {
        public double x;
        public double y;
        public double z;

        #region constructors
        public Vector3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3D()
            : this(0, 0, 0)
        { }

        public Vector3D(Vector3D vector)
            : this(vector.x, vector.y, vector.z)
        { }
        #endregion

        #region overrides
        public override bool Equals(object obj)
        {
            if (!(obj is Vector3D)) return false;

            return this.Equals(obj as Vector3D);
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", this.x, this.y, this.z);
        }
        #endregion

        #region IEquatable
        public bool Equals(Vector3D other)
        {
            return (EqualsWithMargin(this.x, other.x) &&
                    EqualsWithMargin(this.y, other.y) &&
                    EqualsWithMargin(this.z, other.z));
        }

        private bool EqualsWithMargin(double self, double other)
        {
            double margin = Math.Abs((self + other) * .000005);
            return Math.Abs(self - other) <= margin;
        }
        #endregion

        #region operators
        public static Vector3D operator *(Vector3D a, double d)
        {
            return new Vector3D(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3D operator *(double d, Vector3D a)
        {
            return new Vector3D(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3D operator /(Vector3D a, double d)
        {
            return new Vector3D(a.x / d, a.y / d, a.z / d);
        }
        #endregion
    }
}