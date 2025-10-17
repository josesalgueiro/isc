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

	public class ConvertJpgsToTiffsTests : BaseSplitterTest
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
		public void ConvertJpgToTiffs_Valid_ConvertJpgToTiffs()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.GREYSCALE_BPPDITHER, FreeImageCompressionEnum.FAX_LZW, false);

			var jpgContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "JPGs", "23311100000021_1.jpg")));
			ConvertJpgsToTiffs convertJpgToTIffs = new ConvertJpgsToTiffs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertJpgToTIffs.StartConvertJpgsToTiffs(jpgContent, 0);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("TIFFs", $"JPGtoTIFFs_test01_{imageIndex}.tiff", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertJpgsToTiffs_Valid_ConvertJpgToTiffs()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.GREYSCALE_BPPDITHER, FreeImageCompressionEnum.FAX_LZW, false);

			var jpgContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "JPGs", "23311100000021_1.jpg")));
			var jpgContent2 = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "JPGs", "23311100000021_3.jpg")));
			var jpgContent3 = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "JPGs", "23311100000021_7.jpg")));
			ConvertJpgsToTiffs convertJpgToTIffs = new ConvertJpgsToTiffs(specificImageSplitterConverterOption);

			IList<Stream> jpgContentFiles = new List<Stream>();
			jpgContentFiles.Add(jpgContent);
			jpgContentFiles.Add(jpgContent2);
			jpgContentFiles.Add(jpgContent3);

			IEnumerable<string> filesPath = convertJpgToTIffs.StartConvertJpgsToTiffs(jpgContentFiles, 0);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("TIFFs", $"JPGtoTIFFs_test02_{imageIndex}.tiff", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertJpgToTiffs_Valid_ConvertJpgToTiffsWithPartitionMode1_ColorBits24_ColorDepthGreyScale_CompressionFaxLZW()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.GREYSCALE_BPPDITHER, FreeImageCompressionEnum.FAX_LZW, false);

			var jpgContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "JPGs", "23311100000021_1.jpg")));
			ConvertJpgsToTiffs convertJpgToTIffs = new ConvertJpgsToTiffs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertJpgToTIffs.StartConvertJpgsToTiffs(jpgContent, 1);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("TIFFs", $"JPGtoTIFFs_test03_{imageIndex}.tiff", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertJpgToTiffs_Valid_ConvertJpgToTiffsWithPartitionMode1_ColorBits16_ColorDepthNone_CompressionFaxNone()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS16, FreeImageColorDepthEnum.NONE, FreeImageCompressionEnum.NONE, false);

			var jpgContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "JPGs", "23311100000021_1.jpg")));
			ConvertJpgsToTiffs convertJpgToTIffs = new ConvertJpgsToTiffs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertJpgToTIffs.StartConvertJpgsToTiffs(jpgContent, 1);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("TIFFs", $"JPGtoTIFFs_test04_{imageIndex}.tiff", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertJpgToTiffs_Valid_ConvertJpgToTiffsWithPartitionMode1_ColorBits8_ColorDepthNone_CompressionFaxNone()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS8, FreeImageColorDepthEnum.NONE, FreeImageCompressionEnum.NONE, false);

			var jpgContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "JPGs", "23311100000021_1.jpg")));
			ConvertJpgsToTiffs convertJpgToTIffs = new ConvertJpgsToTiffs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertJpgToTIffs.StartConvertJpgsToTiffs(jpgContent, 1);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("TIFFs", $"JPGtoTIFFs_test05_{imageIndex}.tiff", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertJpgToTiffs_Valid_ConvertJpgToTiffsWithPartitionMode1_ColorBitsGreyScale_ColorDepthNone_CompressionFaxNone()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.GREYSCALE, FreeImageColorDepthEnum.NONE, FreeImageCompressionEnum.NONE, false);

			var jpgContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "JPGs", "23311100000021_1.jpg")));
			ConvertJpgsToTiffs convertJpgToTIffs = new ConvertJpgsToTiffs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertJpgToTIffs.StartConvertJpgsToTiffs(jpgContent, 1);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("TIFFs", $"JPGtoTIFFs_test06_{imageIndex}.tiff", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

	}
}
