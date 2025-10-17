namespace SIBS.ISC.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	public class ImageJoinerConverterException : Exception
	{
		public ImageJoinerConverterException()
		{
		}

		public ImageJoinerConverterException(string? message) : base(message)
		{
		}

		public ImageJoinerConverterException(string? message, Exception? innerException) : base(message, innerException)
		{
		}

		protected ImageJoinerConverterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
