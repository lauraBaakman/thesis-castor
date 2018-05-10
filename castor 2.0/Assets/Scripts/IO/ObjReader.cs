using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace IO
{
	public class ObjReader
	{
		private readonly string filePath;

		private readonly Dictionary<string, Reader> readers;

		public ObjReader(string filePath)
		{
			this.filePath = filePath;

			this.readers = new Dictionary<string, Reader>();
			this.readers.Add("comment", new CommentReader());
			this.readers.Add("vertex", new VertexReader());
			this.readers.Add("normal", new VertexNormalReader());
			this.readers.Add("face", new FaceReader());
			this.readers.Add("texture", new VertexTextureReader());
			this.readers.Add("group", new GroupReader());
			this.readers.Add("smoothing group", new SmoothingGroupReader());
			this.readers.Add("object", new ObjectReader());
			this.readers.Add("material name", new MaterialNameReader());
			this.readers.Add("material library", new MaterialLibraryReader());
		}

		public ReadResult ImportFile()
		{
			ReadResult result = null;
			try
			{
				string[] lines = ReadLines();
				ProcessLines(lines);

				Mesh mesh = BuildMesh();

				result = ReadResult.OKResult(filePath, mesh);
			}
			catch (Exception exception)
			{
				result = ReadResult.ErrorResult(filePath, exception);
			}
			return result;
		}

		private void ProcessLines(string[] lines)
		{
			foreach (string line in lines) ProcessLine(line);
		}

		private string[] ReadLines()
		{
			string[] lines;
			try
			{
				lines = System.IO.File.ReadAllLines(filePath);
			}
			catch (Exception exception) { throw exception; }
			return lines;
		}

		private void ProcessLine(string line)
		{
			try
			{
				string trimmedLine = line.Trim();
				if (trimmedLine.Equals("")) return;

				foreach (Reader reader in this.readers.Values)
				{
					if (reader.IsApplicable(trimmedLine))
					{
						reader.Read(trimmedLine);
						return;
					}
				}
				Debug.LogWarning("Encountered and ignored an unrecognised line: " + trimmedLine);
			}
			catch (InvalidObjFileException exception)
			{
				throw new InvalidObjFileException(
					string.Format("Could not read the file {0}: {1}" + this.filePath, exception.Message));
			}
			catch (Exception exception)
			{
				throw new InvalidObjFileException(
					string.Format("Could not read the file {0}, got the error: {1}", this.filePath, exception.Message));
			}
		}

		private Mesh BuildMesh()
		{
			VertexReader vertexReader = readers["vertex"] as VertexReader;
			VertexNormalReader normalReader = readers["normal"] as VertexNormalReader;
			FaceReader faceReader = readers["face"] as FaceReader;
			VertexTextureReader vertexTextureReader = readers["texture"] as VertexTextureReader;

			MeshBuilder builder = new MeshBuilder(
				vertices: vertexReader.elements,
				normals: normalReader.elements,
				textures: vertexTextureReader.elements,
				faces: faceReader.faces);
			Mesh mesh = builder.Build();

			return mesh;
		}
	}

	public abstract class Reader
	{
		protected Regex typeRegex;

		protected Reader(string lineTypeSymbol)
		{
			typeRegex = new Regex(@"^" + lineTypeSymbol + @"\s+");
		}

		public bool IsApplicable(string line)
		{
			return typeRegex.IsMatch(line);
		}

		public abstract void Read(string line);
	}

	public abstract class ReferenceReader<T> : Reader
	{
		private int currentReferenceNumber = 1;

		public Dictionary<int, T> elements;

		protected ReferenceReader(string lineTypeSymbol)
			: base(lineTypeSymbol)
		{
			elements = new Dictionary<int, T>();
		}

		public override void Read(string line)
		{
			try
			{
				T vertex = ExtractElement(line);
				elements.Add(currentReferenceNumber++, vertex);
			}
			catch (Exception e) { throw e; }
		}

		public abstract T ExtractElement(string line);
	}

	public abstract class VectorReader : ReferenceReader<Vector3>
	{
		protected VectorReader(string lineTypeSymbol)
			: base(lineTypeSymbol)
		{ }

		public override Vector3 ExtractElement(string line)
		{
			Vector3 vertex;
			try
			{
				string[] values = line.Split(' ');
				//Values[0] contains vn/v/vt
				vertex = new Vector3(
					x: float.Parse(values[1]),
					y: float.Parse(values[2]),
					z: float.Parse(values[3])
				);
			}
			catch (Exception e)
			{
				throw new InvalidObjFileException(
					string.Format("Could not read the line {0}, got the execption: {1}", line, e.Message)
				);
			}
			return vertex;
		}
	}

	public class CommentReader : Reader
	{
		public CommentReader()
			: base("#")
		{ }

		public override void Read(string line) { }
	}

	public class VertexReader : VectorReader
	{
		public VertexReader()
			: base("v")
		{ }
	}

	public class VertexNormalReader : VectorReader
	{
		public VertexNormalReader()
			: base("vn")
		{ }
	}

	public class FaceReader : Reader
	{
		private Regex noTexturesNoNormalsRegex;
		private Regex noTexturesNormalsRegex;
		private Regex TexturesNormalsRegex;

		public List<Face> faces;

		public FaceReader()
			: base("f")
		{
			faces = new List<Face>();

			noTexturesNoNormalsRegex = new Regex(typeRegex + @"(?<v0>\d+)\s+(?<v1>\d+)\s+(?<v2>\d+)$");

			Regex vertex = new Regex(@"(\d+)//(\d+)");
			noTexturesNormalsRegex = new Regex(typeRegex.ToString() + vertex + @"\s+" + vertex + @"\s+" + vertex + @"$");

			vertex = new Regex(@"(\d+)/(\d+)/(\d+)");
			TexturesNormalsRegex = new Regex(typeRegex.ToString() + vertex + @"\s+" + vertex + @"\s+" + vertex + @"$");
		}

		public override void Read(string line)
		{
			Face face = ExtractFace(line);
			faces.Add(face);
		}

		public Face ExtractFace(string line)
		{
			if (HasNormalsHasTextures(line)) return ExtractNormalsTexturesFace(line);
			if (HasNoTexturesHasNormals(line)) return ExtractNormalsNoTexturesFace(line);
			if (HasNoTexturesHasNoNormals(line)) return ExtractNoNormalsNoTexturesFace(line);

			throw new InvalidObjFileException(
				string.Format(
					"The line: '{0}' does not define a valid face.", line
				)
			);
		}

		public bool HasNormalsHasTextures(string line)
		{
			return TexturesNormalsRegex.Match(line).Success;
		}

		public bool HasNoTexturesHasNormals(string line)
		{
			return noTexturesNormalsRegex.Match(line).Success;
		}

		public bool HasNoTexturesHasNoNormals(string line)
		{
			return noTexturesNoNormalsRegex.Match(line).Success;
		}

		private Face ExtractNoNormalsNoTexturesFace(string line)
		{
			Face face;
			try
			{
				MatchCollection matches = noTexturesNoNormalsRegex.Matches(line);
				GroupCollection groups = matches[0].Groups;

				face = new Face(
					v0: Int32.Parse(groups["v0"].Value),
					v1: Int32.Parse(groups["v1"].Value),
					v2: Int32.Parse(groups["v2"].Value)
				);
			}
			catch (Exception e)
			{
				throw new InvalidObjFileException(
					string.Format("Could not read the face: '{0}', got the execption: {1}", line, e.Message)
				);
			}
			return face;
		}

		private Face ExtractNormalsNoTexturesFace(string line)
		{
			Face face;
			try
			{
				MatchCollection matches = noTexturesNormalsRegex.Matches(line);
				GroupCollection groups = matches[0].Groups;

				face = new Face(
					v0: Int32.Parse(groups[1].Value),
					n0: Int32.Parse(groups[2].Value),
					v1: Int32.Parse(groups[3].Value),
					n1: Int32.Parse(groups[4].Value),
					v2: Int32.Parse(groups[5].Value),
					n2: Int32.Parse(groups[6].Value));
			}
			catch (Exception e)
			{
				throw new InvalidObjFileException(
					string.Format("Could not read the face: '{0}', got the exception: {1}", line, e.Message)
				);
			}
			return face;
		}

		private Face ExtractNormalsTexturesFace(string line)
		{
			Face face;
			try
			{
				MatchCollection matches = TexturesNormalsRegex.Matches(line);
				GroupCollection groups = matches[0].Groups;

				face = new Face(
					//Vertex 0
					v0: Int32.Parse(groups[1].Value),
					t0: Int32.Parse(groups[2].Value),
					n0: Int32.Parse(groups[3].Value),
					//Vertex 1
					v1: Int32.Parse(groups[4].Value),
					t1: Int32.Parse(groups[5].Value),
					n1: Int32.Parse(groups[6].Value),
					//Vertex 2
					v2: Int32.Parse(groups[7].Value),
					t2: Int32.Parse(groups[8].Value),
					n2: Int32.Parse(groups[9].Value)
				);
			}
			catch (Exception e)
			{
				throw new InvalidObjFileException(
					string.Format("Could not read the face: '{0}', got the exception: {1}", line, e.Message)
				);
			}
			return face;
		}

		public class Face : IEquatable<Face>
		{
			public readonly int[] vertexIndices = null;
			public readonly int[] normalIndices = null;
			public readonly int[] textureIndices = null;

			public int v0 { get { return vertexIndices[0]; } }
			public int v1 { get { return vertexIndices[1]; } }
			public int v2 { get { return vertexIndices[2]; } }

			public int n0 { get { return normalIndices[0]; } }
			public int n1 { get { return normalIndices[1]; } }
			public int n2 { get { return normalIndices[2]; } }

			public int t0 { get { return textureIndices[0]; } }
			public int t1 { get { return textureIndices[1]; } }
			public int t2 { get { return textureIndices[2]; } }

			public Face(int v0, int v1, int v2)
			{
				vertexIndices = new int[3];
				vertexIndices[0] = v0;
				vertexIndices[1] = v1;
				vertexIndices[2] = v2;
			}

			public Face(
				int v0, int v1, int v2,
				int n0, int n1, int n2
			) : this(v0, v1, v2)
			{
				normalIndices = new int[3];
				normalIndices[0] = n0;
				normalIndices[1] = n1;
				normalIndices[2] = n2;
			}

			public Face(
				int v0, int v1, int v2,
				int n0, int n1, int n2,
				int t0, int t1, int t2
			) : this(v0, v1, v2, n0, n1, n2)
			{
				textureIndices = new int[3];
				textureIndices[0] = t0;
				textureIndices[1] = t1;
				textureIndices[2] = t2;
			}

			public bool HasNormalIndices()
			{
				return this.normalIndices != null;
			}

			public bool HasTextureIndices()
			{
				return this.textureIndices != null;
			}

			public override bool Equals(object obj)
			{
				if (obj == null || GetType() != obj.GetType())
					return false;
				return this.Equals(obj as Face);
			}

			public override int GetHashCode()
			{
				int hashCode = 67;

				if (HasNormalIndices())
				{
					hashCode *= (31 + this.n0.GetHashCode());
					hashCode *= (31 + this.n1.GetHashCode());
					hashCode *= (31 + this.n2.GetHashCode());
				}
				if (HasTextureIndices())
				{
					hashCode *= (31 + this.t0.GetHashCode());
					hashCode *= (31 + this.t1.GetHashCode());
					hashCode *= (31 + this.t2.GetHashCode());
				}
				hashCode *= (31 + this.v0.GetHashCode());
				hashCode *= (31 + this.v1.GetHashCode());
				hashCode *= (31 + this.v2.GetHashCode());
				return hashCode;
			}

			public bool Equals(Face other)
			{
				bool verticesEqual = (
					this.v0.Equals(other.v0) &
					this.v1.Equals(other.v1) &
					this.v2.Equals(other.v2)
				);
				bool normalsEqual = !HasNormalIndices() || (
					this.n0.Equals(other.n0) &
					this.n1.Equals(other.n1) &
					this.n2.Equals(other.n2)
				);
				bool texturesEqual = !HasTextureIndices() || (
					this.t0.Equals(other.t0) &
					this.t1.Equals(other.t1) &
					this.t2.Equals(other.t2)
				);
				return verticesEqual && normalsEqual && texturesEqual;
			}
		}
	}

	public class VertexTextureReader : VectorReader
	{
		public VertexTextureReader()
			: base("vt")
		{ }
	}

	public class GroupReader : Reader
	{
		public GroupReader()
			: base("g")
		{ }

		public override void Read(string line) { }
	}

	public class SmoothingGroupReader : Reader
	{
		public SmoothingGroupReader()
			: base("s")
		{ }

		public override void Read(string line) { }
	}

	public class ObjectReader : Reader
	{
		public ObjectReader()
			: base("o")
		{ }

		public override void Read(string line) { }
	}

	public class MaterialNameReader : Reader
	{
		public MaterialNameReader()
			: base("usemtl")
		{ }

		public override void Read(string line) { }
	}

	public class MaterialLibraryReader : Reader
	{
		public MaterialLibraryReader()
			: base("mtllib")
		{ }

		public override void Read(string line) { }
	}

	public class MeshBuilder
	{
		private readonly Dictionary<int, Vector3> objVertices;
		private readonly Dictionary<int, Vector3> objNormals;
		private readonly Dictionary<int, Vector3> objTextures;
		private readonly List<FaceReader.Face> objFaces;

		private readonly List<Vector3> meshVertices;
		private readonly List<Vector3> meshNormals;
		private readonly List<Vector2> meshUV2;
		private readonly List<Vector2> meshUV3;

		private readonly int[] meshTriangles;

		private int idx = 0;

		private MeshBuilder(Dictionary<int, Vector3> vertices, List<FaceReader.Face> faces)
		{
			this.objVertices = vertices;
			this.objFaces = faces;

			meshVertices = new List<Vector3>();
			meshTriangles = new int[faces.Count * 3];
		}

		private MeshBuilder(Dictionary<int, Vector3> vertices, Dictionary<int, Vector3> normals, List<FaceReader.Face> faces)
			: this(vertices, faces)
		{
			this.objNormals = normals;
			this.objFaces = faces;

			meshNormals = new List<Vector3>();
		}

		public MeshBuilder(Dictionary<int, Vector3> vertices, Dictionary<int, Vector3> normals, Dictionary<int, Vector3> textures, List<FaceReader.Face> faces)
			: this(vertices, normals, faces)
		{
			this.objTextures = textures;

			meshUV2 = new List<Vector2>();
			meshUV3 = new List<Vector2>();
		}

		public Mesh Build()
		{
			ProcessFaces();

			Mesh mesh = new Mesh();
			mesh.vertices = meshVertices.ToArray();
			mesh.normals = meshNormals.ToArray();
			mesh.triangles = meshTriangles;
			mesh.uv2 = meshUV2.ToArray();
			mesh.uv3 = meshUV3.ToArray();

			return mesh;
		}

		private void ProcessFaces()
		{
			foreach (FaceReader.Face face in objFaces) ProcessFace(face);
		}

		private void ProcessFace(FaceReader.Face face)
		{
			AddVertexFromFace(face, 0);
			AddVertexFromFace(face, 1);
			AddVertexFromFace(face, 2);

			meshTriangles[idx + 0] = idx + 0;
			meshTriangles[idx + 1] = idx + 1;
			meshTriangles[idx + 2] = idx + 2;

			idx += 3;
		}

		private void AddVertexFromFace(FaceReader.Face face, int vertexIdx)
		{
			meshVertices.Add(objVertices[face.vertexIndices[vertexIdx]]);

			if (face.HasNormalIndices())
			{
				meshNormals.Add(objNormals[face.normalIndices[vertexIdx]].normalized);
			}

			if (face.HasTextureIndices())
			{
				Vector3 texture = objTextures[face.textureIndices[vertexIdx]];
				meshUV2.Add(new Vector2(texture.x, texture.y));
				meshUV3.Add(new Vector2(texture.z, 0));
			}
		}
	}

	public class InvalidObjFileException : Exception
	{
		public InvalidObjFileException()
		{ }

		public InvalidObjFileException(string message)
			: base(message)
		{ }

		public InvalidObjFileException(string message, Exception inner)
			: base(message, inner)
		{ }
	}
}