using NUnit.Framework;
using UnityEngine;
using Utils;

namespace Tests.Utils
{
    [TestFixture]
    public class MeshUtilsTest
    {
        [SetUp]
        public void SetUp()
        { }

        [TearDown]
        public void TearDown()
        { }

        [Test]
        public void CopyVerticesToUV2AndUV3_Test()
        {
            Mesh actual = GenerateMesh();
            MeshUtils.CopyVerticesToUV2AndUV3(actual);

            Mesh expected = GenerateMesh();
            expected.uv2 = new Vector2[]{
                new Vector2(1, 2),
                new Vector2(2, 3),
                new Vector2(3, 4),
                new Vector2(6, 7),
                new Vector2(7, 8),
                new Vector2(8, 6),
            };
            expected.uv3 = new Vector2[]{
                new Vector2(3, 0),
                new Vector2(4, 0),
                new Vector2(5, 0),
                new Vector2(8, 0),
                new Vector2(9, 0),
                new Vector2(8, 0),
            };

            Assert.AreEqual(expected.vertices, actual.vertices);
            Assert.AreEqual(expected.normals, actual.normals);
            Assert.AreEqual(expected.triangles, actual.triangles);
            Assert.AreEqual(expected.uv2, actual.uv2);
            Assert.AreEqual(expected.uv3, actual.uv3);
        }

        private Mesh GenerateMesh()
        {
            Vector3 v_a = new Vector3(1, 2, 3);
            Vector3 v_b = new Vector3(2, 3, 4);
            Vector3 v_c = new Vector3(3, 4, 5);
            Vector3 v_d = new Vector3(6, 7, 8);
            Vector3 v_e = new Vector3(7, 8, 9);
            Vector3 v_f = new Vector3(8, 6, 4);

            Vector3 n = Vector3.forward;

            Mesh mesh = new Mesh();
            mesh.vertices = new Vector3[] { v_a, v_b, v_c, v_d, v_e, v_d };
            mesh.normals = new Vector3[] { n, n, n };
            mesh.triangles = new int[] { 0, 1, 2 };


            return mesh;
        }
    }
}