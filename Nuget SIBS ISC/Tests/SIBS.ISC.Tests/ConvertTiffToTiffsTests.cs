namespace SIBS.ISC.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using FluentAssertions;
	using SIBS.ISC.Domain.Convertions;
	using SIBS.ISC.Options;
	using Xunit;

	public class ConvertTiffToTiffsTests : BaseSplitterTest
	{
		private SpecificImageSplitterConverterOption ConverterOptionTiff(int productType, /*int partitionMode,*/
			int tiffMaxWidth, int tiffMaxHeight, uint tiffWidthDPI, uint tiffHeightDPI,
			PDFiumRenderFlagEnum tiffPdfiumRenderFlag, FreeImageConverToColorBitsEnum tiffFreeImageConverToColorBits,
			FreeImageColorDepthEnum tiffFreeImageColorDepth, FreeImageCompressionEnum tiffFreeImageCompression, bool tiffIsUsingColorCorrect)
		{
			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = new SpecificImageSplitterConverterOption
			{
				ProductType = productType,
				//PageSheetPartitionMode = partitionMode,
				TiffMaxWidth = tiffMaxWidth,
				TiffMaxHeight = tiffMaxHeight,
				TiffWidthDPI = tiffWidthDPI,
				TiffHeightDPI = tiffHeightDPI,
				TiffPdfiumRenderFlag = tiffPdfiumRenderFlag,
				TiffFreeImageConverToColorBits = tiffFreeImageConverToColorBits,
				TiffFreeImageColorDepth = tiffFreeImageColorDepth,
				TiffFreeImageCompression = tiffFreeImageCompression,
				TiffIsUsingColorCorrect = tiffIsUsingColorCorrect
			};

			return specificImageSplitterConverterOption;
		}

		[Fact]
		public void ConvertTiffToTiffs_Valid_ConvertTiffToTiffs()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.GREYSCALE_BPPDITHER, FreeImageCompressionEnum.FAX_LZW, false);

			var tiffContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "TIFs", "ERRO_23286800024601.tiff")));
			ConvertTiffToTiffs convertTiffToTiffs = new ConvertTiffToTiffs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertTiffToTiffs.StartConvertTiffToTiffs(tiffContent, 0);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("TIFFs", $"TIFFtoTiffs_test01_{imageIndex}.tiff", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertTiffToTiffs_Valid_ConvertTiffToTiffs_PartitionMode1_ColorBits24_ColorDepthGreyScale_CompressionFaxLZW()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.GREYSCALE_BPPDITHER, FreeImageCompressionEnum.FAX_LZW, false);

			var tiffContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "TIFs", "ERRO_23286800024601.tiff")));
			ConvertTiffToTiffs convertTiffToTiffs = new ConvertTiffToTiffs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertTiffToTiffs.StartConvertTiffToTiffs(tiffContent, 1);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("TIFFs", $"TIFFtoTiffs_test02_{imageIndex}.tiff", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertTiffToTiffs_Valid_ConvertTiffToTiffs_PartitionMode1_ColorBits16_ColorDepthNONE_CompressionNONE()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS16, FreeImageColorDepthEnum.NONE, FreeImageCompressionEnum.NONE, false);

			var tiffContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "TIFs", "ERRO_23286800024601.tiff")));
			ConvertTiffToTiffs convertTiffToTiffs = new ConvertTiffToTiffs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertTiffToTiffs.StartConvertTiffToTiffs(tiffContent, 1);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("TIFFs", $"TIFFtoTiffs_test03_{imageIndex}.tiff", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertTiffToTiffs_Valid_ConvertTiffToTiffs_PartitionMode1_ColorBits8_ColorDepthNONE_CompressionNONE()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS8, FreeImageColorDepthEnum.NONE, FreeImageCompressionEnum.NONE, false);

			var tiffContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "TIFs", "ERRO_23286800024601.tiff")));
			ConvertTiffToTiffs convertTiffToTiffs = new ConvertTiffToTiffs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertTiffToTiffs.StartConvertTiffToTiffs(tiffContent, 1);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("TIFFs", $"TIFFtoTiffs_test04_{imageIndex}.tiff", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertTiffToTiffs_Valid_ConvertTiffToTiffs_PartitionMode1_ColorBitsGREYSCALE_ColorDepthNONE_CompressionNONE()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.GREYSCALE, FreeImageColorDepthEnum.NONE, FreeImageCompressionEnum.NONE, false);

			var tiffContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "TIFs", "ERRO_23286800024601.tiff")));
			ConvertTiffToTiffs convertTiffToTiffs = new ConvertTiffToTiffs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertTiffToTiffs.StartConvertTiffToTiffs(tiffContent, 1);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("TIFFs", $"TIFFtoTiffs_test05_{imageIndex}.tiff", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}



	}
}
