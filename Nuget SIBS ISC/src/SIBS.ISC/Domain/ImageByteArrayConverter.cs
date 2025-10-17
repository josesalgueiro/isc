namespace SIBS.ISC.Domain
{
	using FreeImageAPI;
	using SIBS.ISC.Abstractions;
	using SIBS.ISC.Helpers;

	public class ImageByteArrayConverter : IImageByteArrayConverter
	{
		public byte[] ConvertTo(byte[] imagesBytes, FREE_IMAGE_FORMAT freeImageFormat) 
		{
			byte[] resultBytes = [];

			Stream stream = new MemoryStream(imagesBytes);

			FIBITMAP freeImageLoadStream = FreeImage.LoadFromStream(stream);

			if (freeImageLoadStream != null)
			{
				// Convert the image to a byte array
				resultBytes = ImageToBytes(freeImageLoadStream, freeImageFormat);
			}
			
			return resultBytes;

		}

		public byte[]? ConvertToTiff(string fullPathFileName)
		{
			var file = ImageConverterHelper.ReadTargetFile(fullPathFileName);
			if (file is null)
				return null;

			return ConvertTo(file, FREE_IMAGE_FORMAT.FIF_TIFF);
		}

		public byte[]? ConvertToPng(string fullPathFileName)
		{
			var file = ImageConverterHelper.ReadTargetFile(fullPathFileName);
			if (file is null)
				return null;

			return ConvertTo(file, FREE_IMAGE_FORMAT.FIF_PNG);
		}
		public byte[]? ConvertToBmp(string fullPathFileName)
		{
			var file = ImageConverterHelper.ReadTargetFile(fullPathFileName);
			if (file is null)
				return null;

			return ConvertTo(file, FREE_IMAGE_FORMAT.FIF_BMP);
		}

		public byte[]? ConvertToGif(string fullPathFileName)
		{
			var file = ImageConverterHelper.ReadTargetFile(fullPathFileName);
			if (file is null)
				return null;

			return ConvertTo(file, FREE_IMAGE_FORMAT.FIF_GIF);
		}

		public byte[]? ConvertToJpg(string fullPathFileName)
		{
			var file = ImageConverterHelper.ReadTargetFile(fullPathFileName);
			if (file is null)
				return null;

			return ConvertTo(file, FREE_IMAGE_FORMAT.FIF_JPEG);
		}

		public byte[]? ConvertToJpg(byte[] byteArray)
		{
			
			if (byteArray is null)
				return null;

			return ConvertTo(byteArray, FREE_IMAGE_FORMAT.FIF_JPEG);
		}

		static byte[] ImageToBytes(FIBITMAP image, FREE_IMAGE_FORMAT format)
		{
			// Create a MemoryStream to hold the image bytes
			using MemoryStream stream = new MemoryStream();

			// Save the image to the MemoryStream
			FreeImage.SaveToStream(image, stream, format);

			// Return the byte array
			return stream.ToArray();
		}

	}
}
