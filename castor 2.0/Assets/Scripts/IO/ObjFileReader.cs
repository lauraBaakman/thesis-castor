using UnityEngine;

namespace IO
{
    public class ObjFileReader
    {
        private string filePath;

        public ObjFileReader(string filePath)
        {
            this.filePath = filePath;
        }

        public Mesh ImportFile()
        {
            Mesh mesh = new Mesh();

            return mesh;
        }
    }
}