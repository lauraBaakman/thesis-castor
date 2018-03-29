using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace IO
{
    public class ObjFileReader
    {
        private readonly string filePath;

        private readonly Dictionary<string, Reader> readers;

        public ObjFileReader(string filePath)
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
                foreach (Reader reader in this.readers.Values)
                {
                    if (reader.IsApplicable(line))
                    {
                        reader.Read(line);
                        return;
                    }
                }
                Debug.LogWarning("Encountered and ignored an unrecognised line: " + line);
            }
            catch (Exception exception) { throw exception; }
        }

        private Mesh BuildMesh()
        {
            VertexReader vertexReader = readers["vertex"] as VertexReader;
            VertexNormalReader normalReader = readers["normal"] as VertexNormalReader;
            FaceReader faceReader = readers["face"] as FaceReader;

            MeshBuilder builder = new MeshBuilder(vertexReader.elements, normalReader.elements, faceReader.faces);
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
        private readonly Regex vectorRegex;

        protected VectorReader(string lineTypeSymbol)
            : base(lineTypeSymbol)
        {
            vectorRegex = new Regex(typeRegex + @"(?<x>\+?\-?\d+(\.\d+)?)\s+(?<y>\+?\-?\d+(\.\d+)?)\s+(?<z>\+?\-?\d+(\.\d+)?)$");
        }

        public override Vector3 ExtractElement(string line)
        {
            Vector3 vertex;
            try
            {
                MatchCollection matches = vectorRegex.Matches(line);
                GroupCollection groups = matches[0].Groups;
                vertex = new Vector3(
                    x: float.Parse(groups["x"].Value),
                    y: float.Parse(groups["y"].Value),
                    z: float.Parse(groups["z"].Value)
                );
            }
            catch (Exception e)
            {
                throw new CouldNotReadFileException(
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

        public override void Read(string line)
        {
            //do nothing, comments are not part of the mesh
        }
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
        private Regex noNormalFaceRegex;
        private Regex completeFaceRegex;

        public List<Face> faces;

        public FaceReader()
            : base("f")
        {
            faces = new List<Face>();

            Regex vertex = new Regex(@"(\d+)\s*/\s*/\s*(\d+)");

            noNormalFaceRegex = new Regex(typeRegex + @"(?<v0>\d+)\s+(?<v1>\d+)\s+(?<v2>\d+)$");
            completeFaceRegex = new Regex(typeRegex.ToString() + vertex + @"\s+" + vertex + @"\s+" + vertex + @"$");
        }

        public override void Read(string line)
        {
            Face face = ExtractFace(line);
            faces.Add(face);
        }

        public Face ExtractFace(string line)
        {
            if (HasNormals(line)) return ExtractCompleteFace(line);

            return ExtractNoNormalFace(line);
        }

        public bool HasNormals(string line)
        {
            return !noNormalFaceRegex.Match(line).Success;
        }

        public Face ExtractNoNormalFace(string line)
        {
            MatchCollection matches = noNormalFaceRegex.Matches(line);
            GroupCollection groups = matches[0].Groups;

            Face face;
            try
            {
                face = new Face(
                    v0: Int32.Parse(groups["v0"].Value),
                    v1: Int32.Parse(groups["v1"].Value),
                    v2: Int32.Parse(groups["v2"].Value)
                );
            }
            catch (Exception e) { throw e; }
            return face;
        }

        public Face ExtractCompleteFace(string line)
        {
            MatchCollection matches = completeFaceRegex.Matches(line);
            GroupCollection groups = matches[0].Groups;

            Face face;
            try
            {
                face = new Face(
                    v0: Int32.Parse(groups[1].Value),
                    n0: Int32.Parse(groups[2].Value),
                    v1: Int32.Parse(groups[3].Value),
                    n1: Int32.Parse(groups[4].Value),
                    v2: Int32.Parse(groups[5].Value),
                    n2: Int32.Parse(groups[6].Value));
            }
            catch (Exception e) { throw e; }

            return face;
        }

        public class Face : IEquatable<Face>
        {
            public readonly int[] vertexIndices = null;
            public readonly int[] normalIndices = null;

            public int v0 { get { return vertexIndices[0]; } }
            public int v1 { get { return vertexIndices[1]; } }
            public int v2 { get { return vertexIndices[2]; } }

            public int n0 { get { return normalIndices[0]; } }
            public int n1 { get { return normalIndices[1]; } }
            public int n2 { get { return normalIndices[2]; } }

            public Face(
                int v0, int v1, int v2,
                int n0, int n1, int n2
            )
            {
                vertexIndices = new int[3];
                vertexIndices[0] = v0;
                vertexIndices[1] = v1;
                vertexIndices[2] = v2;

                normalIndices = new int[3];
                normalIndices[0] = n0;
                normalIndices[1] = n1;
                normalIndices[2] = n2;
            }

            public Face(int v0, int v1, int v2)
            {
                vertexIndices = new int[3];
                vertexIndices[0] = v0;
                vertexIndices[1] = v1;
                vertexIndices[2] = v2;
            }

            public bool HasNormalIndices()
            {
                return this.normalIndices != null;
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
                return verticesEqual && normalsEqual;
            }
        }
    }

    public class VertexTextureReader : Reader
    {
        public VertexTextureReader()
            : base("vt")
        { }

        public override void Read(string line)
        {
            Debug.LogWarning("Textures are ignored");
        }
    }

    public class GroupReader : Reader
    {
        public GroupReader()
            : base("g")
        { }

        public override void Read(string line)
        {
            Debug.LogWarning("Groups are ignored");
        }
    }

    public class SmoothingGroupReader : Reader
    {
        public SmoothingGroupReader()
            : base("s")
        { }

        public override void Read(string line)
        {
            Debug.LogWarning("Smoothing Groups are ignored");
        }
    }

    public class ObjectReader : Reader
    {
        public ObjectReader()
            : base("o")
        { }

        public override void Read(string line)
        {
            Debug.LogWarning("Objects are ignored");
        }
    }

    public class MeshBuilder
    {
        private readonly Dictionary<int, Vector3> objVertices;
        private readonly Dictionary<int, Vector3> objMormals;
        private readonly List<FaceReader.Face> objFaces;

        private readonly List<Vector3> meshVertices;
        private readonly List<Vector3> meshNormals;
        private readonly int[] meshTriangles;

        private int idx = 0;

        public MeshBuilder(Dictionary<int, Vector3> vertices, Dictionary<int, Vector3> normals, List<FaceReader.Face> faces)
        {
            this.objVertices = vertices;
            this.objMormals = normals;
            this.objFaces = faces;

            meshVertices = new List<Vector3>();
            meshNormals = new List<Vector3>();
            meshTriangles = new int[faces.Count * 3];
        }

        public Mesh Build()
        {
            ProcessFaces();

            Mesh mesh = new Mesh();
            mesh.vertices = meshVertices.ToArray();
            mesh.normals = meshNormals.ToArray();
            mesh.triangles = meshTriangles;

            return mesh;
        }

        private void ProcessFaces()
        {
            foreach (FaceReader.Face face in objFaces) ProcessFace(face);
        }

        private void ProcessFace(FaceReader.Face face)
        {
            meshVertices.Add(objVertices[face.v0]);
            meshNormals.Add(objMormals[face.n0]);

            meshVertices.Add(objVertices[face.v1]);
            meshNormals.Add(objMormals[face.n1]);

            meshVertices.Add(objVertices[face.v2]);
            meshNormals.Add(objMormals[face.n2]);

            meshTriangles[idx + 0] = idx + 0;
            meshTriangles[idx + 1] = idx + 1;
            meshTriangles[idx + 2] = idx + 2;

            idx += 3;
        }
    }

    public class CouldNotReadFileException : Exception
    {
        public CouldNotReadFileException()
        { }

        public CouldNotReadFileException(string message)
            : base(message)
        { }

        public CouldNotReadFileException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}