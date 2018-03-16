using UnityEngine;
using System.IO;
using System.Text;

namespace IO
{
    //Adapted from http://wiki.unity3d.com/index.php?title=ObjExporter
    internal static class ObjExporter
    {

        public static string MeshToString(Mesh mesh)
        {
#pragma warning disable XS0001 // Find APIs marked as TODO in Mono
            StringBuilder sb = new StringBuilder();
#pragma warning restore XS0001 // Find APIs marked as TODO in Mono

            sb.Append("g ").Append(mesh.name).Append("\n");
            foreach (Vector3 v in mesh.vertices)
            {
                sb.Append(string.Format("v {0} {1} {2}\n", v.x, v.y, v.z));
            }
            sb.Append("\n");
            foreach (Vector3 v in mesh.normals)
            {
                sb.Append(string.Format("vn {0} {1} {2}\n", v.x, v.y, v.z));
            }
            sb.Append("\n");
            foreach (Vector3 v in mesh.uv)
            {
                sb.Append(string.Format("vt {0} {1}\n", v.x, v.y));
            }
            for (int material = 0; material < mesh.subMeshCount; material++)
            {
                sb.Append("\n");
                int[] triangles = mesh.GetTriangles(0);
                for (int i = 0; i < triangles.Length; i += 3)
                {
                    sb.Append(string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n",
                        triangles[i] + 1, triangles[i + 1] + 1, triangles[i + 2] + 1));
                }
            }
            return sb.ToString();
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
}