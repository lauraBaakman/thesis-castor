using NUnit.Framework;
using UnityEngine;

namespace Tests.Extensions
{
    [TestFixture]
    public class MeshExtensionTests
    {
        Mesh thisMesh;

        Vector3 p0 = new Vector3(2, 3, 4);
        Vector3 p1 = new Vector3(7, 3, 5);
        Vector3 p2 = new Vector3(6, 4, 9);
        Vector3 p3 = new Vector3(4, 6, 7);

        [SetUp]
        public void SetUp()
        {
            thisMesh = startMesh();
        }

        private Mesh startMesh()
        {
            Mesh mesh = new Mesh();

            mesh.vertices = new Vector3[]{
                p0, p1, p3,
                p0, p2, p1,
                p0, p3, p2,
                p1, p2, p3,
            };

            mesh.triangles = new int[]{
                00, 01, 02,
                03, 04, 05,
                06, 07, 08,
                09, 10, 11,
            };

            mesh.normals = new Vector3[]{
                new Vector3(0, +1, 0).normalized, new Vector3(0, +1, 0).normalized, new Vector3(0, +1, 0).normalized,
                new Vector3(0, 0, -1).normalized, new Vector3(0, 0, -1).normalized, new Vector3(0, 0, -1).normalized,
                new Vector3(-1, 0, 0).normalized, new Vector3(-1, 0, 0).normalized, new Vector3(-1, 0, 0).normalized,
                new Vector3(+1, 0, 0).normalized, new Vector3(+1, 0, 0).normalized, new Vector3(+1, 0, 0).normalized
            };

            return mesh;
        }

        [Test]
        public void Test_Equals_Equal()
        {
            Mesh otherMesh = startMesh();

            Assert.IsTrue(thisMesh.MeshEquals(otherMesh));
            Assert.IsTrue(otherMesh.MeshEquals(thisMesh));
        }

        [Test]
        public void Test_Equals_VerticesDifferent_order()
        {
            Mesh otherMesh = startMesh();

            otherMesh.vertices = new Vector3[]{
                p1, p2, p3,
                p0, p1, p3,
                p0, p2, p1,
                p0, p3, p2,
            };

            Assert.IsFalse(thisMesh.MeshEquals(otherMesh));
            Assert.IsFalse(otherMesh.MeshEquals(thisMesh));
        }

        [Test]
        public void Test_Equals_VerticesDifferent_content()
        {
            Mesh otherMesh = startMesh();

            otherMesh.vertices = new Vector3[]{
                p3, p2, p3,
                p0, p3, p3,
                p0, p2, p3,
                p0, p3, p2,
            };

            Assert.IsFalse(thisMesh.MeshEquals(otherMesh));
            Assert.IsFalse(otherMesh.MeshEquals(thisMesh));
        }

        [Test]
        public void Test_Equals_NormalsDifferent_order()
        {
            Mesh otherMesh = startMesh();

            thisMesh.normals = new Vector3[]{
                new Vector3(+1, 0, 0).normalized, new Vector3(+1, 0, 0).normalized, new Vector3(+1, 0, 0).normalized,
                new Vector3(0, +1, 0).normalized, new Vector3(0, +1, 0).normalized, new Vector3(0, +1, 0).normalized,
                new Vector3(0, 0, -1).normalized, new Vector3(0, 0, -1).normalized, new Vector3(0, 0, -1).normalized,
                new Vector3(-1, 0, 0).normalized, new Vector3(-1, 0, 0).normalized, new Vector3(-1, 0, 0).normalized,
            };

            Assert.IsFalse(thisMesh.MeshEquals(otherMesh));
            Assert.IsFalse(otherMesh.MeshEquals(thisMesh));
        }

        [Test]
        public void Test_Equals_NormalsDifferent_content()
        {
            Mesh otherMesh = startMesh();

            otherMesh.normals = new Vector3[]{
                new Vector3(+1, +1, 0).normalized, new Vector3(+1, +1, 0).normalized, new Vector3(+1, +1, 0).normalized,
                new Vector3(0, 0, -1).normalized, new Vector3(0, 0, -1).normalized, new Vector3(0, 0, -1).normalized,
                new Vector3(-1, 0, 0).normalized, new Vector3(-1, 0, 0).normalized, new Vector3(-1, 0, 0).normalized,
                new Vector3(+1, 0, 0).normalized, new Vector3(+1, 0, 0).normalized, new Vector3(+1, 0, 0).normalized
            };

            Assert.IsFalse(thisMesh.MeshEquals(otherMesh));
            Assert.IsFalse(otherMesh.MeshEquals(thisMesh));
        }

        [Test]
        public void Test_Equals_TrianglesDifferent_length()
        {
            Mesh otherMesh = startMesh();

            otherMesh.triangles = new int[]{
                00, 01, 02,
                03, 04, 05,
                09, 10, 11,
            };

            Assert.IsFalse(thisMesh.MeshEquals(otherMesh));
            Assert.IsFalse(otherMesh.MeshEquals(thisMesh));
        }

        [Test]
        public void Test_Equals_TrianglesDifferent_order()
        {
            Mesh otherMesh = startMesh();

            otherMesh.triangles = new int[]{
                00, 01, 02,
                03, 04, 05,
                09, 10, 11,
                06, 07, 08,
            };

            Assert.IsFalse(thisMesh.MeshEquals(otherMesh));
            Assert.IsFalse(otherMesh.MeshEquals(thisMesh));
        }

        [Test]
        public void Test_Equals_TrianglesDifferent_content()
        {
            Mesh otherMesh = startMesh();

            otherMesh.triangles = new int[]{
                01, 03, 02,
                03, 04, 05,
                06, 07, 08,
                09, 10, 11,
            };

            Assert.IsFalse(thisMesh.MeshEquals(otherMesh));
            Assert.IsFalse(otherMesh.MeshEquals(thisMesh));
        }

        [Test]
        public void Test_SetWindingOrderCCW_CCWInput()
        {
            Mesh actual = CCWRectangularMesh();
            actual.SetWindingOrderToCCW();

            Mesh expected = CCWRectangularMesh();

            Assert.IsTrue(actual.MeshEquals(expected));
        }

        [Test]
        public void Test_SetWindingOrderCCW_CWInput()
        {
            Mesh actual = CWRectangularMesh();
            actual.SetWindingOrderToCCW();

            Mesh expected = CCWRectangularMesh();

            Assert.IsTrue(actual.MeshEquals(expected));
        }

        private static Mesh CCWRectangularMesh()
        {
            Mesh CCWMesh = new Mesh();

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

            CCWMesh.vertices = vertices;
            CCWMesh.triangles = triangles;
            CCWMesh.normals = normals;

            return CCWMesh;
        }

        private static Mesh CWRectangularMesh()
        {
            Mesh CWMesh = new Mesh();

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

            CWMesh.vertices = vertices;
            CWMesh.triangles = triangles;
            CWMesh.normals = normals;

            return CWMesh;
        }
    }
}