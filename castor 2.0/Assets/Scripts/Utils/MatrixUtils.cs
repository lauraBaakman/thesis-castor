using UnityEngine;
using System;
namespace Utils
{
	public static class MatrixUtils
	{
		public static Matrix4x4 TransformationMatrixFromUnitQuaternion(Quaternion unitQuaternion)
		{
			//Use Wheeler notation to represnt the quaternion elements
			float s = unitQuaternion.w;

			float u = unitQuaternion.x;
			float v = unitQuaternion.y;
			float w = unitQuaternion.z;

			float s2 = s * s;
			float u2 = u * u;
			float v2 = v * v;
			float w2 = w * w;

			Matrix4x4 matrix = Matrix4x4.identity;

			matrix.m00 = s2 + u2 - v2 - w2;
			matrix.m01 = 2 * (u * v - s * w);
			matrix.m02 = 2 * (u * w + s * v);

			matrix.m10 = 2 * (u * v + s * w);
			matrix.m11 = s2 - u2 + v2 - w2;
			matrix.m12 = 2 * (v * w - s * u);

			matrix.m20 = 2 * (u * w - s * v);
			matrix.m21 = 2 * (v * w + s * u);
			matrix.m22 = s2 - u2 - v2 + w2;

			return matrix;
		}

		public static Matrix4x4 TransformationMatrixFromTranslation(Vector3 translation)
		{
			Matrix4x4 matrix = Matrix4x4.identity;
			matrix.m03 = translation.x;
			matrix.m13 = translation.y;
			matrix.m23 = translation.z;

			return matrix;
		}

		public static Matrix4x4 MultiplyWithScalar(Matrix4x4 matrix, float scalar)
		{
			Matrix4x4 result = new Matrix4x4();

			for (int i = 0; i < 16; i++) result[i] = scalar * matrix[i];

			return result;
		}

		public static Matrix4x4 TransformationMatrixFromQuaternion(Quaternion quaternion)
		{
			return MatrixUtils.MultiplyWithScalar(
				matrix: TransformationMatrixFromUnitQuaternion(quaternion),
					scalar: 1.0f / quaternion.Dot(quaternion)
			);
		}
	}
}