namespace SIBS.ISC.Domain.Factories
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using SIBS.ISC.Abstractions;
	using SIBS.ISC.Exceptions;
	using SIBS.ISC.Options;

	public class ImageByteArrayConverterFactory : BaseFactory, IImageByteArrayConverterFactory
	{
		public IImageByteArrayConverter CreateByteArrayConverter()
		{
			throw new NotImplementedException();
		}

		public IImageByteArrayConverter CreateByteArrayConverter(ImageByteArrayConverterOption imageByteArrayConverterOption)
		{
			// Create Image Byte Array Converter Object base on options.
			return new ImageByteArrayConverter();
		}

		public IImageByteArrayConverter CreateByteArrayConverter(string imageByteArrayConverterConfigFileName, string? relativePath = null)
		{
			var imageJoinerConverterOption = CreateMergedImageConverterOption<ImageJoinerConverterOption>(imageByteArrayConverterConfigFileName, relativePath);
			if (imageJoinerConverterOption is null)
			{
				if (string.IsNullOrWhiteSpace(imageByteArrayConverterConfigFileName))
					throw new ImageJoinerConverterException("Doesn't exist image joiner converter config file.");
				else
					throw new ImageJoinerConverterException($"Doesn't exist image joiner converter config file with name: {imageByteArrayConverterConfigFileName}.{ExtensionNameConfigFile}.");
			}

			// Create Image Byte Array Converter Object base on options.
			return new ImageByteArrayConverter();
		}
	}
}
