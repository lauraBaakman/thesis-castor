using UnityEngine;
using System;
namespace Utils
{
    public static class MatrixUtils
    {
        public static Matrix4x4 TransformationMatrixFromUnitQuaternion(Quaternion unitQuaternion)
        {
            throw new NotImplementedException();
        }

        public static Matrix4x4 TransformationMatrixFromTranslation(Vector3 translation)
        {
            Matrix4x4 matrix = Matrix4x4.identity;
            matrix.m03 = translation.x;
            matrix.m13 = translation.y;
            matrix.m23 = translation.z;

            return matrix;
        }

        public static Matrix4x4 TransformationMatrixFromQuaternion(Quaternion quaternion)
        {
            throw new NotImplementedException();
        }

        public static Matrix4x4 FillDiagonal()
        {
            throw new NotImplementedException();
        }
    }
}