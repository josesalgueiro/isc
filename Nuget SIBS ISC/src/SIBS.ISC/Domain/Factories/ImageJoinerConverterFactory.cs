namespace SIBS.ISC.Domain.Factories
{
	using SIBS.ISC.Abstractions;
	using SIBS.ISC.Exceptions;
	using SIBS.ISC.Options;

	public class ImageJoinerConverterFactory : BaseFactory, IImageJoinerConverterFactory
	{
		public IImageJoinerConverter CreateImageJoinerConverter(ImageJoinerConverterOption imageJoinerConverterOption)
		{
			// Create Image Joiner Converter Object base on options.
			return new ImageJoinerConverter();
		}

		public IImageJoinerConverter CreateImageJoinerConverter(string imageJoinerConverterConfigFileName, string? relativePath = null)
		{
			var imageJoinerConverterOption = CreateMergedImageConverterOption<ImageJoinerConverterOption>(imageJoinerConverterConfigFileName, relativePath);
			if (imageJoinerConverterOption is null)
			{
				if (string.IsNullOrWhiteSpace(imageJoinerConverterConfigFileName))
					throw new ImageJoinerConverterException("Doesn't exist image joiner converter config file.");
				else
					throw new ImageJoinerConverterException($"Doesn't exist image joiner converter config file with name: {imageJoinerConverterConfigFileName}.{ExtensionNameConfigFile}.");
			}

			// Create Image Joiner Converter Object base on options.
			return new ImageJoinerConverter();
		}
	}
}
