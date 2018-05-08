using System.IO;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using IO;
using System.Runtime.CompilerServices;
using System;

namespace Tests.IO
{
	[TestFixture]
	public class ObjFileWriterTests
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

		[TearDown]
		public void TearDown()
		{
			UnityEditor.FileUtil.DeleteFileOrDirectory(this.outputPath);
		}

		[Test]
		public void Write_NoNormalsNoTextures()
		{
			Mesh mesh = GenerateNoNormalNoTextureMesh();

			ObjFile.Write(mesh, this.outputPath);

			ReadResult readResult = ObjFile.Read(this.outputPath);

			Assert.AreEqual(mesh.vertices, readResult.Mesh.vertices);
			Assert.AreEqual(mesh.normals, readResult.Mesh.normals);
			Assert.AreEqual(mesh.triangles, readResult.Mesh.triangles);
			Assert.AreEqual(mesh.uv2, readResult.Mesh.uv2);
			Assert.AreEqual(mesh.uv3, readResult.Mesh.uv3);
		}

		private Mesh GenerateNoNormalNoTextureMesh()
		{
			Vector3 a = new Vector3(1, 1, 1);
			Vector3 b = new Vector3(2, 2, 2);
			Vector3 c = new Vector3(3, 3, 3);

			Mesh expected = new Mesh();
			expected.vertices = new Vector3[] { a, b, c };
			expected.triangles = new int[] { 0, 1, 2 };

			return expected;
		}

		[Test]
		public void Write_NormalsNoTextures()
		{
			Mesh mesh = GenerateNormalsNoTextureMesh();

			ObjFile.Write(mesh, this.outputPath);

			ReadResult readResult = ObjFile.Read(this.outputPath);

			Assert.AreEqual(mesh.vertices, readResult.Mesh.vertices);
			Assert.AreEqual(mesh.normals, readResult.Mesh.normals);
			Assert.AreEqual(mesh.triangles, readResult.Mesh.triangles);
			Assert.AreEqual(mesh.uv2, readResult.Mesh.uv2);
			Assert.AreEqual(mesh.uv3, readResult.Mesh.uv3);
		}

		private Mesh GenerateNormalsNoTextureMesh()
		{
			Vector3 v_a = new Vector3(1, 1, 1);
			Vector3 v_b = new Vector3(2, 2, 2);
			Vector3 v_c = new Vector3(3, 3, 3);

			Vector3 n = Vector3.forward;

			Mesh mesh = new Mesh();
			mesh.vertices = new Vector3[] { v_a, v_b, v_c };
			mesh.normals = new Vector3[] { n, n, n };
			mesh.triangles = new int[] { 0, 1, 2 };


			return mesh;
		}

		[Test]
		public void Write_NormalsTextures()
		{
			Mesh expected = GenerateNormalsTextureMesh();

			WriteResult writeResult = ObjFile.Write(expected, this.outputPath);

			ReadResult readResult = ObjFile.Read(this.outputPath);

			Assert.AreEqual(expected.vertices, readResult.Mesh.vertices);
			Assert.AreEqual(expected.normals, readResult.Mesh.normals);
			Assert.AreEqual(expected.triangles, readResult.Mesh.triangles);
			Assert.AreEqual(expected.uv2, readResult.Mesh.uv2);
			Assert.AreEqual(expected.uv3, readResult.Mesh.uv3);
		}

		private Mesh GenerateNormalsTextureMesh()
		{
			Vector3 v_a = new Vector3(1, 1, 1);
			Vector3 v_b = new Vector3(2, 2, 2);
			Vector3 v_c = new Vector3(3, 3, 3);

			Vector3 n = Vector3.forward;

			Vector3 t_a = new Vector3(0, 1, 2);
			Vector3 t_b = new Vector3(2, 3, 4);
			Vector3 t_c = new Vector3(4, 5, 6);

			Mesh mesh = new Mesh();
			mesh.vertices = new Vector3[] { v_a, v_b, v_c };
			mesh.normals = new Vector3[] { n, n, n };
			mesh.triangles = new int[] { 0, 1, 2 };
			mesh.uv2 = new Vector2[]{
				new Vector2(t_a.x, t_a.y),
				new Vector2(t_b.x, t_b.y),
				new Vector2(t_c.x, t_c.y)
			};
			mesh.uv3 = new Vector2[]{
				new Vector2(t_a.z, 0),
				new Vector2(t_b.z, 0),
				new Vector2(t_c.z, 0),
			};

			return mesh;
		}
	}
}