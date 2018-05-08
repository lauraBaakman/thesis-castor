using UnityEngine;
using System.IO;
using System.Text;
using System;

namespace IO
{
#pragma warning disable XS0001 // Find APIs marked as TODO in Mono
	//Adapted from http://wiki.unity3d.com/index.php?title=ObjExporter
	internal static class ObjExporter
	{

		public static string MeshToString(Mesh mesh)
		{
			StringBuilder sb = new StringBuilder();

			WriteInfoString(sb);
			WriteObjectName(sb, mesh.name);
			WriteVertices(sb, mesh.vertices);
			WriteNormals(sb, mesh);
			WriteTextures(sb, mesh);
			WriteTriangles(sb, mesh);

			return sb.ToString();
		}

		private static void WriteInfoString(StringBuilder sb)
		{
			sb.Append(string.Format(
				"# Generated this file using CAstOR on {0}\n",
				System.DateTime.Now.ToLocalTime().ToString()));
		}

		private static void WriteObjectName(StringBuilder sb, string name)
		{
			name = name.Equals("") ? "no name" : name;
			sb.Append("o ").Append(name).Append("\n");
		}

		private static void WriteVertices(StringBuilder sb, Vector3[] vertices)
		{
			foreach (Vector3 v in vertices)
			{
				sb.Append(string.Format("v {0} {1} {2}\n", v.x, v.y, v.z));
			}
			sb.Append("\n");
		}

		private static void WriteNormals(StringBuilder sb, Mesh mesh)
		{
			if (!HasNormals(mesh)) return;
			Vector3[] normals = mesh.normals;

			foreach (Vector3 v in normals)
			{
				sb.Append(string.Format("vn {0} {1} {2}\n", v.x, v.y, v.z));
			}
			sb.Append("\n");
		}

		private static void WriteTextures(StringBuilder sb, Mesh mesh)
		{
			if (!HasTextures(mesh)) return;

			Vector2[] uv2 = mesh.uv2;
			Vector2[] uv3 = mesh.uv3;

			Vector3 vt;
			for (int i = 0; i < uv2.Length; i++)
			{
				vt.x = uv2[i].x;
				vt.y = uv2[i].y;
				vt.z = uv3[i].x;

				sb.Append(string.Format("vt {0} {1} {2}\n", vt.x, vt.y, vt.z));
			}
			sb.Append("\n");
		}

		private static void WriteTriangles(StringBuilder sb, Mesh mesh)
		{
			if (HasNormals(mesh))
			{
				if (HasTextures(mesh)) WriteTrianglesWithNormalsWithTextures(sb, mesh);
				else WriteTrianglesWithNormalsNoTextures(sb, mesh);
			}
			else WriteTrianglesNoNormalsNoTextures(sb, mesh);
		}

		private static void WriteTrianglesNoNormalsNoTextures(StringBuilder sb, Mesh mesh)
		{
			int[] triangles = mesh.triangles;
			for (int i = 0; i < triangles.Length; i += 3)
			{
				sb.Append(string.Format("f {0} {1} {2}\n",
					triangles[i + 0] + 1, triangles[i + 1] + 1, triangles[i + 2] + 1));
			}
		}

		private static void WriteTrianglesWithNormalsNoTextures(StringBuilder sb, Mesh mesh)
		{
			int[] triangles = mesh.triangles;
			for (int i = 0; i < triangles.Length; i += 3)
			{
				sb.Append(string.Format("f {0}//{0} {1}//{1} {2}//{2}\n",
					triangles[i + 0] + 1, triangles[i + 1] + 1, triangles[i + 2] + 1));
			}
		}

		private static void WriteTrianglesWithNormalsWithTextures(StringBuilder sb, Mesh mesh)
		{
			int[] triangles = mesh.triangles;
			for (int i = 0; i < triangles.Length; i += 3)
			{
				sb.Append(string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n",
					triangles[i + 0] + 1, triangles[i + 1] + 1, triangles[i + 2] + 1));
			}
		}

		private static bool HasNormals(Mesh mesh)
		{
			return mesh.normals.Length > 0;
		}

		private static bool HasTextures(Mesh mesh)
		{
			return mesh.uv2.Length > 0 && mesh.uv3.Length > 0;
		}

		public static void MeshToFile(Mesh mesh, string path)
		{
			ValidateMesh(mesh);

			using (StreamWriter sw = new StreamWriter(path))
			{
				try { sw.Write(MeshToString(mesh)); }
				catch (Exception e) { throw e; }
			}
		}

		private static void ValidateMesh(Mesh mesh)
		{
			if (mesh.subMeshCount != 1) throw new System.ArgumentException("The writer can only handle meshes without submeshes.");
		}
	}
#pragma warning restore XS0001 // Find APIs marked as TODO in Mono
}