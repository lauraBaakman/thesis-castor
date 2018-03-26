using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace IO
{
    public class ObjFileReader
    {
        private readonly string filePath;
        private ReadResult result = null;

        private CommentReader commentReader;
        private VertexReader vertexReader;
        private VertexNormalReader normalReader;
        private FaceReader faceReader;

        public ObjFileReader(string filePath)
        {
            this.filePath = filePath;

            commentReader = new CommentReader();
            vertexReader = new VertexReader();
            normalReader = new VertexNormalReader();
            faceReader = new FaceReader();
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
            string trimmedLine = Trim(line);

            if (commentReader.IsApplicable(trimmedLine)) commentReader.Read(trimmedLine);
            if (vertexReader.IsApplicable(trimmedLine)) vertexReader.Read(trimmedLine);
            if (normalReader.IsApplicable(trimmedLine)) normalReader.Read(trimmedLine);
            if (faceReader.IsApplicable(trimmedLine)) faceReader.Read(trimmedLine);
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
            throw new NotImplementedException();
        }
    }

    public class VertexReader : Reader
    {
        public VertexReader()
            : base("v")
        { }

        public override void Read(string line)
        {
            throw new NotImplementedException();
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