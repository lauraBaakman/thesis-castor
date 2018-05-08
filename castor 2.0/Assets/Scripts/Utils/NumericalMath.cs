using UnityEngine;

namespace Utils
{
	public static class NumericalMath
	{
		public static Vector3 NewellsMethod(Vector3 a, Vector3 b, Vector3 c)
		{
			return NewellsMethod(new Vector3[] { a, b, c });
		}

		public static Vector3 NewellsMethod(Vector3[] vertices)
		{
			int numVertices = vertices.Length;

			if (numVertices < 3) throw new System.ArgumentException("Input at least three vertices to determine the face normal.");

			Vector3 normal = new Vector3();
			Vector3 current, next;

			for (int currentIdx = 0, nextIdx = 1; currentIdx < numVertices; currentIdx++, nextIdx = (nextIdx + 1) % numVertices)
			{
				current = vertices[currentIdx];
				next = vertices[nextIdx];

				normal.x += (current.y - next.y) * (current.z + next.z);
				normal.y += (current.z - next.z) * (current.x + next.x);
				normal.z += (current.x - next.x) * (current.y + next.y);
			}
			return normal.normalized;
		}
	}
}