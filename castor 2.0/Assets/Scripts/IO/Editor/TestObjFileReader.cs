using System.IO;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using IO;

namespace Tests.IO
{
    [TestFixture]
    public class ObjFileReaderTests
    {

        private string InputPath(string file)
        {
            return Path.Combine(Application.dataPath, Path.Combine("Scripts/IO/Editor", file));
        }

        [Test]
        public void Test_ReadCube()
        {
            string inputPath = InputPath("cube.obj");

            Mesh mesh = new Mesh();

            Vector3[] vertices = {
                //face 1
                new Vector3(-1.000000f, -1.000000f, -1.000000f),
                new Vector3(+1.000000f, -1.000000f, +1.000000f),
                new Vector3(-1.000000f, -1.000000f, +1.000000f),
                //face 2
                new Vector3(+1.000000f, +1.000000f, +1.000000f),
                new Vector3(-0.999999f, +1.000000f, -1.000001f),
                new Vector3(-1.000000f, +1.000000f, +0.999999f),
                //face 3
                new Vector3(-1.000000f, +1.000000f, +0.999999f),
                new Vector3(-1.000000f, -1.000000f, -1.000000f),
                new Vector3(-1.000000f, -1.000000f, +1.000000f),
                //face 4
                new Vector3(-0.999999f, +1.000000f, -1.000001f),
                new Vector3(+1.000000f, -1.000000f, -1.000000f),
                new Vector3(-1.000000f, -1.000000f, -1.000000f),
                //face 5
                new Vector3(+1.000000f, -1.000000f, -1.000000f),
                new Vector3(+1.000000f, +1.000000f, +1.000000f),
                new Vector3(+1.000000f, -1.000000f, +1.000000f),
                //face 6
                new Vector3(-1.000000f, -1.000000f, +1.000000f),
                new Vector3(+1.000000f, +1.000000f, +1.000000f),
                new Vector3(-1.000000f, +1.000000f, +0.999999f),
                //face 7
                new Vector3(-1.000000f, -1.000000f, -1.000000f),
                new Vector3(+1.000000f, -1.000000f, -1.000000f),
                new Vector3(+1.000000f, -1.000000f, +1.000000f),
                //face 8
                new Vector3(+1.000000f, +1.000000f, +1.000000f),
                new Vector3(+1.000000f, +1.000000f, -1.000000f),
                new Vector3(-0.999999f, +1.000000f, -1.000001f),
                //face 9
                new Vector3(-1.000000f, +1.000000f, +0.999999f),
                new Vector3(-0.999999f, +1.000000f, -1.000001f),
                new Vector3(-1.000000f, -1.000000f, -1.000000f),
                //face 10
                new Vector3(-0.999999f, +1.000000f, -1.000001f),
                new Vector3(+1.000000f, +1.000000f, -1.000000f),
                new Vector3(+1.000000f, -1.000000f, -1.000000f),
                //face 11
                new Vector3(+1.000000f, -1.000000f, -1.000000f),
                new Vector3(+1.000000f, +1.000000f, -1.000000f),
                new Vector3(+1.000000f, +1.000000f, +1.000000f),
                //face 12
                new Vector3(-1.000000f, -1.000000f, +1.000000f),
                new Vector3(+1.000000f, -1.000000f, +1.000000f),
                new Vector3(+1.000000f, +1.000000f, +1.000000f),
            };
            mesh.vertices = vertices;

            Vector3[] normals = {
                //face 1
                new Vector3(+0.0000f, -1.0000f, +0.0000f),
                new Vector3(+0.0000f, -1.0000f, +0.0000f),
                new Vector3(+0.0000f, -1.0000f, +0.0000f),
                //face 2
                new Vector3(+0.0000f, +1.0000f, -0.0000f),
                new Vector3(+0.0000f, +1.0000f, -0.0000f),
                new Vector3(+0.0000f, +1.0000f, -0.0000f),
                //face 3
                new Vector3(-1.0000f, -0.0000f, +0.0000f),
                new Vector3(-1.0000f, -0.0000f, +0.0000f),
                new Vector3(-1.0000f, -0.0000f, +0.0000f),
                //face 4
                new Vector3(+0.0000f, -0.0000f, -1.0000f),
                new Vector3(+0.0000f, -0.0000f, -1.0000f),
                new Vector3(+0.0000f, -0.0000f, -1.0000f),
                //face 5
                new Vector3(+1.0000f, -0.0000f, +0.0000f),
                new Vector3(+1.0000f, -0.0000f, +0.0000f),
                new Vector3(+1.0000f, -0.0000f, +0.0000f),
                //face 6
                new Vector3(-0.0000f, +0.0000f, +1.0000f),
                new Vector3(-0.0000f, +0.0000f, +1.0000f),
                new Vector3(-0.0000f, +0.0000f, +1.0000f),
                //face 7
                new Vector3(+0.0000f, -1.0000f, +0.0000f),
                new Vector3(+0.0000f, -1.0000f, +0.0000f),
                new Vector3(+0.0000f, -1.0000f, +0.0000f),
                //face 8
                new Vector3(+0.0000f, +1.0000f, -0.0000f),
                new Vector3(+0.0000f, +1.0000f, -0.0000f),
                new Vector3(+0.0000f, +1.0000f, -0.0000f),
                //face 9
                new Vector3(-1.0000f, -0.0000f, +0.0000f),
                new Vector3(-1.0000f, -0.0000f, +0.0000f),
                new Vector3(-1.0000f, -0.0000f, +0.0000f),
                //face 10
                new Vector3(+0.0000f, -0.0000f, -1.0000f),
                new Vector3(+0.0000f, -0.0000f, -1.0000f),
                new Vector3(+0.0000f, -0.0000f, -1.0000f),
                //face 11
                new Vector3(+1.0000f, -0.0000f, +0.0000f),
                new Vector3(+1.0000f, -0.0000f, +0.0000f),
                new Vector3(+1.0000f, -0.0000f, +0.0000f),
                //face 12
                new Vector3(-0.0000f, +0.0000f, +1.0000f),
                new Vector3(-0.0000f, +0.0000f, +1.0000f),
                new Vector3(-0.0000f, +0.0000f, +1.0000f),
            };
            mesh.normals = normals;

            int[] triangles = {
                00, 01, 02,
                03, 04, 05,
                06, 07, 08,
                09, 10, 11,
                12, 13, 14,
                15, 16, 17,
                18, 19, 20,
                21, 22, 23,
                24, 25, 26,
                27, 28, 29,
                30, 31, 32,
                33, 34, 35
            };
            mesh.triangles = triangles;

            ReadResult expected = ReadResult.OKResult(inputPath, mesh);
            ReadResult actual = new ObjFileReader(inputPath).ImportFile();

            Assert.IsTrue(actual.Succeeded());
            Assert.AreEqual(expected.Mesh.vertices, actual.Mesh.vertices);
            Assert.AreEqual(expected.Mesh.normals, actual.Mesh.normals);
            Assert.AreEqual(expected.Mesh.triangles, actual.Mesh.triangles);
        }

        [Test]
        public void Test_InvalidFile()
        {
            string inputPath = InputPath("nonexistentfile.obj");

            ReadResult expected = ReadResult.ErrorResult(inputPath, new FileNotFoundException());
            ReadResult actual = new ObjFileReader(inputPath).ImportFile();

            Assert.IsFalse(actual.Succeeded());
            Assert.IsTrue(actual.Failed());
        }
    }

    [TestFixture]
    public class CommentReaderTests
    {

        CommentReader reader;

        [SetUp]
        public void SetUp()
        {
            reader = new CommentReader();
        }

        [TestCase("# some comment", true)]
        [TestCase("v some vertex", false)]
        [TestCase("vn some normal", false)]
        [TestCase("vt some texture", false)]
        [TestCase("f some face", false)]
        [TestCase("p some point", false)]
        [TestCase("l some line", false)]
        [TestCase("curv2 some 2D curve", false)]
        [TestCase("surf some surface", false)]
        [TestCase("g some group", false)]
        [TestCase("s some smoothing group", false)]
        [TestCase("mg some merging group", false)]
        [TestCase("o some object name", false)]
        public void IsApplicableReaderTest(string line, bool expected)
        {
            bool actual = reader.IsApplicable(line);
            Assert.AreEqual(expected, actual);
        }
    }

    [TestFixture]
    public class VertexReaderTests
    {
        VertexReader reader;

        [SetUp]
        public void SetUp()
        {
            reader = new VertexReader();
        }

        [TestCase("# some comment", false)]
        [TestCase("v some vertex", true)]
        [TestCase("vn some normal", false)]
        [TestCase("vt some texture", false)]
        [TestCase("f some face", false)]
        [TestCase("p some point", false)]
        [TestCase("l some line", false)]
        [TestCase("curv2 some 2D curve", false)]
        [TestCase("surf some surface", false)]
        [TestCase("g some group", false)]
        [TestCase("s some smoothing group", false)]
        [TestCase("mg some merging group", false)]
        [TestCase("o some object name", false)]
        public void IsApplicableReaderTest(string line, bool expected)
        {
            bool actual = reader.IsApplicable(line);
            Assert.AreEqual(expected, actual);
        }


        [TestCase("v 1 2 3", 1, 2, 3)]
        [TestCase("v +1 +2 +3", 1, 2, 3)]
        [TestCase("v -1 +2 -3", -1, 2, -3)]
        [TestCase("v 1.5 2.4 3.6", 1.5f, 2.4f, 3.6f)]
        [TestCase("v -1.5 -2.4 -3.6", -1.5f, -2.4f, -3.6f)]
        [TestCase("v 1.53 2.47 3.68", 1.53f, 2.47f, 3.68f)]
        public void ExtractVertexTest(string line, float x, float y, float z)
        {
            Vector3 actual = reader.ExtractElement(line);
            Vector3 expected = new Vector3(x, y, z);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ReadVertexTest_ValidVertex()
        {
            reader.Read("v 1.53 +2.47 -3.68");
            reader.Read("v -1.5 -2.4 -3.6");
            reader.Read("v -1.5 -2.4 -3.6");

            Vector3 expected1 = new Vector3(+1.53f, +2.47f, -3.68f);
            Vector3 expected2 = new Vector3(-1.50f, -2.40f, -3.60f);
            Vector3 expected3 = new Vector3(-1.50f, -2.40f, -3.60f);

            Assert.AreEqual(expected1, reader.elements[1]);
            Assert.AreEqual(expected2, reader.elements[2]);
            Assert.AreEqual(expected3, reader.elements[3]);
        }

        [Test]
        public void ReadVertexTest_InValidVertex()
        {
            Assert.Throws(typeof(InvalidObjFileException), new TestDelegate(ReadVertexTest_InValidVertex_Helper));
        }

        private void ReadVertexTest_InValidVertex_Helper()
        {
            string line = "v 1.53 +2.47";
            reader.Read(line);
        }

        [Test]
        public void ReadCubeVertices_Test()
        {
            reader.Read("v -1.000000 -1.000000 1.000000");
            reader.Read("v -1.000000 -1.000000 -1.000000");
            reader.Read("v 1.000000 -1.000000 -1.000000");
            reader.Read("v 1.000000 -1.000000 1.000000");
            reader.Read("v -1.000000 1.000000 0.999999");
            reader.Read("v -0.999999 1.000000 -1.000001");
            reader.Read("v 1.000000 1.000000 -1.000000");
            reader.Read("v 1.000000 1.000000 1.000000");

            Dictionary<int, Vector3> expected = new Dictionary<int, Vector3>();
            expected.Add(1, new Vector3(-1.000000f, -1.000000f, +1.000000f));
            expected.Add(2, new Vector3(-1.000000f, -1.000000f, -1.000000f));
            expected.Add(3, new Vector3(+1.000000f, -1.000000f, -1.000000f));
            expected.Add(4, new Vector3(+1.000000f, -1.000000f, +1.000000f));
            expected.Add(5, new Vector3(-1.000000f, +1.000000f, +0.999999f));
            expected.Add(6, new Vector3(-0.999999f, +1.000000f, -1.000001f));
            expected.Add(7, new Vector3(+1.000000f, +1.000000f, -1.000000f));
            expected.Add(8, new Vector3(+1.000000f, +1.000000f, +1.000000f));

            Dictionary<int, Vector3> actual = reader.elements;

            Assert.That(actual, Is.EquivalentTo(expected));
        }
    }

    [TestFixture]
    public class VertexNormalReaderTests
    {
        VertexNormalReader reader;

        [SetUp]
        public void SetUp()
        {
            reader = new VertexNormalReader();
        }

        [TestCase("# some comment", false)]
        [TestCase("v some vertex", false)]
        [TestCase("vn some normal", true)]
        [TestCase("vt some texture", false)]
        [TestCase("f some face", false)]
        [TestCase("p some point", false)]
        [TestCase("l some line", false)]
        [TestCase("curv2 some 2D curve", false)]
        [TestCase("surf some surface", false)]
        [TestCase("g some group", false)]
        [TestCase("s some smoothing group", false)]
        [TestCase("mg some merging group", false)]
        [TestCase("o some object name", false)]
        public void IsApplicableReaderTest(string line, bool expected)
        {
            bool actual = reader.IsApplicable(line);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ReadTest()
        {
            string line = "vn 1.53 +2.47 -3.68";
            reader.Read(line);

            Vector3 expected = new Vector3(1.53f, 2.47f, -3.68f);

            Vector3 actual = reader.elements[1];

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Read_CubeTest()
        {
            reader.Read("vn 0.0000 -1.0000 0.0000");
            reader.Read("vn 0.0000 1.0000 -0.0000");
            reader.Read("vn -1.0000 -0.0000 0.0000");
            reader.Read("vn 0.0000 -0.0000 -1.0000");
            reader.Read("vn 1.0000 -0.0000 0.0000");
            reader.Read("vn -0.0000 0.0000 1.0000");

            Dictionary<int, Vector3> expected = new Dictionary<int, Vector3>();
            expected.Add(1, new Vector3(+0.0000f, -1.0000f, +0.0000f));
            expected.Add(2, new Vector3(+0.0000f, +1.0000f, -0.0000f));
            expected.Add(3, new Vector3(-1.0000f, -0.0000f, +0.0000f));
            expected.Add(4, new Vector3(+0.0000f, -0.0000f, -1.0000f));
            expected.Add(5, new Vector3(+1.0000f, -0.0000f, +0.0000f));
            expected.Add(6, new Vector3(-0.0000f, +0.0000f, +1.0000f));

            Dictionary<int, Vector3> actual = reader.elements;

            Assert.That(actual, Is.EquivalentTo(expected));
        }
    }

    [TestFixture]
    public class FaceReaderTests
    {

        FaceReader reader;

        [SetUp]
        public void SetUp()
        {
            reader = new FaceReader();
        }

        [TestCase("# some comment", false)]
        [TestCase("v some vertex", false)]
        [TestCase("vn some normal", false)]
        [TestCase("f some face", true)]
        [TestCase("vt some texture", false)]
        [TestCase("p some point", false)]
        [TestCase("l some line", false)]
        [TestCase("curv2 some 2D curve", false)]
        [TestCase("surf some surface", false)]
        [TestCase("g some group", false)]
        [TestCase("s some smoothing group", false)]
        [TestCase("mg some merging group", false)]
        [TestCase("o some object name", false)]
        public void IsApplicableReaderTest(string line, bool expected)
        {
            bool actual = reader.IsApplicable(line);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Read_CubeTest()
        {
            reader.Read("f 2//1 4//1 1//1");
            reader.Read("f 8//2 6//2 5//2");
            reader.Read("f 5//3 2//3 1//3");
            reader.Read("f 6//4 3//4 2//4");
            reader.Read("f 3//5 8//5 4//5");
            reader.Read("f 1//6 8//6 5//6");
            reader.Read("f 2//1 3//1 4//1");
            reader.Read("f 8//2 7//2 6//2");
            reader.Read("f 5//3 6//3 2//3");
            reader.Read("f 6//4 7//4 3//4");
            reader.Read("f 3//5 7//5 8//5");
            reader.Read("f 1//6 4//6 8//6");

            List<FaceReader.Face> expected = new List<FaceReader.Face>();
            expected.Add(new FaceReader.Face(v0: 2, n0: 1, v1: 4, n1: 1, v2: 1, n2: 1));
            expected.Add(new FaceReader.Face(v0: 8, n0: 2, v1: 6, n1: 2, v2: 5, n2: 2));
            expected.Add(new FaceReader.Face(v0: 5, n0: 3, v1: 2, n1: 3, v2: 1, n2: 3));
            expected.Add(new FaceReader.Face(v0: 6, n0: 4, v1: 3, n1: 4, v2: 2, n2: 4));
            expected.Add(new FaceReader.Face(v0: 3, n0: 5, v1: 8, n1: 5, v2: 4, n2: 5));
            expected.Add(new FaceReader.Face(v0: 1, n0: 6, v1: 8, n1: 6, v2: 5, n2: 6));
            expected.Add(new FaceReader.Face(v0: 2, n0: 1, v1: 3, n1: 1, v2: 4, n2: 1));
            expected.Add(new FaceReader.Face(v0: 8, n0: 2, v1: 7, n1: 2, v2: 6, n2: 2));
            expected.Add(new FaceReader.Face(v0: 5, n0: 3, v1: 6, n1: 3, v2: 2, n2: 3));
            expected.Add(new FaceReader.Face(v0: 6, n0: 4, v1: 7, n1: 4, v2: 3, n2: 4));
            expected.Add(new FaceReader.Face(v0: 3, n0: 5, v1: 7, n1: 5, v2: 8, n2: 5));
            expected.Add(new FaceReader.Face(v0: 1, n0: 6, v1: 4, n1: 6, v2: 8, n2: 6));

            List<FaceReader.Face> actual = reader.faces;

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void ReadTest_NoNormals()
        {
            string line = "f 1 4 8";
            FaceReader.Face expected = new FaceReader.Face(1, 4, 8);
            FaceReader.Face actual = reader.ExtractFace(line);

            Assert.AreEqual(expected.vertexIndices, actual.vertexIndices);
            Assert.AreEqual(expected.normalIndices, actual.normalIndices);
        }

        [Test]
        public void ExtractCompleteFace()
        {
            string line = "f 1//2 4//3 8//5";
            FaceReader.Face expected = new FaceReader.Face(1, 4, 8, 2, 3, 5);
            FaceReader.Face actual = reader.ExtractCompleteFace(line);

            Assert.AreEqual(expected.vertexIndices, actual.vertexIndices);
            Assert.AreEqual(expected.normalIndices, actual.normalIndices);
        }

        [Test]
        public void ExtractNoNormalsFace()
        {
            string line = "f 1 4 8";
            FaceReader.Face expected = new FaceReader.Face(1, 4, 8);
            FaceReader.Face actual = reader.ExtractNoNormalFace(line);

            Assert.AreEqual(expected.vertexIndices, actual.vertexIndices);
            Assert.AreEqual(expected.normalIndices, actual.normalIndices);
        }

        [TestCase("f 1 4 8", false)]
        [TestCase("f 1//2 4//3 8//5", true)]
        public void HasNormals(string line, bool expected)
        {
            bool actual = reader.HasNormals(line);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ReadTest_WithNormals()
        {
            string line = "f 1//2 4//3 8//5";
            FaceReader.Face expected = new FaceReader.Face(1, 4, 8, 2, 3, 5);
            FaceReader.Face actual = reader.ExtractFace(line);

            Assert.AreEqual(expected.vertexIndices, actual.vertexIndices);
            Assert.AreEqual(expected.normalIndices, actual.normalIndices);
        }

        [Test]
        public void ReadTest_TwoVerticesNoNormals()
        {
            Assert.Throws(typeof(InvalidObjFileException), new TestDelegate(ReadTest_TwoVerticesNoNormals_Helper));
        }

        private void ReadTest_TwoVerticesNoNormals_Helper()
        {
            string line = "f 1 4";
            reader.Read(line);
        }

        [Test]
        public void ReadTest_TwoVertices_WithNormals()
        {
            Assert.Throws(typeof(InvalidObjFileException), new TestDelegate(ReadTest_TwoVertices_WithNormals_Helper));
        }

        private void ReadTest_TwoVertices_WithNormals_Helper()
        {
            string line = "f 1//2 4//3";
            reader.Read(line);
        }

        [Test]
        public void ReadTest_FourVerticesNoNormals()
        {
            Assert.Throws(typeof(InvalidObjFileException), new TestDelegate(ReadTest_FourVerticesNoNormals_Helper));
        }

        private void ReadTest_FourVerticesNoNormals_Helper()
        {
            string line = "f 1 4 8 7";
            reader.Read(line);
        }

        [Test]
        public void ReadTest_FourVertices_WithNormals()
        {
            Assert.Throws(typeof(InvalidObjFileException), new TestDelegate(ReadTest_FourVertices_WithNormals_Helper));
        }

        private void ReadTest_FourVertices_WithNormals_Helper()
        {
            string line = "f 1//2 4//3 1//2 4//3";
            reader.Read(line);
        }
    }

    [TestFixture]
    public class VertexTextureReaderTests
    {

        VertexTextureReader reader;

        [SetUp]
        public void SetUp()
        {
            reader = new VertexTextureReader();
        }

        [TestCase("# some comment", false)]
        [TestCase("v some vertex", false)]
        [TestCase("vn some normal", false)]
        [TestCase("f some face", false)]
        [TestCase("vt some texture", true)]
        [TestCase("p some point", false)]
        [TestCase("l some line", false)]
        [TestCase("curv2 some 2D curve", false)]
        [TestCase("surf some surface", false)]
        [TestCase("g some group", false)]
        [TestCase("s some smoothing group", false)]
        [TestCase("mg some merging group", false)]
        [TestCase("o some object name", false)]
        public void IsApplicableReaderTest(string line, bool expected)
        {
            bool actual = reader.IsApplicable(line);
            Assert.AreEqual(expected, actual);
        }
    }

    [TestFixture]
    public class GroupReaderTests
    {

        GroupReader reader;

        [SetUp]
        public void SetUp()
        {
            reader = new GroupReader();
        }

        [TestCase("# some comment", false)]
        [TestCase("v some vertex", false)]
        [TestCase("vn some normal", false)]
        [TestCase("f some face", false)]
        [TestCase("vt some texture", false)]
        [TestCase("p some point", false)]
        [TestCase("l some line", false)]
        [TestCase("curv2 some 2D curve", false)]
        [TestCase("surf some surface", false)]
        [TestCase("g some group", true)]
        [TestCase("s some smoothing group", false)]
        [TestCase("mg some merging group", false)]
        [TestCase("o some object name", false)]
        public void IsApplicableReaderTest(string line, bool expected)
        {
            bool actual = reader.IsApplicable(line);
            Assert.AreEqual(expected, actual);
        }
    }

    [TestFixture]
    public class SmoothingGroupReaderTests
    {

        SmoothingGroupReader reader;

        [SetUp]
        public void SetUp()
        {
            reader = new SmoothingGroupReader();
        }

        [TestCase("# some comment", false)]
        [TestCase("v some vertex", false)]
        [TestCase("vn some normal", false)]
        [TestCase("f some face", false)]
        [TestCase("vt some texture", false)]
        [TestCase("p some point", false)]
        [TestCase("l some line", false)]
        [TestCase("curv2 some 2D curve", false)]
        [TestCase("surf some surface", false)]
        [TestCase("g some group", false)]
        [TestCase("s some smoothing group", true)]
        [TestCase("mg some merging group", false)]
        [TestCase("o some object name", false)]
        public void IsApplicableReaderTest(string line, bool expected)
        {
            bool actual = reader.IsApplicable(line);
            Assert.AreEqual(expected, actual);
        }
    }

    [TestFixture]
    public class ObjectReaderTests
    {

        ObjectReader reader;

        [SetUp]
        public void SetUp()
        {
            reader = new ObjectReader();
        }

        [TestCase("# some comment", false)]
        [TestCase("v some vertex", false)]
        [TestCase("vn some normal", false)]
        [TestCase("f some face", false)]
        [TestCase("vt some texture", false)]
        [TestCase("p some point", false)]
        [TestCase("l some line", false)]
        [TestCase("curv2 some 2D curve", false)]
        [TestCase("surf some surface", false)]
        [TestCase("g some group", false)]
        [TestCase("s some smoothing group", false)]
        [TestCase("mg some merging group", false)]
        [TestCase("o some object name", true)]
        public void IsApplicableReaderTest(string line, bool expected)
        {
            bool actual = reader.IsApplicable(line);
            Assert.AreEqual(expected, actual);
        }
    }

    [TestFixture]
    public class FaceReaderFaceTests
    {
        [TestCase(1, 2, 3, true, 1, 2, 3)]
        [TestCase(2, 2, 3, false, 1, 2, 3)]
        [TestCase(1, 3, 3, false, 1, 2, 3)]
        [TestCase(1, 2, 4, false, 1, 2, 3)]
        [TestCase(1, 2, 3, false, 4, 2, 3)]
        [TestCase(1, 2, 3, false, 1, 4, 3)]
        [TestCase(1, 2, 3, false, 1, 2, 4)]
        public void EqualsNoNormals(int this_v0, int this_v1, int this_v2, bool expected, int other_v0, int other_v1, int other_v2)
        {
            FaceReader.Face thisFace = new FaceReader.Face(this_v0, this_v1, this_v2);
            FaceReader.Face otherFace = new FaceReader.Face(other_v0, other_v1, other_v2);

            bool actual = thisFace.Equals(otherFace);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, thisFace.GetHashCode().Equals(otherFace.GetHashCode()));
        }

        [TestCase(1, 2, 3, 4, 5, 6, true, 1, 2, 3, 4, 5, 6)]
        [TestCase(9, 2, 3, 4, 5, 6, false, 1, 2, 3, 4, 5, 6)]
        [TestCase(1, 9, 3, 4, 5, 6, false, 1, 2, 3, 4, 5, 6)]
        [TestCase(1, 2, 9, 4, 5, 6, false, 1, 2, 3, 4, 5, 6)]
        [TestCase(1, 2, 3, 9, 5, 6, false, 1, 2, 3, 4, 5, 6)]
        [TestCase(1, 2, 3, 4, 9, 6, false, 1, 2, 3, 4, 5, 6)]
        [TestCase(1, 2, 3, 4, 5, 9, false, 1, 2, 3, 4, 5, 6)]
        [TestCase(1, 2, 3, 4, 5, 6, false, 9, 2, 3, 4, 5, 6)]
        [TestCase(1, 2, 3, 4, 5, 6, false, 1, 9, 3, 4, 5, 6)]
        [TestCase(1, 2, 3, 4, 5, 6, false, 1, 2, 9, 4, 5, 6)]
        [TestCase(1, 2, 3, 4, 5, 6, false, 1, 2, 3, 9, 5, 6)]
        [TestCase(1, 2, 3, 4, 5, 6, false, 1, 2, 3, 4, 9, 6)]
        [TestCase(1, 2, 3, 4, 5, 6, false, 1, 2, 3, 4, 5, 9)]
        public void EqualsWithNormals(
            int this_v0, int this_v1, int this_v2,
            int this_n0, int this_n1, int this_n2,
            bool expected,
            int other_v0, int other_v1, int other_v2,
            int other_n0, int other_n1, int other_n2
        )
        {
            FaceReader.Face thisFace = new FaceReader.Face(
                v0: this_v0, n0: this_n0,
                v1: this_v1, n1: this_n1,
                v2: this_v2, n2: this_n2
            );
            FaceReader.Face otherFace = new FaceReader.Face(
                v0: other_v0, n0: other_n0,
                v1: other_v1, n1: other_n1,
                v2: other_v2, n2: other_n2
            );

            bool actual = thisFace.Equals(otherFace);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, thisFace.GetHashCode().Equals(otherFace.GetHashCode()));
        }
    }

    [TestFixture]
    public class MeshBuilderTests
    {
        [Test]
        public void Build_Triangle()
        {
            Vector3 a = new Vector3(1, 1, 1);
            Vector3 b = new Vector3(2, 2, 2);
            Vector3 c = new Vector3(3, 3, 3);

            Vector3 n = Vector3.forward;

            Dictionary<int, Vector3> vertices = new Dictionary<int, Vector3>();
            vertices.Add(1, a);
            vertices.Add(2, b);
            vertices.Add(3, c);

            Dictionary<int, Vector3> normals = new Dictionary<int, Vector3>();
            normals.Add(1, n);

            List<FaceReader.Face> faces = new List<FaceReader.Face>();
            faces.Add(new FaceReader.Face(v0: 1, v1: 2, v2: 3, n0: 1, n1: 1, n2: 1));

            Mesh expected = new Mesh();
            expected.vertices = new Vector3[] { a, b, c };
            expected.normals = new Vector3[] { n, n, n };
            expected.triangles = new int[] { 0, 1, 2 };

            Mesh actual = new MeshBuilder(vertices, normals, faces).Build();

            Assert.AreEqual(expected.vertices, actual.vertices);
            Assert.AreEqual(expected.normals, actual.normals);
            Assert.AreEqual(expected.triangles, actual.triangles);
        }

        [Test]
        public void Build_Rectangle()
        {
            Vector3 a = new Vector3(1, 1, 1);
            Vector3 b = new Vector3(2, 2, 2);
            Vector3 c = new Vector3(3, 3, 3);
            Vector3 d = new Vector3(4, 4, 4);

            Vector3 n1 = Vector3.forward;
            Vector3 n2 = Vector3.up;

            Dictionary<int, Vector3> vertices = new Dictionary<int, Vector3>();
            vertices.Add(1, a);
            vertices.Add(2, b);
            vertices.Add(3, c);
            vertices.Add(4, d);

            Dictionary<int, Vector3> normals = new Dictionary<int, Vector3>();
            normals.Add(1, n1);
            normals.Add(2, n2);

            List<FaceReader.Face> faces = new List<FaceReader.Face>();
            faces.Add(new FaceReader.Face(v0: 1, v1: 2, v2: 3, n0: 1, n1: 1, n2: 1));
            faces.Add(new FaceReader.Face(v0: 2, v1: 4, v2: 3, n0: 2, n1: 2, n2: 2));

            Mesh expected = new Mesh();
            expected.vertices = new Vector3[] { a, b, c, b, d, c };
            expected.normals = new Vector3[] { n1, n1, n1, n2, n2, n2 };
            expected.triangles = new int[] { 0, 1, 2, 3, 4, 5 };

            Mesh actual = new MeshBuilder(vertices, normals, faces).Build();

            Assert.AreEqual(expected.vertices, actual.vertices);
            Assert.AreEqual(expected.normals, actual.normals);
            Assert.AreEqual(expected.triangles, actual.triangles);
        }
    }
}