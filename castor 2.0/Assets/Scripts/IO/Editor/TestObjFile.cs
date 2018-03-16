using NUnit.Framework;
using System.IO;
using UnityEngine;
using IO;

namespace Tests.IO
{
    [TestFixture]
    public class ObjFileTests
    {
        string cube = "Scripts/IO/Editor/cube.obj";
        string balk = "Scripts/IO/Editor/balk.obj";

        string balkPath;
        string outputPath;

        [SetUp]
        public void SetUp()
        {
            this.outputPath = UnityEditor.FileUtil.GetUniqueTempPathInProject();
        }

        private string InputPath(string file)
        {
            return Path.Combine(Application.dataPath, file);
        }

        [TearDown]
        public void TearDown()
        {
            UnityEditor.FileUtil.DeleteFileOrDirectory(this.outputPath);
        }

        [Test]
        public void Test_ReadWrite_Cube()
        {
            string inputPath = InputPath(cube);
            ReadResult readResult = ObjFile.Read(inputPath);
            Mesh mesh = readResult.Mesh;

            WriteResult writeResult = ObjFile.Write(mesh, this.outputPath);

            FileAssert.AreEqual(inputPath, this.outputPath);
        }

        [Test]
        public void Test_ReadWrite_Balk()
        {
            string inputPath = InputPath(balk);
            ReadResult readResult = ObjFile.Read(inputPath);
            Mesh mesh = readResult.Mesh;

            WriteResult writeResult = ObjFile.Write(mesh, this.outputPath);

            FileAssert.AreEqual(inputPath, this.outputPath);
        }
    }
}