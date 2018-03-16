using UnityEngine;
using Ticker;

namespace IO
{
    public static class FragmentFile
    {
        public static ReadResult Read(string path)
        {
            Mesh mesh = ObjFileReader.ImportFile(path);
            return new ReadResult(IOCode.OK, path, mesh);
        }

        public static bool Write(Mesh mesh, string path)
        {
            Debug.Log("Temporarily returning true, without writing the mesh to file");
            return true;
        }

        internal enum IOCode { OK, Error }

        public class ReadResult : IToTickerMessage
        {
            private Mesh mesh;
            private readonly IOCode status;
            private readonly string message;

            public Mesh Mesh
            {
                get { return mesh; }
            }

            internal ReadResult(IOCode status, string path, Mesh mesh)
            {
                this.mesh = mesh;
                this.status = status;
                this.message = buildMessage(status, path);
            }

            private string buildMessage(IOCode statusCode, string path)
            {
                return statusCode.Equals(IOCode.OK)
                    ? string.Format("Succesfully read the file {0}", path)
                    : string.Format("Could not read the file {0}", path);
            }

            public bool Succeeded()
            {
                return status.Equals(IOCode.OK);
            }

            public bool Failed()
            {
                return status.Equals(IOCode.Error);
            }

            public Message ToTickerMessage()
            {
                if (status.Equals(IOCode.OK)) return ToInfoMessage();
                return ToErrorMessage();
            }

            private Message ToInfoMessage()
            {
                return new Message.InfoMessage(message);
            }

            private Message ToErrorMessage()
            {
                return new Message.ErrorMessage(message);
            }
        };

    }
}

