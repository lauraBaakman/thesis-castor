using System;
using UnityEngine;

namespace IO
{
    public class ObjFileReader
    {
        private readonly string filePath;
        private ReadResult result = null;

        public ObjFileReader(string filePath)
        {
            this.filePath = filePath;
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

        private bool EncounteredError()
        {
            return (result != null);
        }

        private void ProcessLine(string line)
        {

        }

        private Mesh BuildMesh()
        {
            Mesh mesh = new Mesh();

            return mesh;
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
    }
}