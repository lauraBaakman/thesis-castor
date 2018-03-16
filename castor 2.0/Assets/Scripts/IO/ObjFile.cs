using UnityEngine;
using Ticker;

namespace IO
{
    internal static class ObjFile
    {
        public static ReadResult Read(string path)
        {
            Mesh mesh = ObjFileReader.ImportFile(path);
            return new ReadResult(IOCode.OK, path, mesh);
        }

        public static WriteResult Write(Mesh mesh, string path)
        {
            Debug.Log("Temporarily returning OK, without writing the mesh to file");
            return new WriteResult(IOCode.OK, path);
        }
    }
}

