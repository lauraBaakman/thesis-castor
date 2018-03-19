using System.IO;
using NUnit.Framework;
using UnityEngine;
using IO;

namespace Tests.IO
{
    [TestFixture]
    public class ObjFileReader
    {
        string outputPath;

        Mesh expected;

        [SetUp]
        public void SetUp()
        {
            this.outputPath = Path.GetTempFileName();

            expected = new Mesh();

            Vector3 p1 = new Vector3(5, 7, 2);
            Vector3 p2 = new Vector3(5, 3, 3);
            Vector3 p3 = new Vector3(2, 3, 2.5f);
            Vector3 p4 = new Vector3(2, 5, 3f);

            Vector3 normal = Vector3.forward;

            Vector3[] vertices = { p1, p2, p3, p4 };
            Vector3[] normals = { normal, normal, normal, normal };
            int[] triangles = {
                0, 2, 1,
                0, 3, 2,
            };

            expected.vertices = vertices;
            expected.triangles = triangles;
            expected.normals = normals;
        }

        [TearDown]
        public void TearDown()
        {
            UnityEditor.FileUtil.DeleteFileOrDirectory(this.outputPath);
        }

        [Test]
        [Ignore("The OBJ reader makes a mess of everything, near impossible to actually compare meshes.")]
        public void ImportFile_CW()
        {
            Vector3 p1 = new Vector3(5, 7, 2);
            Vector3 p2 = new Vector3(5, 3, 3);
            Vector3 p3 = new Vector3(2, 3, 2.5f);
            Vector3 p4 = new Vector3(2, 5, 3f);

            Vector3 normal = Vector3.forward;

            Vector3[] vertices = { p1, p2, p3, p4 };
            Vector3[] normals = { normal, normal, normal, normal };
            int[] triangles = {
                0, 1, 2,
                2, 3, 0
            };

            Mesh CWmesh = new Mesh();
            CWmesh.vertices = vertices;
            CWmesh.triangles = triangles;
            CWmesh.normals = normals;

            ObjFile.Write(CWmesh, outputPath);

            ReadResult actual = ObjFile.Read(outputPath);

            Assert.IsTrue(expected.MeshEquals(actual.Mesh));
        }

        [Test]
        [Ignore("The OBJ reader makes a mess of everything, near impossible to actually compare meshes.")]
        public void ImportFile_CCW()
        {
            ObjFile.Write(expected, outputPath);

            ReadResult actual = ObjFile.Read(outputPath);

            Assert.IsTrue(expected.MeshEquals(actual.Mesh));
        }
    }
}