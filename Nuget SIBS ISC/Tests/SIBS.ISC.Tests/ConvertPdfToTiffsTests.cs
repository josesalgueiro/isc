namespace SIBS.ISC.Tests
{
	using System.Collections.Generic;
	using FluentAssertions;
	using SIBS.ISC.Domain.Convertions;
	using SIBS.ISC.Options;
	using Xunit;

	public class ConvertPdfToTiffsTests : BaseSplitterTest
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

		private bool SaveImages(IEnumerable<string> filesPath, string imageNameAux)
		{
			bool isValid = true;

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("TIFFs", $"Image_{imageNameAux}_{imageIndex}.tiff", filePath);

				imageIndex++;
			}

			return isValid;
		}

		[Fact]
		public void ConvertPdfToTiffs_Valid_ConvertToPdfToTiffsExceptionImage()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.GREYSCALE_BPPDITHER, FreeImageCompressionEnum.FAX_LZW, false);

			var pdfContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "exception.pdf")));
			ConvertPdfToTiffs convertPdfToTiffs = new ConvertPdfToTiffs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertPdfToTiffs.StartConvertPdfToTiffs(pdfContent, 0);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("TIFFs", $"Image_Exception_{imageIndex}.tiff", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}


		[Fact]
		public void ConvertPdfToJpgs_Valid_ConvertToPdfToTiffsWidthAllPageSheetPartitionMode()
		{
			bool isValid = true;

			//PAGEPartitionMode
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionPAGEPartitionMode = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.GREYSCALE_BPPDITHER, FreeImageCompressionEnum.FAX_LZW, false);

			var pdfContentPAGEPartitionMode = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToTiffs convertPdfToTiffsPAGEPartitionMode = new ConvertPdfToTiffs(specificImageSplitterConverterOptionPAGEPartitionMode);
			IEnumerable<string> filesPathPAGEPartitionMode = convertPdfToTiffsPAGEPartitionMode.StartConvertPdfToTiffs(pdfContentPAGEPartitionMode, 1);

			IsValidResults.Add(("PAGEPartitionMode", SaveImages(filesPathPAGEPartitionMode, "PAGEPartitionMode")));

			//SHEETPartitionMode
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionSHEETPartitionMode = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.GREYSCALE_BPPDITHER, FreeImageCompressionEnum.FAX_LZW, false);

			var pdfContentSHEETPartitionMode = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToTiffs convertPdfToTiffsSHEETPartitionMode = new ConvertPdfToTiffs(specificImageSplitterConverterOptionSHEETPartitionMode);
			IEnumerable<string> filesPathSHEETPartitionMode = convertPdfToTiffsSHEETPartitionMode.StartConvertPdfToTiffs(pdfContentSHEETPartitionMode, 0);

			IsValidResults.Add(("SHEETPartitionMode", SaveImages(filesPathSHEETPartitionMode, "SHEETPartitionMode")));

			var isValidResults = IsValidResults.Any(r => r.res == false);

			if (isValidResults is true)
			{
				isValid = false;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertPdfToTiffs_Valid_ConvertToPdfToTiffsWithPageWidthMoreThan4000()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.GREYSCALE_BPPDITHER, FreeImageCompressionEnum.FAX_LZW, false);

			var pdfContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "envelope.pdf")));
			ConvertPdfToTiffs convertPdfToTiffs = new ConvertPdfToTiffs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertPdfToTiffs.StartConvertPdfToTiffs(pdfContent, 0);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("TIFFs", $"Image_PageWidthMoreThan4000_{imageIndex}.tiff", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertPdfToTiffs_Valid_ConvertToPdfToTiffsWithPageHeightMoreThan4000()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.GREYSCALE_BPPDITHER, FreeImageCompressionEnum.FAX_LZW, false);

			var pdfContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "paginaROLO.pdf")));
			ConvertPdfToTiffs convertPdfToTiffs = new ConvertPdfToTiffs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertPdfToTiffs.StartConvertPdfToTiffs(pdfContent, 0);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("TIFFs", $"Image_PageHeightMoreThan4000_{imageIndex}.tiff", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		private List<(string bits, bool res)> IsValidResults { get; set; } = new List<(string bits, bool res)>();

		[Fact]
		public void ConvertPdfToTiffs_Valid_ConvertToPdfToTiffsWithAllPDFiumRenderFlagOptions()
		{
			bool isValid = true;

			//NONE
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionNONE = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.NONE, FreeImageCompressionEnum.NONE, false);

			var pdfContentNONE = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToTiffs convertPdfToTiffsNONE = new ConvertPdfToTiffs(specificImageSplitterConverterOptionNONE);
			IEnumerable<string> filesPathNONE = convertPdfToTiffsNONE.StartConvertPdfToTiffs(pdfContentNONE, 0);

			IsValidResults.Add(("NONE", SaveImages(filesPathNONE, "PDFiumRenderFlagNONE")));

			//GRAYSCALE
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionGRAYSCALE = ConverterOptionTiff(9, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.GRAYSCALE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.NONE, FreeImageCompressionEnum.NONE, false);

			var pdfContentGRAYSCALE = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToTiffs convertPdfToTiffsGRAYSCALE = new ConvertPdfToTiffs(specificImageSplitterConverterOptionGRAYSCALE);
			IEnumerable<string> filesPathGRAYSCALE = convertPdfToTiffsGRAYSCALE.StartConvertPdfToTiffs(pdfContentGRAYSCALE, 0);

			IsValidResults.Add(("GRAYSCALE", SaveImages(filesPathGRAYSCALE, "PDFiumRenderFlagGRAYSCALE")));

			//FORCEHALFTONE
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionFORCEHALFTONE = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.FORCEHALFTONE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.NONE, FreeImageCompressionEnum.NONE, false);

			var pdfContentFORCEHALFTONE = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToTiffs convertPdfToTiffsFORCEHALFTONE = new ConvertPdfToTiffs(specificImageSplitterConverterOptionFORCEHALFTONE);
			IEnumerable<string> filesPathFORCEHALFTONE = convertPdfToTiffsFORCEHALFTONE.StartConvertPdfToTiffs(pdfContentFORCEHALFTONE, 0);

			IsValidResults.Add(("FORCEHALFTONE", SaveImages(filesPathFORCEHALFTONE, "PDFiumRenderFlagFORCEHALFTONE")));


			var isValidResults = IsValidResults.Any(r => r.res == false);

			if (isValidResults is true)
			{
				isValid = false;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertPdfToTiffs_Valid_ConvertToPdfToTiffsWithAllFreeImageConverToColorBitsOptions()
		{
			bool isValid = true;

			//BITS24
			SpecificImageSplitterConverterOption specificImageSplitterConverterOption24 = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.NONE, FreeImageCompressionEnum.NONE, false);

			var pdfContent24 = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToTiffs convertPdfToTiffs24 = new ConvertPdfToTiffs(specificImageSplitterConverterOption24);
			IEnumerable<string> filesPath24 = convertPdfToTiffs24.StartConvertPdfToTiffs(pdfContent24, 0);

			IsValidResults.Add(("BITS24", SaveImages(filesPath24, "FreeImageConverToColorBitsBITS24")));

			//BITS16
			SpecificImageSplitterConverterOption specificImageSplitterConverterOption16 = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS16, FreeImageColorDepthEnum.NONE, FreeImageCompressionEnum.NONE, false);

			var pdfContent16 = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToTiffs convertPdfToTiffs16 = new ConvertPdfToTiffs(specificImageSplitterConverterOption16);
			IEnumerable<string> filesPath16 = convertPdfToTiffs16.StartConvertPdfToTiffs(pdfContent16, 0);

			IsValidResults.Add(("BITS16", SaveImages(filesPath16, "FreeImageConverToColorBitsBITS16")));

			//BITS8
			SpecificImageSplitterConverterOption specificImageSplitterConverterOption8 = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS8, FreeImageColorDepthEnum.NONE, FreeImageCompressionEnum.NONE, false);

			var pdfContent8 = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToTiffs convertPdfToTiffs8 = new ConvertPdfToTiffs(specificImageSplitterConverterOption8);
			IEnumerable<string> filesPath8 = convertPdfToTiffs8.StartConvertPdfToTiffs(pdfContent8, 0);

			IsValidResults.Add(("BITS8", SaveImages(filesPath8, "FreeImageConverToColorBitsBITS8")));

			//GREYSCALE
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionGREYSCALE = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.GREYSCALE, FreeImageColorDepthEnum.NONE, FreeImageCompressionEnum.NONE, false);

			var pdfContentGREYSCALE = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToTiffs convertPdfToTiffsGREYSCALE = new ConvertPdfToTiffs(specificImageSplitterConverterOptionGREYSCALE);
			IEnumerable<string> filesPathGREYSCALE = convertPdfToTiffsGREYSCALE.StartConvertPdfToTiffs(pdfContentGREYSCALE, 0);

			IsValidResults.Add(("GREYSCALE", SaveImages(filesPathGREYSCALE, "FreeImageConverToColorBitsGREYSCALE")));

			var isValidResults = IsValidResults.Any(r => r.res == false);

			if (isValidResults is true)
			{
				isValid = false;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertPdfToTiffs_Valid_ConvertToPdfToTiffsWithAllFreeImageColorDepthOptions()
		{
			bool isValid = true;

			//GREYSCALE_BPPDITHER
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionGREYSCALE_BPPDITHER = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.GREYSCALE_BPPDITHER, FreeImageCompressionEnum.NONE, false);

			var pdfContentGREYSCALE_BPPDITHER = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToTiffs convertPdfToTiffsGREYSCALE_BPPDITHER = new ConvertPdfToTiffs(specificImageSplitterConverterOptionGREYSCALE_BPPDITHER);
			IEnumerable<string> filesPathGREYSCALE_BPPDITHER = convertPdfToTiffsGREYSCALE_BPPDITHER.StartConvertPdfToTiffs(pdfContentGREYSCALE_BPPDITHER, 0);

			IsValidResults.Add(("GREYSCALE_BPPDITHER", SaveImages(filesPathGREYSCALE_BPPDITHER, "FreeImageColorDepthGREYSCALE_BPPDITHER")));

			//NONE
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionNONE = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.NONE, FreeImageCompressionEnum.NONE, false);

			var pdfContentNONE = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToTiffs convertPdfToTiffsNONE = new ConvertPdfToTiffs(specificImageSplitterConverterOptionNONE);
			IEnumerable<string> filesPathNONE = convertPdfToTiffsNONE.StartConvertPdfToTiffs(pdfContentNONE, 0);

			IsValidResults.Add(("NONE", SaveImages(filesPathNONE, "FreeImageColorDepthNONE")));


			var isValidResults = IsValidResults.Any(r => r.res == false);

			if (isValidResults is true)
			{
				isValid = false;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertPdfToTiffs_Valid_ConvertToPdfToTiffsWithAllFreeImageCompressionOptions()
		{
			bool isValid = true;

			//NONE
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionNONE = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.NONE, FreeImageCompressionEnum.NONE, false);

			var pdfContentNONE = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToTiffs convertPdfToTiffsNONE = new ConvertPdfToTiffs(specificImageSplitterConverterOptionNONE);
			IEnumerable<string> filesPathNONE = convertPdfToTiffsNONE.StartConvertPdfToTiffs(pdfContentNONE, 0);

			IsValidResults.Add(("NONE", SaveImages(filesPathNONE, "FreeImageCompressionNONE")));

			//NONE
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionFAX_LZW = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.NONE, FreeImageCompressionEnum.FAX_LZW, false);

			var pdfContentFAX_LZW = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToTiffs convertPdfToTiffsFAX_LZW = new ConvertPdfToTiffs(specificImageSplitterConverterOptionFAX_LZW);
			IEnumerable<string> filesPathFAX_LZW = convertPdfToTiffsFAX_LZW.StartConvertPdfToTiffs(pdfContentFAX_LZW, 0);

			IsValidResults.Add(("FAX_LZW", SaveImages(filesPathFAX_LZW, "FreeImageCompressionFAX_LZW")));


			var isValidResults = IsValidResults.Any(r => r.res == false);

			if (isValidResults is true)
			{
				isValid = false;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertPdfToTiffs_Valid_ConvertToPdfToTiffsWithAllUsingColorCorrectOptions()
		{
			bool isValid = true;

			//TRUE
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionTRUE = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.NONE, FreeImageCompressionEnum.NONE, true);

			var pdfContentTRUE = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToTiffs convertPdfToTiffsTRUE = new ConvertPdfToTiffs(specificImageSplitterConverterOptionTRUE);
			IEnumerable<string> filesPathTRUE = convertPdfToTiffsTRUE.StartConvertPdfToTiffs(pdfContentTRUE, 0);

			IsValidResults.Add(("TRUE", SaveImages(filesPathTRUE, "UsingColorCorrectTRUE")));

			//FALSE
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionFALSE = ConverterOptionTiff(99, 1080, 1920, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageConverToColorBitsEnum.BITS24, FreeImageColorDepthEnum.NONE, FreeImageCompressionEnum.NONE, false);

			var pdfContentFALSE = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToTiffs convertPdfToTiffsFALSE = new ConvertPdfToTiffs(specificImageSplitterConverterOptionFALSE);
			IEnumerable<string> filesPathFALSE = convertPdfToTiffsFALSE.StartConvertPdfToTiffs(pdfContentFALSE, 0);

			IsValidResults.Add(("FALSE", SaveImages(filesPathFALSE, "UsingColorCorrectFALSE")));


			var isValidResults = IsValidResults.Any(r => r.res == false);

			if (isValidResults is true)
			{
				isValid = false;
			}

			isValid.Should().BeTrue();
		}

	}
}
