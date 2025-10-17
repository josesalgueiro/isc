namespace SIBS.ISC.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	public class ImageSplitterConverterException : Exception
	{
		public ImageSplitterConverterException()
		{
		}

		public ImageSplitterConverterException(string? message) : base(message)
		{
		}

		public ImageSplitterConverterException(string? message, Exception? innerException) : base(message, innerException)
		{
		}

		protected ImageSplitterConverterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
