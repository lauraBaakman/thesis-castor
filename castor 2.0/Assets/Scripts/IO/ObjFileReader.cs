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
                string trimmedLine = Trim(line);
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

        public string Trim(string line)
        {
            return line.Trim();
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
        protected Regex isApplicableRegex;

        protected Reader(string lineTypeSymbol)
        {
            isApplicableRegex = new Regex(@"^" + lineTypeSymbol + @"\s");
        }

        public bool IsApplicable(string line)
        {
            return isApplicableRegex.IsMatch(line);
        }

        public abstract void Read(string line);
    }

    public class CommentReader : Reader
    {
        public CommentReader()
            : base("#")
        { }

        public override void Read(string line)
        {
            //do nothing, comments are irrelevant
        }
    }

    public class VertexReader : Reader
    {
        public Dictionary<int, Vector3> vertices;

        private readonly Regex extractTypeRegex;
        private int currentReferenceNumber = 1;

        public VertexReader()
            : base("v")
        {
            vertices = new Dictionary<int, Vector3>();

            extractTypeRegex = new Regex(@"^v\s+
            	(?<x>\+?\-?\d+(\.\d+)?)\s+
				(?<y>\+?\-?\d+(\.\d+)?)\s+
				(?<z>\+?\-?\d+(\.\d+)?)$");
        }

        public override void Read(string line)
        {
            try
            {
                Vector3 vertex = ExtractVertex(line);
                vertices.Add(currentReferenceNumber++, vertex);
            }
            catch (Exception e) { throw e; }
        }

        public Vector3 ExtractVertex(string line)
        {
            Vector3 vertex;
            try
            {
                MatchCollection matches = extractTypeRegex.Matches(line);
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

    public class VertexNormalReader : Reader
    {
        public VertexNormalReader()
            : base("vn")
        { }

        public override void Read(string line)
        {
            throw new NotImplementedException();
        }
    }

    public class FaceReader : Reader
    {
        public FaceReader()
            : base("f")
        { }

        public override void Read(string line)
        {
            throw new NotImplementedException();
        }
    }

    public class VertexTextureReader : Reader
    {
        public VertexTextureReader()
            : base("vt")
        { }

        public override void Read(string line)
        {
            //do nothing
        }
    }

    public class GroupReader : Reader
    {
        public GroupReader()
            : base("g")
        { }

        public override void Read(string line)
        {
            //do nothing
        }
    }

    public class SmoothingGroupReader : Reader
    {
        public SmoothingGroupReader()
            : base("s")
        { }

        public override void Read(string line)
        {
            //do nothing
        }
    }

    public class ObjectReader : Reader
    {
        public ObjectReader()
            : base("o")
        { }

        public override void Read(string line)
        {
            //do nothing
        }
    }
}