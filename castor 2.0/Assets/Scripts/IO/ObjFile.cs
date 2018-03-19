using UnityEngine;
using System;

namespace IO
{
    public static class ObjFile
    {
        public static ReadResult Read(string path)
        {
            Mesh mesh;
            try
            {
                mesh = ObjFileReader.ImportFile(path);
            }
            catch (FileNotFoundException exception)
            {
                return ReadResult.ErrorResult(path, exception);
            }
            return ReadResult.OKResult(path, mesh);
        }

        public static WriteResult Write(Mesh mesh, string path)
        {
            WriteResult result;
            try
            {
                ObjExporter.MeshToFile(mesh, path);
                result = WriteResult.OKResult(string.Format("Wrote a mesh to the file: {0}", path));
            }
            catch (ArgumentException exception)
            {
                result = WriteResult.ErrorResult(string.Format("Invalid mesh in the file {0}", path));
                if (Application.isEditor) throw exception;
            }
            catch (Exception exception)
            {
                result = WriteResult.ErrorResult(path, exception);
                if (Application.isEditor) throw exception;
            }
            return result;
        }
    }
}

