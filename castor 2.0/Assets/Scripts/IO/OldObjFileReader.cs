using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#pragma warning disable CS0649
namespace IO
{
    /// <summary>
    /// Object file reader imports .obj files at runtime.
    /// Source: http://wiki.unity3d.com/index.php?title=ObjImporter
    /// </summary>
    internal class OldObjFileReader
    {
        private struct MeshStruct
        {
            public Vector3[] vertices;
            public Vector3[] normals;
            public Vector2[] uv;
            public Vector2[] uv1;
            public Vector2[] uv2;
            public int[] triangles;
            public int[] faceVerts;
            public int[] faceUVs;
            public Vector3[] faceData;
            public string name;
            public string fileName;
        }

        // Use this for initialization
        public static Mesh ImportFile(string filePath)
        {
            MeshStruct meshInfo = CreateMeshStruct(filePath);
            PopulateMeshStruct(ref meshInfo);

            Vector3[] vertices = new Vector3[meshInfo.faceData.Length];
            Vector3[] normals = new Vector3[meshInfo.faceData.Length];
            int i = 0;
            /* The following foreach loops through the facedata and assigns the appropriate vertex, uv, or normal
             * for the appropriate Unity mesh array. It also performs scaling.
             */
            foreach (Vector3 v in meshInfo.faceData)
            {
                vertices[i] = meshInfo.vertices[(int)v.x - 1];

                if (v.z >= 1) normals[i] = (meshInfo.normals[(int)v.z - 1]).normalized;
                i++;
            }

            Mesh mesh = new Mesh
            {
                vertices = vertices,
                normals = normals,
                triangles = meshInfo.triangles
            };
            mesh.RecalculateBounds();

            return mesh;
        }

        private static MeshStruct CreateMeshStruct(string filename)
        {
            var triangles = 0;
            var vertices = 0;
            var vt = 0;
            var vn = 0;
            var face = 0;
            var mesh = new MeshStruct { fileName = filename };
            var stream = File.OpenText(filename);
            var entireText = stream.ReadToEnd();
            stream.Close();
            using (var reader = new StringReader(entireText))
            {
                var currentText = reader.ReadLine();
                char[] splitIdentifier = { ' ' };
                while (currentText != null)
                {
                    if (!currentText.StartsWith("f ") &&
                        !currentText.StartsWith("v ") &&
                        !currentText.StartsWith("vt ") &&
                        !currentText.StartsWith("vn ")
                    )
                    {
                        currentText = reader.ReadLine();
                        if (currentText != null)
                        {
                            currentText = currentText.Replace("  ", " ");
                        }
                    }
                    else
                    {
                        currentText = currentText.Trim(); //Trim the current line
                        var brokenString = currentText.Split(splitIdentifier,
                            50);
                        switch (brokenString[0])
                        {
                            case "v":
                                vertices++;
                                break;
                            case "vt":
                                vt++;
                                break;
                            case "vn":
                                vn++;
                                break;
                            case "f":
                                face = face + brokenString.Length - 1;
                                triangles = triangles + 3 *
                                            (brokenString.Length -
                                             2); /*brokenString.Length is 3 or greater since a face must have at least
                                                                                     3 vertices.  For each additional vertice, there is an additional
                                                                                     triangle in the mesh (hence this formula).*/
                                break;
                        }
                        currentText = reader.ReadLine();
                        if (currentText != null)
                        {
                            currentText = currentText.Replace("  ", " ");
                        }
                    }
                }
            }
            mesh.triangles = new int[triangles];
            mesh.vertices = new Vector3[vertices];
            mesh.uv = new Vector2[vt];
            mesh.normals = new Vector3[vn];
            mesh.faceData = new Vector3[face];
            return mesh;
        }

        private static void PopulateMeshStruct(ref MeshStruct mesh)
        {
            var stream = File.OpenText(mesh.fileName);
            var entireText = stream.ReadToEnd();
            stream.Close();
            using (var reader = new StringReader(entireText))
            {
                var currentText = reader.ReadLine();

                char[] splitIdentifier = { ' ' };
                char[] splitIdentifier2 = { '/' };
                var f = 0;
                var f2 = 0;
                var v = 0;
                var vn = 0;
                var vt = 0;
                var vt1 = 0;
                var vt2 = 0;
                while (currentText != null)
                {
                    if (!currentText.StartsWith("f ") && !currentText.StartsWith("v ") && !currentText.StartsWith("vt ") &&
                        !currentText.StartsWith("vn ") && !currentText.StartsWith("g ") &&
                        !currentText.StartsWith("usemtl ") &&
                        !currentText.StartsWith("mtllib ") && !currentText.StartsWith("vt1 ") &&
                        !currentText.StartsWith("vt2 ") &&
                        !currentText.StartsWith("vc ") && !currentText.StartsWith("usemap "))
                    {
                        currentText = reader.ReadLine();
                        if (currentText != null)
                        {
                            currentText = currentText.Replace("  ", " ");
                        }
                    }
                    else
                    {
                        currentText = currentText.Trim();
                        var brokenString = currentText.Split(splitIdentifier, 50);
                        switch (brokenString[0])
                        {
                            case "g":
                                break;
                            case "usemtl":
                                break;
                            case "usemap":
                                break;
                            case "mtllib":
                                break;
                            case "v":
                                mesh.vertices[v] = new Vector3(System.Convert.ToSingle(brokenString[1]),
                                    System.Convert.ToSingle(brokenString[2]),
                                    System.Convert.ToSingle(brokenString[3]));
                                v++;
                                break;
                            case "vt":
                                mesh.uv[vt] = new Vector2(System.Convert.ToSingle(brokenString[1]),
                                    System.Convert.ToSingle(brokenString[2]));
                                vt++;
                                break;
                            case "vt1":
                                mesh.uv[vt1] = new Vector2(System.Convert.ToSingle(brokenString[1]),
                                    System.Convert.ToSingle(brokenString[2]));
                                vt1++;
                                break;
                            case "vt2":
                                mesh.uv[vt2] = new Vector2(System.Convert.ToSingle(brokenString[1]),
                                    System.Convert.ToSingle(brokenString[2]));
                                vt2++;
                                break;
                            case "vn":
                                mesh.normals[vn] = new Vector3(System.Convert.ToSingle(brokenString[1]),
                                    System.Convert.ToSingle(brokenString[2]),
                                    System.Convert.ToSingle(brokenString[3]));
                                vn++;
                                break;
                            case "vc":
                                break;
                            case "f":

                                var j = 1;
                                var intArray = new List<int>();
                                while (j < brokenString.Length && ("" + brokenString[j]).Length > 0)
                                {
                                    var temp = new Vector3();
                                    var brokenBrokenString = brokenString[j]
                                        .Split(splitIdentifier2,
                                            3);
                                    temp.x = System.Convert.ToInt32(brokenBrokenString[0]);
                                    if (brokenBrokenString.Length > 1) //Some .obj files skip UV and normal
                                    {
                                        if (brokenBrokenString[1] != "") //Some .obj files skip the uv and not the normal
                                        {
                                            temp.y = System.Convert.ToInt32(brokenBrokenString[1]);
                                        }
                                        temp.z = System.Convert.ToInt32(brokenBrokenString[2]);
                                    }
                                    j++;

                                    mesh.faceData[f2] = temp;
                                    intArray.Add(f2);
                                    f2++;
                                }
                                j = 1;
                                while (j + 2 < brokenString.Length
                                ) //Create triangles out of the face data.  There will generally be more than 1 triangle per face.
                                {
                                    mesh.triangles[f] = intArray[0];
                                    f++;
                                    mesh.triangles[f] = intArray[j];
                                    f++;
                                    mesh.triangles[f] = intArray[j + 1];
                                    f++;

                                    j++;
                                }
                                break;
                        }
                        currentText = reader.ReadLine();
                        if (currentText != null)
                        {
                            currentText =
                                currentText.Replace("  ", " "); //Some .obj files insert double spaces, this removes them.
                        }
                    }
                }
            }
        }
    }
}
#pragma warning restore CS0649