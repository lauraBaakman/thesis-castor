using UnityEngine;
using NUnit.Framework;

namespace IO
{
    public static class ObjFileReader
    {
        public static Mesh ImportFile(string filePath)
        {
            return new _ObjFileReader(filePath).Read();
        }
    }

    public class _ObjFileReader
    {
        private string filepath;

        public _ObjFileReader(string filepath)
        {
            this.filepath = filepath;
        }

        public Mesh Read()
        {
            Mesh mesh = new Mesh();

            return mesh;
        }
    }
}