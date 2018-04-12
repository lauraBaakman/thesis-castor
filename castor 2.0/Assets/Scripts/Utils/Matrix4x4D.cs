using System;
using UnityEngine;

namespace Utils
{

    public class Matrix4x4D : IEquatable<Matrix4x4D>
    {
        #region constructors
        public Matrix4x4D()
            : this(new Vector4D(), new Vector4D(), new Vector4D(), new Vector4D())
        { }

        public Matrix4x4D(Vector4D column0, Vector4D column1, Vector4D column2, Vector4D column3)
        {
            this.m00 = column0.x;
            this.m01 = column1.x;
            this.m02 = column2.x;
            this.m03 = column3.x;
            this.m10 = column0.y;
            this.m11 = column1.y;
            this.m12 = column2.y;
            this.m13 = column3.y;
            this.m20 = column0.z;
            this.m21 = column1.z;
            this.m22 = column2.z;
            this.m23 = column3.z;
            this.m30 = column0.w;
            this.m31 = column1.w;
            this.m32 = column2.w;
            this.m33 = column3.w;
        }

        public Matrix4x4D(
            double m00, double m01, double m02, double m03,
            double m10, double m11, double m12, double m13,
            double m20, double m21, double m22, double m23,
            double m30, double m31, double m32, double m33
        )
            : this(
                new Vector4D(m00, m10, m20, m30),
                new Vector4D(m01, m11, m21, m31),
                new Vector4D(m02, m12, m22, m32),
                new Vector4D(m03, m13, m23, m33)
            )
        { }
        #endregion

        #region static constructors
        public static Matrix4x4D TransformationMatrixFromTranslation(Vector4D translation)
        {
            Matrix4x4D matrix = identityMatrix;

            matrix.m03 = translation.x;
            matrix.m13 = translation.y;
            matrix.m23 = translation.z;

            return matrix;
        }

        public static Matrix4x4D TransformationMatrixFromUnitQuaternion(QuaternionD unitQuaternion)
        {
            throw new NotSupportedException();
        }

        public static Matrix4x4D TransformationMatrixFromQuaternion(QuaternionD quaternion)
        {
            throw new NotSupportedException();
        }
        #endregion

        #region overrides
        public override bool Equals(object obj)
        {
            if (!(obj is Matrix4x4D)) return false;

            return this.Equals(obj as Matrix4x4D);
        }

        public override int GetHashCode()
        {
            return this.GetColumn(0).GetHashCode() ^ this.GetColumn(1).GetHashCode() << 2 ^ this.GetColumn(2).GetHashCode() >> 2 ^ this.GetColumn(3).GetHashCode() >> 1;
        }

        public override string ToString()
        {
            return string.Format("{0:F5}\t{1:F5}\t{2:F5}\t{3:F5}\n{4:F5}\t{5:F5}\t{6:F5}\t{7:F5}\n{8:F5}\t{9:F5}\t{10:F5}\t{11:F5}\n{12:F5}\t{13:F5}\t{14:F5}\t{15:F5}\n", new object[] {
                this.m00, this.m01, this.m02, this.m03,
                this.m10, this.m11, this.m12, this.m13,
                this.m20, this.m21, this.m22, this.m23,
                this.m30, this.m31, this.m32, this.m33
            });
        }
        #endregion

        #region indexers
        public double this[int row, int column]
        {
            get { return this[row + column * 4]; }
            set { this[row + column * 4] = value; }
        }

        private double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.m00;
                    case 1:
                        return this.m10;
                    case 2:
                        return this.m20;
                    case 3:
                        return this.m30;
                    case 4:
                        return this.m01;
                    case 5:
                        return this.m11;
                    case 6:
                        return this.m21;
                    case 7:
                        return this.m31;
                    case 8:
                        return this.m02;
                    case 9:
                        return this.m12;
                    case 10:
                        return this.m22;
                    case 11:
                        return this.m32;
                    case 12:
                        return this.m03;
                    case 13:
                        return this.m13;
                    case 14:
                        return this.m23;
                    case 15:
                        return this.m33;
                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.m00 = value;
                        break;
                    case 1:
                        this.m10 = value;
                        break;
                    case 2:
                        this.m20 = value;
                        break;
                    case 3:
                        this.m30 = value;
                        break;
                    case 4:
                        this.m01 = value;
                        break;
                    case 5:
                        this.m11 = value;
                        break;
                    case 6:
                        this.m21 = value;
                        break;
                    case 7:
                        this.m31 = value;
                        break;
                    case 8:
                        this.m02 = value;
                        break;
                    case 9:
                        this.m12 = value;
                        break;
                    case 10:
                        this.m22 = value;
                        break;
                    case 11:
                        this.m32 = value;
                        break;
                    case 12:
                        this.m03 = value;
                        break;
                    case 13:
                        this.m13 = value;
                        break;
                    case 14:
                        this.m23 = value;
                        break;
                    case 15:
                        this.m33 = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }
        }
        #endregion

        #region static properties
        public static Matrix4x4D identity
        {
            get { return new Matrix4x4D(new Vector4D(1, 0, 0, 0), new Vector4D(0, 1, 0, 0), new Vector4D(0, 0, 1, 0), new Vector4D(0, 0, 0, 1)); }
        }

        public static Matrix4x4D zero
        {
            get { return zeroMatrix; }
        }
        #endregion

        #region static fields
        private static readonly Matrix4x4D identityMatrix = new Matrix4x4D(new Vector4D(1, 0, 0, 0), new Vector4D(0, 1, 0, 0), new Vector4D(0, 0, 1, 0), new Vector4D(0, 0, 0, 1));

        private static readonly Matrix4x4D zeroMatrix = new Matrix4x4D(new Vector4D(0, 0, 0, 0), new Vector4D(0, 0, 0, 0), new Vector4D(0, 0, 0, 0), new Vector4D(0, 0, 0, 0));
        #endregion

        #region fields
        public double m00, m01, m02, m03;
        public double m10, m11, m12, m13;
        public double m20, m21, m22, m23;
        public double m30, m31, m32, m33;
        #endregion

        #region methods
        public Vector4D GetColumn(int index)
        {
            switch (index)
            {
                case 0:
                    return new Vector4D(this.m00, this.m10, this.m20, this.m30);
                case 1:
                    return new Vector4D(this.m01, this.m11, this.m21, this.m31);
                case 2:
                    return new Vector4D(this.m02, this.m12, this.m22, this.m32);
                case 3:
                    return new Vector4D(this.m03, this.m13, this.m23, this.m33);
                default:
                    throw new IndexOutOfRangeException("Invalid column index!");
            }
        }

        public Matrix4x4 ToUnityMatrix4x4()
        {
            return new Matrix4x4(
                GetColumn(0).ToUnityVector(),
                GetColumn(1).ToUnityVector(),
                GetColumn(2).ToUnityVector(),
                GetColumn(3).ToUnityVector()
            );
        }
        #endregion

        #region operators
        public static Matrix4x4D operator *(Matrix4x4D lhs, Matrix4x4D rhs)
        {
            Matrix4x4D result = new Matrix4x4D();
            result.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30;
            result.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31;
            result.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32;
            result.m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33;
            result.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30;
            result.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31;
            result.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32;
            result.m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33;
            result.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30;
            result.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31;
            result.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32;
            result.m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33;
            result.m30 = lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30;
            result.m31 = lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31;
            result.m32 = lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32;
            result.m33 = lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33;
            return result;
        }

        public static Vector4D operator *(Matrix4x4D lhs, Vector4D vector)
        {
            Vector4D result = new Vector4D();
            result.x = lhs.m00 * vector.x + lhs.m01 * vector.y + lhs.m02 * vector.z + lhs.m03 * vector.w;
            result.y = lhs.m10 * vector.x + lhs.m11 * vector.y + lhs.m12 * vector.z + lhs.m13 * vector.w;
            result.z = lhs.m20 * vector.x + lhs.m21 * vector.y + lhs.m22 * vector.z + lhs.m23 * vector.w;
            result.w = lhs.m30 * vector.x + lhs.m31 * vector.y + lhs.m32 * vector.z + lhs.m33 * vector.w;
            return result;
        }
        #endregion

        #region IEquatable
        public bool Equals(Matrix4x4D other)
        {
            return (
                this.GetColumn(0).Equals(other.GetColumn(0)) &&
                this.GetColumn(1).Equals(other.GetColumn(1)) &&
                this.GetColumn(2).Equals(other.GetColumn(2)) &&
                this.GetColumn(3).Equals(other.GetColumn(3)));
        }
        #endregion
    }
}
