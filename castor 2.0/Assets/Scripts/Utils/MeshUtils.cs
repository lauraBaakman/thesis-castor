using UnityEngine;

namespace Utils
{
	public static class MeshUtils
	{
		public static void CopyVerticesToUV2AndUV3(Mesh mesh)
		{
			Vector2[] uv2 = new Vector2[mesh.vertices.Length];
			Vector2[] uv3 = new Vector2[mesh.vertices.Length];

			Vector3 vertex;

			for (int i = 0; i < mesh.vertices.Length; i++)
			{
				vertex = mesh.vertices[i];
				uv2[i] = new Vector2(vertex.x, vertex.y);
				uv3[i] = new Vector2(vertex.z, 0);
			}
			mesh.uv2 = uv2;
			mesh.uv3 = uv3;
		}
	}
}
