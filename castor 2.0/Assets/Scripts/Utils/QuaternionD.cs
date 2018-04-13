using System;

namespace Utils
{
    public class QuaternionD : IEquatable<QuaternionD>
    {
        #region staticFields
        private static readonly QuaternionD identityQuaternion = new QuaternionD(0f, 0f, 0f, 1f);

        public const float kEpsilon = 1E-06f;
        #endregion

        #region fields
        public double x;
        public double y;
        public double z;
        public double w;
        #endregion

        #region Static Properties
        public static QuaternionD identity
        {
            get { return QuaternionD.identityQuaternion; }
        }
        #endregion

        #region properties
        /// <summary>
        /// Gets the rotation axis of the quaternion, using Unity's notation.
        /// </summary>
        /// <value>The rotation axis.</value>
        public Vector3D xyz
        {
            get { return new Vector3D(this.x, this.y, this.z); }
        }

        /// <summary>
        /// Gets the rotation axis of the quaternion, using Wheeler's notation.
        /// </summary>
        /// <value>The rotation axis.</value>
        public Vector3D Wuvw
        {
            get { return new Vector3D(this.x, this.y, this.z); }
        }

        /// <summary>
        /// Gets the x component of the rotation axis of the quaternion, using Wheeler's notation.
        /// </summary>
        /// <value>The rotation angle</value>
        public double Wu
        {
            get { return this.x; }
        }

        /// <summary>
        /// Gets the y component of the rotation axis of the quaternion, using Wheeler's notation.
        /// </summary>
        /// <value>The rotation angle</value>
        public double Wv
        {
            get { return this.y; }
        }

        /// <summary>
        /// Gets the z component of the rotation axis of the quaternion, using Wheeler's notation.
        /// </summary>
        /// <value>The rotation angle</value>
        public double Ww
        {
            get { return this.z; }
        }

        /// <summary>
        /// Gets the rotation angle of the quaternion, using Wheeler's notation.
        /// </summary>
        /// <value>The rotation angle</value>
        public double Ws
        {
            get { return this.w; }
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
                        throw new IndexOutOfRangeException("Invalid Quaternion index!");
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
                        throw new IndexOutOfRangeException("Invalid Quaternion index!");
                }
            }
        }
        #endregion

        #region Constructors
        public QuaternionD(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public QuaternionD(Vector3D axis, double angle)
            : this(axis.x, axis.y, axis.z, angle)
        { }
        #endregion

        #region staticMethods
        public static double Dot(QuaternionD a, QuaternionD b)
        {
            return (
                a.x * b.x +
                a.y * b.y +
                a.z * b.z +
                a.w * b.w
            );
        }
        #endregion

        #region Methods
        public override bool Equals(object obj)
        {
            if (!(obj is QuaternionD)) return false;

            return this.Equals(obj as QuaternionD);
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
        }

        public void Set(double newX, double newY, double newZ, double newW)
        {
            this.x = newX;
            this.y = newY;
            this.z = newZ;
            this.w = newW;
        }
        #endregion

        #region overrides
        public bool Equals(QuaternionD other)
        {
            return (
                this.x.Equals(other.x) &&
                this.y.Equals(other.y) &&
                this.z.Equals(other.z) &&
                this.w.Equals(other.w)
            );
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2}, {3})", new object[] {
                this.x,
                this.y,
                this.z,
                this.w
            });
        }

        public string ToString(string format)
        {
            return string.Format("({0}, {1}, {2}, {3})", new object[] {
                this.x.ToString (format),
                this.y.ToString (format),
                this.z.ToString (format),
                this.w.ToString (format)
            });
        }
        #endregion

        #region operators
        public static QuaternionD operator *(QuaternionD lhs, QuaternionD rhs)
        {
            return new QuaternionD(
                lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y,
                lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z,
                lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x,
                lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z
            );
        }

        public static Vector3D operator *(QuaternionD rotation, Vector3D point)
        {
            double num = rotation.x * 2f;
            double num2 = rotation.y * 2f;
            double num3 = rotation.z * 2f;
            double num4 = rotation.x * num;
            double num5 = rotation.y * num2;
            double num6 = rotation.z * num3;
            double num7 = rotation.x * num2;
            double num8 = rotation.x * num3;
            double num9 = rotation.y * num3;
            double num10 = rotation.w * num;
            double num11 = rotation.w * num2;
            double num12 = rotation.w * num3;

            return new Vector3D(
                x: (1 - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z,
                y: (num7 + num12) * point.x + (1 - (num4 + num6)) * point.y + (num9 - num10) * point.z,
                z: (num8 - num11) * point.x + (num9 + num10) * point.y + (1 - (num4 + num5)) * point.z
            );
        }

        public static QuaternionD operator *(QuaternionD rotation, double scalar)
        {
            return new QuaternionD(
                rotation.x * scalar,
                rotation.y * scalar,
                rotation.z * scalar,
                rotation.w * scalar
            );
        }

        public static QuaternionD operator *(double scalar, QuaternionD rotation)
        {
            return rotation * scalar;
        }

        public static QuaternionD operator /(QuaternionD rotation, double scalar)
        {
            return new QuaternionD(
                rotation.x / scalar,
                rotation.y / scalar,
                rotation.z / scalar,
                rotation.w / scalar
            );
        }
        #endregion
    }
}