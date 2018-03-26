using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace IO
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
                2, 4, 1,
                8, 6, 5,
                5, 2, 1,
                6, 3, 2,
                3, 8, 4,
                1, 8, 5,
                2, 3, 4,
                8, 7, 6,
                5, 6, 2,
                6, 7, 3,
                3, 7, 8,
                1, 4, 8,
            };
            mesh.triangles = triangles;

            ReadResult expected = ReadResult.OKResult(inputPath, mesh);
            ReadResult actual = new ObjFileReader(inputPath).ImportFile();

            Assert.IsTrue(actual.Mesh.MeshEquals(expected.Mesh));
            Assert.IsTrue(actual.Succeeded());
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

        [Test]
        public void Trim_NoWhiteSpace()
        {
            string input = "hoi";
            string expected = "hoi";
            string actual = new ObjFileReader("").Trim(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Trim_MiddleWhiteSpace()
        {
            string input = "hoi hallo\tdoeg";
            string expected = "hoi hallo\tdoeg";
            string actual = new ObjFileReader("").Trim(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Trim_OnlyStartWhiteSpace()
        {
            string input = " hoi hallo\tdoeg";
            string expected = "hoi hallo\tdoeg";
            string actual = new ObjFileReader("").Trim(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Trim_OnlyEndWhiteSpace()
        {
            string input = "hoi hallo\tdoeg   ";
            string expected = "hoi hallo\tdoeg";
            string actual = new ObjFileReader("").Trim(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Trim_StartAndEndWhiteSpace()
        {
            string input = "\thoi hallo\tdoeg   ";
            string expected = "hoi hallo\tdoeg";
            string actual = new ObjFileReader("").Trim(input);

            Assert.AreEqual(expected, actual);
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
        public void IsApplicableReader(string line, bool expected)
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
        public void IsApplicableReader(string line, bool expected)
        {
            bool actual = reader.IsApplicable(line);
            Assert.AreEqual(expected, actual);
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
        public void IsApplicableReader(string line, bool expected)
        {
            bool actual = reader.IsApplicable(line);
            Assert.AreEqual(expected, actual);
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
        public void IsApplicableReader(string line, bool expected)
        {
            bool actual = reader.IsApplicable(line);
            Assert.AreEqual(expected, actual);
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
        public void IsApplicableReader(string line, bool expected)
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
        public void IsApplicableReader(string line, bool expected)
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
        public void IsApplicableReader(string line, bool expected)
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
        public void IsApplicableReader(string line, bool expected)
        {
            bool actual = reader.IsApplicable(line);
            Assert.AreEqual(expected, actual);
        }
    }
}