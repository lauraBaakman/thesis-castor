using UnityEngine;
using System.IO;
using System.Text;

namespace IO
{
#pragma warning disable XS0001 // Find APIs marked as TODO in Mono
    //Adapted from http://wiki.unity3d.com/index.php?title=ObjExporter
    internal static class ObjExporter
    {

        public static string MeshToString(Mesh mesh)
        {
            StringBuilder sb = new StringBuilder();

            WriteObjectName(sb, mesh.name);
            WriteVertices(sb, mesh.vertices);
            WriteNormals(sb, mesh.normals);
            WriteTriangles(sb, mesh.triangles);

            return sb.ToString();
        }

        private static void WriteObjectName(StringBuilder sb, string name)
        {
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

        private static void WriteNormals(StringBuilder sb, Vector3[] normals)
        {
            foreach (Vector3 v in normals)
            {
                sb.Append(string.Format("vn {0} {1} {2}\n", v.x, v.y, v.z));
            }
            sb.Append("\n");
        }

        private static void WriteTriangles(StringBuilder sb, int[] triangles)
        {
            for (int i = 0; i < triangles.Length; i += 3)
            {
                sb.Append(string.Format("f {0}//{0} {1}//{1} {2}//{2}\n",
                    triangles[i] + 1, triangles[i + 1] + 1, triangles[i + 2] + 1));
            }
        }

        public static void MeshToFile(Mesh mesh, string path)
        {
            ValidateMesh(mesh);

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(MeshToString(mesh));
            }
        }

        private static void ValidateMesh(Mesh mesh)
        {
            if (mesh.subMeshCount != 1) throw new System.ArgumentException("The writer can only handle meshes without submeshes.");
        }
    }
#pragma warning restore XS0001 // Find APIs marked as TODO in Mono
}