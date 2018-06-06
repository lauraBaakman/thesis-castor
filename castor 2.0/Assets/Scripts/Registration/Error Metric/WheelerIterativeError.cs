using UnityEngine;
using System.Collections.Generic;
using Utils;

namespace Registration.Error
{
	/// <summary>
	/// Implements the error proposed by Wheeler, M. D., and K. Ikeuchi.
	/// "Iterative estimation of rotation and translation using the
	/// quaternion: School of Computer Science." (1995).
	/// </summary>
	public class WheelerIterativeError : IIterativeErrorMetric
	{
		public double ComputeError(List<Vector4> XCs, List<Vector4> Ps, Vector4 translation, object sharedParameters)
		{
			int N = XCs.Count;

			double error = 0;
			for (int i = 0; i < N; i++)
			{
				error += ComputeError(XCs[i], Ps[i], translation);
			}
			error /= N;

			return error;
		}

		private double ComputeError(Vector4 Xc, Vector4 p, Vector4 translation)
		{
			Vector4 distance = Xc + translation - p;
			return distance.SqrMagnitude();
		}

		/// <summary>
		/// Computes the rotational gradient.
		/// </summary>
		/// <returns>The gradient.</returns>
		/// <param name="XCs">The model points, premultiplied with the rotation matrix.</param>
		/// <param name="Ps">The static points.</param>
		/// <param name="translation">The current translation vector.</param>
		public Quaternion RotationalGradient(List<Vector4> XCs, List<Vector4> Ps, Vector4 translation, object sharedParameters)
		{
			int N = XCs.Count;
			Vector4 gradient = new Vector4();
			for (int i = 0; i < N; i++)
			{
				gradient += RotationalGradient(XCs[i], Ps[i], translation);
			}

			gradient = VectorUtils.MultiplyWithScalar(
				vector: gradient,
				scalar: 1.0f / (2.0f * N)
			);

			//Wheeler: The gradient w.r.t. to q will have no w component
			return new Quaternion(x: gradient.x, y: gradient.y, z: gradient.z, w: 0);
		}

		public Vector4 RotationalGradient(Vector4 Xc, Vector4 p, Vector4 translation)
		{
			Vector4 gradient = VectorUtils.MultiplyWithScalar(
				vector: VectorUtils.Cross(Xc, translation - p),
				scalar: -4
			);
			gradient.w = 0;
			return gradient;
		}

		/// <summary>
		/// Computes the translational gradient.
		/// </summary>
		/// <returns>The gradient.</returns>
		/// <param name="XCs">The model points, premultiplied with the rotation matrix.</param>
		/// <param name="Ps">The static points.</param>
		/// <param name="translation">The current translation vector.</param>
		public Vector4 TranslationalGradient(List<Vector4> XCs, List<Vector4> Ps, Vector4 translation, object sharedParameters)
		{
			int N = XCs.Count;
			Vector4 gradient = new Vector4();
			for (int i = 0; i < N; i++)
			{
				gradient += TranslationalGradient(XCs[i], Ps[i], translation);
			}
			return VectorUtils.MultiplyWithScalar(gradient, 1.0f / (2.0f * N));
		}

		private Vector4 TranslationalGradient(Vector4 Xc, Vector4 p, Vector4 translation)
		{
			return 2 * (Xc + translation - p);
		}

		public float ComputeError(CorrespondenceCollection correspondences, Transform originalTransform, Transform newTransform)
		{
			return ErrorMetric.Wheeler().ComputeError(correspondences, originalTransform, newTransform);
		}

		public float ComputeInitialError(CorrespondenceCollection correspondences)
		{
			return ErrorMetric.Wheeler().ComputeInitialError(correspondences);
		}

		public float ComputeTerminationError(CorrespondenceCollection correspondences, Transform originalTransform, Transform currentTransform)
		{
			return ErrorMetric.Wheeler().ComputeTerminationError(correspondences, originalTransform, currentTransform);
		}

		public void Set(GameObject staticModel, Transform referenceTransform)
		{
			//Do nothing, we don't need the static model, no need to store a reference to it.
		}

		public object ComputeSharedParameters(List<Vector4> modelPoints, List<Vector4> staticPoints, Vector4 translation)
		{
			return null;
		}

		public SerializableErrorMetric Serialize()
		{
			return new SerializableErrorMetric(
				distanceWeight: 1, intersectionWeight: 0,
				aggregationMethod: "mean", distanceMethod: "squared euclidean",
				normalizePoints: 0
			);
		}
	}
}