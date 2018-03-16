using Ticker;
using UnityEngine;

namespace IO
{
    public enum IOCode { OK, Error }

    public abstract class IOResult : IToTickerMessage
    {
        protected readonly IOCode status;
        protected Message message;

        internal IOResult(IOCode status, string path)
        {
            this.status = status;
            this.message = buildMessage(status, path);
        }

        protected Message buildMessage(IOCode statusCode, string path)
        {
            string messageText = buildMessageText(statusCode, path);

            if (status.Equals(IOCode.OK)) return new Message.InfoMessage(messageText);
            return new Message.ErrorMessage(messageText);
        }

        internal abstract string buildMessageText(IOCode statusCode, string path);

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
            return this.message;
        }
    }

    public class ReadResult : IOResult
    {
        private Mesh mesh;

        public Mesh Mesh
        {
            get { return mesh; }
        }

        internal ReadResult(IOCode status, string path, Mesh mesh)
            : base(status, path)
        {
            this.mesh = mesh;
        }

        internal override string buildMessageText(IOCode statusCode, string path)
        {
            return statusCode.Equals(IOCode.OK)
                ? string.Format("Succesfully read the file {0}", path)
                : string.Format("Could not read the file {0}", path);
        }
    };

    public class WriteResult : IOResult
    {
        internal WriteResult(IOCode status, string path)
            : base(status, path)
        { }

        internal override string buildMessageText(IOCode statusCode, string path)
        {
            return statusCode.Equals(IOCode.OK)
                             ? string.Format("Succesfully wrote the fragment to the file {0}", path)
                                 : string.Format("Could not write the fragment to the file {0}", path);
        }
    }
}