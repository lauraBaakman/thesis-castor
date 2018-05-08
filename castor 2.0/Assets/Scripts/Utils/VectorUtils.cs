using System;
using UnityEngine;

namespace Utils
{
	public static class VectorUtils
	{
		/// <summary>
		/// Convert the Vector3 to a Homogeneous position coordinate.
		/// </summary>
		/// <returns>The homogeneous coordinate.</returns>
		/// <param name="position">The position.</param>
		public static Vector4 HomogeneousCoordinate(Vector3 position)
		{
			return new Vector4(position.x, position.y, position.z, 1);
		}

		/// <summary>
		/// Compute the cross product of the homogeneous vectors
		/// a = [ax, ay, az, 1] and b = [bx, by, bz, 1]. The results is defined
		///  as [[ax, ay, az] x [bx, by, bz], 1]
		/// </summary>
		/// <returns>The cross of the two vectors.</returns>
		/// <param name="lhs">Lhs.</param>
		/// <param name="rhs">Rhs.</param>
		public static Vector4 Cross(Vector4 lhs, Vector4 rhs)
		{
			return new Vector4(
				lhs.y * rhs.z - lhs.z * rhs.y,
				lhs.z * rhs.x - lhs.x * rhs.z,
				lhs.x * rhs.y - lhs.y * rhs.x,
				0
			);
		}

		/// <summary>
		/// Multiply the vector with the scalar.
		/// </summary>
		/// <returns>The vector multiplied with the scalar.</returns>
		/// <param name="vector">Vector.</param>
		/// <param name="scalar">Scalar.</param>
		public static Vector4 MultiplyWithScalar(Vector4 vector, float scalar)
		{
			return new Vector4(
				vector.x * scalar,
				vector.y * scalar,
				vector.z * scalar,
				vector.w * scalar
			);
		}
	}
}