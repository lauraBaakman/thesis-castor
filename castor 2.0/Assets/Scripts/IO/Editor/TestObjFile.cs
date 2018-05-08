using NUnit.Framework;
using System.IO;
using UnityEngine;
using IO;

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
			return Path.GetTempFileName();
		}

		private string InputPath(string file)
		{
			return Path.Combine(Application.dataPath, Path.Combine("Scripts/IO/Editor", file));
		}

		[TearDown]
		public void TearDown()
		{
			UnityEditor.FileUtil.DeleteFileOrDirectory(this.outputPath);
		}

		[TestCase("cube.obj")]
		[TestCase("balk.obj")]
		[TestCase("cube_vt.obj")]
		public void Test_ReadWrite(string path)
		{
			string inputPath = InputPath(path);
			ReadResult expected = ObjFile.Read(inputPath);
			Assert.IsTrue(expected.Succeeded);

			Mesh mesh = expected.Mesh;

			WriteResult writeResult = ObjFile.Write(mesh, this.outputPath);
			Assert.IsTrue(writeResult.Succeeded);

			ReadResult actual = ObjFile.Read(this.outputPath);
			Assert.IsTrue(actual.Succeeded);

			Assert.AreEqual(expected.Mesh.vertices, actual.Mesh.vertices);
			Assert.AreEqual(expected.Mesh.normals, actual.Mesh.normals);
			Assert.AreEqual(expected.Mesh.triangles, actual.Mesh.triangles);
			Assert.AreEqual(expected.Mesh.uv2, actual.Mesh.uv2);
			Assert.AreEqual(expected.Mesh.uv3, actual.Mesh.uv3);
		}
	}
}