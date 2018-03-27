using UnityEngine;
using System;

namespace IO
{
    public static class ObjFile
    {
        public static ReadResult Read(string path)
        {
            return new ObjFileReader(path).ImportFile();
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

