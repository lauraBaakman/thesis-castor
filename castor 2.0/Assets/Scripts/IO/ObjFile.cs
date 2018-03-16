using UnityEngine;
using System;

namespace IO
{
    public static class ObjFile
    {
        public static ReadResult Read(string path)
        {
            Mesh mesh = ObjFileReader.ImportFile(path);
            return new ReadResult(IOCode.OK, path, mesh);
        }

        public static WriteResult Write(Mesh mesh, string path)
        {
            IOCode status;
            try
            {
                ObjExporter.MeshToFile(mesh, path);
                status = IOCode.OK;
            }
            catch (ArgumentException)
            {
                status = IOCode.Error;
            }
            return new WriteResult(status, path);
        }
    }
}

