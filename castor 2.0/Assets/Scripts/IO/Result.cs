using System;
using Ticker;
using UnityEngine;

namespace IO
{
	public enum IOCode { OK, Error }

	public abstract class IOResult : IToTickerMessage
	{
		protected readonly IOCode status;
		protected Message message;

		protected IOResult(IOCode status, string message)
			: this(status)
		{
			this.message = buildMessage(message);
		}

		protected IOResult(IOCode status)
		{
			this.status = status;
		}

		protected Message buildMessage(string messageText)
		{
			if (status.Equals(IOCode.OK)) return new Message.InfoMessage(messageText);
			return new Message.ErrorMessage(messageText);
		}

		public string Message
		{
			get { return message.Text; }
		}

		public bool Succeeded
		{
			get { return status.Equals(IOCode.OK); }
		}

		public bool Failed
		{
			get { return status.Equals(IOCode.Error); }
		}

		public Message ToTickerMessage()
		{
			return this.message;
		}
	}

	public class ReadResult : IOResult
	{

		private static Vector3 noPivot = new Vector3(float.NaN, float.NaN, float.NaN);

		public readonly Mesh Mesh;

		public readonly Vector3 Pivot;

		public bool HasPivot { get { return !this.Pivot.ContainsNaNs(); } }

		private ReadResult(IOCode status, string message)
			: base(status, message)
		{ }

		private ReadResult(IOCode status, string message, Mesh mesh, Vector3 pivot)
			: base(status, message)
		{
			this.Mesh = mesh;
			this.Pivot = pivot;
		}

		public static ReadResult OKResult(string path, Mesh mesh)
		{
			Vector3 pivot = noPivot;
			return OKResult(path, mesh, pivot);
		}

		public static ReadResult OKResult(string path, Mesh mesh, Vector3 pivot)
		{
			string messageText = string.Format("Succesfully read the file {0}", path);
			return new ReadResult(IOCode.OK, messageText, mesh, pivot);
		}

		public static ReadResult ErrorResult(string messageText)
		{
			return new ReadResult(IOCode.Error, messageText);
		}

		public static ReadResult ErrorResult(string path, Exception exception)
		{
			string messageText = string.Format("Could not read a fragment from the file {0}: {1}", path, exception.Message);
			return ErrorResult(messageText);
		}
	};

	public class WriteResult : IOResult
	{
		private WriteResult(IOCode status, string messageText)
			: base(status, messageText)
		{ }

		internal static WriteResult ErrorResult(string messageText)
		{
			return new WriteResult(IOCode.Error, messageText);
		}

		internal static WriteResult ErrorResult(string path, Exception exception)
		{
			string messageText = string.Format("Could not write the fragment to the file {0}: {1}", path, exception.Message);
			return ErrorResult(messageText);
		}

		internal static WriteResult OKResult(string path)
		{
			string messageText = string.Format("Succesfully wrote the fragment to the file {0}", path);
			return new WriteResult(IOCode.OK, messageText);
		}
	}
}