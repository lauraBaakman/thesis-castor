using NUnit.Framework;
using System.IO;
using UnityEngine;
using IO;
using System.Runtime.CompilerServices;

namespace Tests.IO
{
    [TestFixture]
    public class ObjFileTests
    {
        string outputPath;

        [SetUp]
        public void SetUp()
        {
            this.outputPath = TempPath();
        }

        private string TempPath()
        {
            return Path.Combine(Application.dataPath, UnityEditor.FileUtil.GetUniqueTempPathInProject());
        }

        private string InputPath(string file)
        {
            return Path.Combine(Application.dataPath, Path.Combine("Scripts/IO/Editor", file));
        }

        [TearDown]
        public void TearDown()
        {
            //UnityEditor.FileUtil.DeleteFileOrDirectory(this.outputPath);
        }


        [TestCase("cube.obj")]
        [TestCase("balk.obj")]
        public void Test_ReadWrite(string path)
        {
            string inputPath = InputPath(path);
            ReadResult expected = ObjFile.Read(inputPath);
            Assert.IsTrue(expected.Succeeded());

            Mesh mesh = expected.Mesh;

            WriteResult writeResult = ObjFile.Write(mesh, this.outputPath);
            Assert.IsTrue(writeResult.Succeeded());

            ReadResult actual = ObjFile.Read(this.outputPath);
            Assert.IsTrue(actual.Succeeded());

            Assert.IsTrue(actual.Mesh.Equals(expected.Mesh));
        }
    }
}