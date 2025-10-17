namespace SIBS.ISC.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	public class ShouldNotHappenIscException : Exception
	{
		public ShouldNotHappenIscException()
		{
		}

		public ShouldNotHappenIscException(string? message) : base(message)
		{
		}

		public ShouldNotHappenIscException(string? message, Exception? innerException) : base(message, innerException)
		{
		}

		protected ShouldNotHappenIscException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
