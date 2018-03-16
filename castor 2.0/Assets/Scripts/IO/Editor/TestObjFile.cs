using NUnit.Framework;
using System.IO;
using UnityEngine;
using IO;

namespace Tests.IO
{
    [TestFixture]
    public class ObjFileTests
    {
        string testObjFile = "Scripts/IO/Editor/cube.obj";
        string inputPath;
        string outputPath;

        [SetUp]
        public void SetUp()
        {
            this.inputPath = Path.Combine(Application.dataPath, testObjFile);
            this.outputPath = UnityEditor.FileUtil.GetUniqueTempPathInProject();
        }

        [TearDown]
        public void TearDown()
        {
            UnityEditor.FileUtil.DeleteFileOrDirectory(this.outputPath);
        }

        [Test]
        public void Test_ReadWrite()
        {
            ReadResult readResult = ObjFile.Read(this.inputPath);
            Mesh mesh = readResult.Mesh;

            WriteResult writeResult = ObjFile.Write(mesh, this.outputPath);

            FileAssert.AreEqual(this.inputPath, this.outputPath);
        }
    }
}