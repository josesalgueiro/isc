namespace SIBS.ISC.Domain.Factories
{
	using SIBS.ISC.Abstractions;
	using SIBS.ISC.Exceptions;
	using SIBS.ISC.Options;

	public class ImageSplitterConverterFactory : BaseFactory, IImageSplitterConverterFactory
	{
		public IImageSplitterConverter CreateImageSplitterConverter()
		{
			// Create Image Splitter Converter Object base on default options.
			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = new SpecificImageSplitterConverterOption();

			specificImageSplitterConverterOption.ProductType = 99;

			specificImageSplitterConverterOption.JpgMaxWidth = 816;
			specificImageSplitterConverterOption.JpgMaxHeight = 1154;
			specificImageSplitterConverterOption.JpgWidthDPI = 300;
			specificImageSplitterConverterOption.JpgHeightDPI = 300;
			specificImageSplitterConverterOption.JpgPDFiumRenderFlag = PDFiumRenderFlagEnum.NONE;
			specificImageSplitterConverterOption.JpgFreeImageQuality = FreeImageQualityEnum.SUPERB;
			specificImageSplitterConverterOption.JpgFreeImageConverToColorBits = FreeImageConverToColorBitsEnum.BITS24;

			specificImageSplitterConverterOption.TiffMaxWidth = 1080;
			specificImageSplitterConverterOption.TiffMaxHeight = 1920;
			specificImageSplitterConverterOption.TiffWidthDPI = 300;
			specificImageSplitterConverterOption.TiffHeightDPI = 300;
			specificImageSplitterConverterOption.TiffPdfiumRenderFlag = PDFiumRenderFlagEnum.NONE;
			specificImageSplitterConverterOption.TiffFreeImageColorDepth = FreeImageColorDepthEnum.GREYSCALE_BPPDITHER;
			specificImageSplitterConverterOption.TiffFreeImageConverToColorBits = FreeImageConverToColorBitsEnum.BITS24;
			specificImageSplitterConverterOption.TiffFreeImageCompression = FreeImageCompressionEnum.FAX_LZW;
			specificImageSplitterConverterOption.TiffIsUsingColorCorrect = false;

			return new ImageSplitterConverter(specificImageSplitterConverterOption);
		}

		public IImageSplitterConverter CreateImageSplitterConverter(SpecificImageSplitterConverterOption specificImageSplitterConverterOption)
		{
			// Create Image Splitter Converter Object base on options.
			return new ImageSplitterConverter(specificImageSplitterConverterOption);
		}

		public IImageSplitterConverter CreateImageSplitterConverter(int productType, string imageSplitterConverterConfigFileName, string? relativePath = null)
		{
			var imageSplitterConverterOption = CreateMergedImageConverterOption<ImageSplitterConverterOption>(imageSplitterConverterConfigFileName, relativePath);
			if (imageSplitterConverterOption is null)
			{
				if (string.IsNullOrWhiteSpace(imageSplitterConverterConfigFileName))
					throw new ImageSplitterConverterException("Doesn't exist image splitter converter config file.");
				else
					throw new ImageSplitterConverterException($"Doesn't exist image splitter converter config file with name: {imageSplitterConverterConfigFileName}.{ExtensionNameConfigFile}.");
			}

			var specificOption = imageSplitterConverterOption.ProductOptions.SingleOrDefault(o => o.ProductType == productType);
			if (specificOption is null)
				throw new ImageSplitterConverterException($"Options file does not have a option for product {productType}");



			// Create Image Splitter Converter Object base on options.
			return new ImageSplitterConverter(specificOption);
			
		}

	}
}
