namespace SIBS.ISC.Domain
{
	using SIBS.ISC.Abstractions;
	using SIBS.ISC.Domain.Convertions;
	using SIBS.ISC.Options;

	internal class ImageSplitterConverter : IImageSplitterConverter
	{
		private readonly SpecificImageSplitterConverterOption _specificImageSplitterConverterOption;

		public ImageSplitterConverter(SpecificImageSplitterConverterOption specificImageSplitterConverterOption)
		{
			this._specificImageSplitterConverterOption = specificImageSplitterConverterOption;
		}

		public IEnumerable<string> ConvertPdfToJpgs(Stream originalFile, int partitionMode)
		{
			ConvertPdfToJpgs convertPdfToJpgs = new ConvertPdfToJpgs(_specificImageSplitterConverterOption);
			return convertPdfToJpgs.StartConvertPdfToJpgs(originalFile, partitionMode);

		}

		public IEnumerable<string> ConvertPdfToTiffs(Stream originalFile, int partitionMode)
		{
			ConvertPdfToTiffs convertPdfToTiffs = new ConvertPdfToTiffs(_specificImageSplitterConverterOption);
			return convertPdfToTiffs.StartConvertPdfToTiffs(originalFile, partitionMode);

		}

		public IEnumerable<string> ConvertJpgToJpg(Stream originalFile, int partitionMode)
		{
			ConvertJpgToJpg convertToJpgs = new ConvertJpgToJpg(_specificImageSplitterConverterOption);
			return convertToJpgs.StartConvertJpgToJpg(originalFile, partitionMode);

		}

		public IEnumerable<string> ConvertTiffToJpgs(Stream originalFile, int partitionMode)
		{
			ConvertTiffToJpgs convertToJpgs = new ConvertTiffToJpgs(_specificImageSplitterConverterOption);
			return convertToJpgs.StartConvertTiffToJpgs(originalFile, partitionMode);

		}

		public IEnumerable<string> ConvertJpgsToTiffs(Stream originalFile, int partitionMode)
		{
			ConvertJpgsToTiffs convertToTiffs = new ConvertJpgsToTiffs(_specificImageSplitterConverterOption);
			IList<Stream> originalFiles = new List<Stream>();
			originalFiles.Add(originalFile);
			return convertToTiffs.StartConvertJpgsToTiffs(originalFiles, partitionMode);
		}

		public IEnumerable<string> ConvertJpgsToTiffs(IList<Stream> originalFiles, int partitionMode)
		{
			ConvertJpgsToTiffs convertToTiffs = new ConvertJpgsToTiffs(_specificImageSplitterConverterOption);
			return convertToTiffs.StartConvertJpgsToTiffs(originalFiles, partitionMode);
		}

		public IEnumerable<string> ConvertTiffToTiffs(Stream originalFile, int partitionMode)
		{
			ConvertTiffToTiffs convertTiffToTiffs = new ConvertTiffToTiffs(_specificImageSplitterConverterOption);
			return convertTiffToTiffs.StartConvertTiffToTiffs(originalFile, partitionMode);
		}


	}
}
