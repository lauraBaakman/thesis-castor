using UnityEngine;
using System;

namespace IO
{
    public static class ObjFile
    {
        public static ReadResult Read(string path)
        {
            Mesh mesh = ObjFileReader.ImportFile(path);
            return ReadResult.OKResult(path, mesh);
        }

        public static WriteResult Write(Mesh mesh, string path)
        {
            WriteResult result;
            try
            {
                ObjExporter.MeshToFile(mesh, path);
                result = WriteResult.OKResult(path);
            }
            catch (ArgumentException)
            {
                result = WriteResult.ErrorResult(string.Format("Invalid mesh in the file {0}", path));
            }
            catch (Exception exception)
            {
                result = WriteResult.ErrorResult(path, exception);
            }

            return result;

        }
    }
}

