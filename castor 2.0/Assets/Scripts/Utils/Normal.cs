using Registration;
using UnityEngine;
using DoubleConnectedEdgeList;

namespace Utils
{
	public class Normal
	{
		private static float magnitudeFactor = 0.5f;

		public readonly Vector3 Start;
		public readonly Vector3 End;

		public Normal(Point point) :
		this(
				position: point.Position,
				direction: point.Normal
			)
		{ }

		public Normal(Vector3 position, Vector3 direction)
		{
			Start = position;
			End = ComputeEnd(position, direction);
		}

		public static float MagnitudeFactor
		{
			get { return magnitudeFactor; }
			set { magnitudeFactor = value; }
		}

		private Vector3 ComputeEnd(Vector3 position, Vector3 direction)
		{
			return position + direction * magnitudeFactor * direction.magnitude;
		}

		public static Normal FaceNormal(Face face)
		{
			return new Normal(face.Centroid, face.Normal);
		}
	}
}