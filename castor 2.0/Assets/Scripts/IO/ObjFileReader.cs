using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace IO
{
    public class ObjFileReader
    {
        private readonly string filePath;
        private ReadResult result = null;

        List<Reader> readers;

        public ObjFileReader(string filePath)
        {
            this.filePath = filePath;

            this.readers = new List<Reader>
            {
                new CommentReader(),
                new VertexReader(),
                new VertexNormalReader(),
                new FaceReader(),
                new VertexTextureReader(),
                new GroupReader(),
                new SmoothingGroupReader(),
                new ObjectReader(),
            };
        }

        public ReadResult ImportFile()
        {
            string[] lines = ReadLines();

            foreach (string line in lines) ProcessLine(line);

            if (!EncounteredError())
            {
                Mesh mesh = BuildMesh();
                result = ReadResult.OKResult(filePath, mesh);
            }
            return result;
        }

        private string[] ReadLines()
        {
            string[] lines;
            try
            {
                lines = System.IO.File.ReadAllLines(filePath);
            }
            catch (Exception exception)
            {
                //Generate an empty string array
                lines = new string[0];

                //Fill the result object
                result = ReadResult.ErrorResult(filePath, exception);
            }
            return lines;
        }

        private void ProcessLine(string line)
        {
            try
            {
                string trimmedLine = line.Trim();
                foreach (Reader reader in this.readers)
                {
                    if (reader.IsApplicable(line))
                    {
                        reader.Read(line);
                        return;
                    }
                }
                Debug.LogWarning("Encountered and ignored an unrecognised line: " + line);
            }
            catch (Exception e)
            {
                result = ReadResult.ErrorResult(filePath, e);
            }
        }

        private bool EncounteredError()
        {
            return (result != null);
        }

        private Mesh BuildMesh()
        {
            Mesh mesh = new Mesh();

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
            catch (Exception e) { throw e; }
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

        public class Face
        {
            public readonly int[] vertexIndices = null;
            public readonly int[] normalIndices = null;

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
}