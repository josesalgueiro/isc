namespace SIBS.ISC.Tests
{
	using System.Collections.Generic;
	using FluentAssertions;
	using SIBS.ISC.Abstractions;
	using SIBS.ISC.Domain.Convertions;
	using SIBS.ISC.Options;
	using Xunit;

	public class ConvertPdfToJpgsTests : BaseSplitterTest
	{
		private List<(string bits, bool res)> IsValidResults { get; set; } = new List<(string bits, bool res)>();

		private SpecificImageSplitterConverterOption ConverterOptionJpg(int productType, /*int partitionMode,*/
			int jpgMaxWidth, int jpgMaxHeight, uint jpgWidthDPI, uint jpgHeightDPI,
			PDFiumRenderFlagEnum jpgPDFiumRenderFlag, FreeImageQualityEnum jpgFreeImageQuality, FreeImageConverToColorBitsEnum jpgFreeImageConverToColorBits)
		{
			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = new SpecificImageSplitterConverterOption
			{
				ProductType = productType,
				//PageSheetPartitionMode = partitionMode,
				JpgMaxWidth = jpgMaxWidth,
				JpgMaxHeight = jpgMaxHeight,
				JpgWidthDPI = jpgWidthDPI,
				JpgHeightDPI = jpgHeightDPI,
				JpgPDFiumRenderFlag = jpgPDFiumRenderFlag,
				JpgFreeImageQuality = jpgFreeImageQuality,
				JpgFreeImageConverToColorBits = jpgFreeImageConverToColorBits
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

				SaveImages("JPGs", $"Image_{imageNameAux}_{imageIndex}.jpg", filePath);

				imageIndex++;
			}

			return isValid;
		}

		[Fact]
		public void ConvertPdfToJpgs_Valid_ConvertToPdfToJPGsExceptionImage()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionJpg(99, 816, 1154, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.NORMAL, FreeImageConverToColorBitsEnum.BITS24);

			var pdfContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "exception.pdf")));
			ConvertPdfToJpgs convertPdfToJpgs = new ConvertPdfToJpgs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertPdfToJpgs.StartConvertPdfToJpgs(pdfContent, 0);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("JPGs", $"Image_Exception_{imageIndex}.jpg", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertPdfToJpgs_Valid_ConvertToPdfToJPGsWidthAllPageSheetPartitionMode()
		{
			bool isValid = true;

			//PAGEPartitionMode
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionPAGEPartitionMode = ConverterOptionJpg(99, 816, 1154, 300, 300,
					PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.NORMAL, FreeImageConverToColorBitsEnum.BITS24);

			var pdfContentPAGEPartitionMode = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToJpgs convertPdfToJpgsPAGEPartitionMode = new ConvertPdfToJpgs(specificImageSplitterConverterOptionPAGEPartitionMode);
			IEnumerable<string> filesPathPAGEPartitionMode = convertPdfToJpgsPAGEPartitionMode.StartConvertPdfToJpgs(pdfContentPAGEPartitionMode, 1);

			IsValidResults.Add(("PAGEPartitionMode", SaveImages(filesPathPAGEPartitionMode, "PAGEPartitionMode")));

			//SHEETPartitionMode
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionSHEETPartitionMode = ConverterOptionJpg(99, 816, 1154, 300, 300,
					PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.NORMAL, FreeImageConverToColorBitsEnum.BITS24);

			var pdfContentSHEETPartitionMode = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToJpgs convertPdfToJpgsSHEETPartitionMode = new ConvertPdfToJpgs(specificImageSplitterConverterOptionSHEETPartitionMode);
			IEnumerable<string> filesPathSHEETPartitionMode = convertPdfToJpgsSHEETPartitionMode.StartConvertPdfToJpgs(pdfContentSHEETPartitionMode, 0);

			IsValidResults.Add(("SHEETPartitionMode", SaveImages(filesPathSHEETPartitionMode, "SHEETPartitionMode")));

			var isValidResults = IsValidResults.Any(r => r.res == false);

			if (isValidResults is true)
			{
				isValid = false;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertPdfToJpgs_Valid_ConvertToPdfToJPGsWithPageWidthMoreThan4000()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionJpg(99, 816, 1154, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.NORMAL, FreeImageConverToColorBitsEnum.BITS24);

			var pdfContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "envelope.pdf")));
			ConvertPdfToJpgs convertPdfToJpgs = new ConvertPdfToJpgs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertPdfToJpgs.StartConvertPdfToJpgs(pdfContent, 0);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("JPGs", $"Image_PageWidthMoreThan4000_{imageIndex}.jpg", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertPdfToJpgs_Valid_ConvertToPdfToJPGsWithPageHeightMoreThan4000()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionJpg(99, 816, 1154, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.NORMAL, FreeImageConverToColorBitsEnum.BITS24);

			var pdfContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "paginaROLO.pdf")));
			ConvertPdfToJpgs convertPdfToJpgs = new ConvertPdfToJpgs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertPdfToJpgs.StartConvertPdfToJpgs(pdfContent, 0);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("JPGs", $"Image_PageHeightMoreThan4000_{imageIndex}.jpg", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertPdfToJpgs_Valid_ConvertToPdfToJPGsWithAllFreeImageConverToColorBitsOptions()
		{
			bool isValid = true;

			//BITS24
			SpecificImageSplitterConverterOption specificImageSplitterConverterOption24 = ConverterOptionJpg(99, 816, 1154, 300, 300,
					PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.NORMAL, FreeImageConverToColorBitsEnum.BITS24);

			var pdfContent24 = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToJpgs convertPdfToJpgs24 = new ConvertPdfToJpgs(specificImageSplitterConverterOption24);
			IEnumerable<string> filesPath24 = convertPdfToJpgs24.StartConvertPdfToJpgs(pdfContent24, 0);

			IsValidResults.Add(("BITS24", SaveImages(filesPath24, "FreeImageConverToColorBitsBITS24")));

			//BITS16
			SpecificImageSplitterConverterOption specificImageSplitterConverterOption16 = ConverterOptionJpg(99, 816, 1154, 300, 300,
					PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.NORMAL, FreeImageConverToColorBitsEnum.BITS16);

			var pdfContent16 = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToJpgs convertPdfToJpgs16 = new ConvertPdfToJpgs(specificImageSplitterConverterOption16);
			IEnumerable<string> filesPath16 = convertPdfToJpgs16.StartConvertPdfToJpgs(pdfContent16, 0);

			IsValidResults.Add(("BITS16", SaveImages(filesPath16, "FreeImageConverToColorBitsBITS16")));

			//BITS8
			SpecificImageSplitterConverterOption specificImageSplitterConverterOption8 = ConverterOptionJpg(99, 816, 1154, 300, 300,
					PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.NORMAL, FreeImageConverToColorBitsEnum.BITS8);

			var pdfContent8 = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToJpgs convertPdfToJpgs8 = new ConvertPdfToJpgs(specificImageSplitterConverterOption8);
			IEnumerable<string> filesPath8 = convertPdfToJpgs8.StartConvertPdfToJpgs(pdfContent8, 0);

			IsValidResults.Add(("BITS8", SaveImages(filesPath8, "FreeImageConverToColorBitsBITS8")));

			//GREYSCALE
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionGREYSCALE = ConverterOptionJpg(99, 816, 1154, 300, 300,
					PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.NORMAL, FreeImageConverToColorBitsEnum.GREYSCALE);

			var pdfContentGREYSCALE = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToJpgs convertPdfToJpgsGREYSCALE = new ConvertPdfToJpgs(specificImageSplitterConverterOptionGREYSCALE);
			IEnumerable<string> filesPathGREYSCALE = convertPdfToJpgsGREYSCALE.StartConvertPdfToJpgs(pdfContentGREYSCALE, 0);

			IsValidResults.Add(("GREYSCALE", SaveImages(filesPathGREYSCALE, "FreeImageConverToColorBitsGREYSCALE")));

			var isValidResults = IsValidResults.Any(r => r.res == false);

			if (isValidResults is true)
			{
				isValid = false;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertPdfToJpgs_Valid_ConvertToPdfToJPGsWithAllFreeImageQualityOptions()
		{
			bool isValid = true;

			//SUPERB
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionSUPERB = ConverterOptionJpg(99, 816, 1154, 300, 300,
					PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.SUPERB, FreeImageConverToColorBitsEnum.BITS24);

			var pdfContentSUPERB = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToJpgs convertPdfToJpgsSUPERB = new ConvertPdfToJpgs(specificImageSplitterConverterOptionSUPERB);
			IEnumerable<string> filesPathSUPERB = convertPdfToJpgsSUPERB.StartConvertPdfToJpgs(pdfContentSUPERB, 0);

			IsValidResults.Add(("SUPERB", SaveImages(filesPathSUPERB, "FreeImageQualitySUPERB")));

			//GOOD
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionGOOD = ConverterOptionJpg(99, 816, 1154, 300, 300,
					PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.GOOD, FreeImageConverToColorBitsEnum.BITS24);

			var pdfContentGOOD = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToJpgs convertPdfToJpgsGOOD = new ConvertPdfToJpgs(specificImageSplitterConverterOptionGOOD);
			IEnumerable<string> filesPathGOOD = convertPdfToJpgsGOOD.StartConvertPdfToJpgs(pdfContentGOOD, 0);

			IsValidResults.Add(("GOOD", SaveImages(filesPathGOOD, "FreeImageQualityGOOD")));

			//NORMAL
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionNORMAL = ConverterOptionJpg(99, 816, 1154, 300, 300,
					PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.NORMAL, FreeImageConverToColorBitsEnum.BITS24);

			var pdfContentNORMAL = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToJpgs convertPdfToJpgsNORMAL = new ConvertPdfToJpgs(specificImageSplitterConverterOptionNORMAL);
			IEnumerable<string> filesPathNORMAL = convertPdfToJpgsNORMAL.StartConvertPdfToJpgs(pdfContentNORMAL, 0);

			IsValidResults.Add(("NORMAL", SaveImages(filesPathNORMAL, "FreeImageQualityNORMAL")));

			//AVERAGE
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionAVERAGE = ConverterOptionJpg(99, 816, 1154, 300, 300,
					PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.AVERAGE, FreeImageConverToColorBitsEnum.BITS24);

			var pdfContentAVERAGE = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToJpgs convertPdfToJpgsAVERAGE = new ConvertPdfToJpgs(specificImageSplitterConverterOptionAVERAGE);
			IEnumerable<string> filesPathAVERAGE = convertPdfToJpgsAVERAGE.StartConvertPdfToJpgs(pdfContentAVERAGE, 0);

			IsValidResults.Add(("AVERAGE", SaveImages(filesPathAVERAGE, "FreeImageQualityAVERAGE")));

			//BAD
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionBAD = ConverterOptionJpg(99, 816, 1154, 300, 300,
					PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.BAD, FreeImageConverToColorBitsEnum.BITS24);

			var pdfContentBAD = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToJpgs convertPdfToJpgsBAD = new ConvertPdfToJpgs(specificImageSplitterConverterOptionBAD);
			IEnumerable<string> filesPathBAD = convertPdfToJpgsBAD.StartConvertPdfToJpgs(pdfContentBAD, 0);

			IsValidResults.Add(("BAD", SaveImages(filesPathBAD, "FreeImageQualityBAD")));

			var isValidResults = IsValidResults.Any(r => r.res == false);

			if (isValidResults is true)
			{
				isValid = false;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertPdfToJpgs_Valid_ConvertToPdfToJPGsWithAllPDFiumRenderFlagOptions()
		{
			bool isValid = true;

			//NONE
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionNONE = ConverterOptionJpg(99, 816, 1154, 300, 300,
					PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.NORMAL, FreeImageConverToColorBitsEnum.BITS24);

			var pdfContentNONE = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToJpgs convertPdfToJpgsNONE = new ConvertPdfToJpgs(specificImageSplitterConverterOptionNONE);
			IEnumerable<string> filesPathNONE = convertPdfToJpgsNONE.StartConvertPdfToJpgs(pdfContentNONE, 0);

			IsValidResults.Add(("NONE", SaveImages(filesPathNONE, "PDFiumRenderFlagNONE")));

			//GRAYSCALE
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionGRAYSCALE = ConverterOptionJpg(99, 816, 1154, 300, 300,
					PDFiumRenderFlagEnum.GRAYSCALE, FreeImageQualityEnum.NORMAL, FreeImageConverToColorBitsEnum.BITS24);

			var pdfContentGRAYSCALE = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToJpgs convertPdfToJpgsGRAYSCALE = new ConvertPdfToJpgs(specificImageSplitterConverterOptionGRAYSCALE);
			IEnumerable<string> filesPathGRAYSCALE = convertPdfToJpgsGRAYSCALE.StartConvertPdfToJpgs(pdfContentGRAYSCALE, 0);

			IsValidResults.Add(("GRAYSCALE", SaveImages(filesPathGRAYSCALE, "PDFiumRenderFlagGRAYSCALE")));

			//FORCEHALFTONE
			SpecificImageSplitterConverterOption specificImageSplitterConverterOptionFORCEHALFTONE = ConverterOptionJpg(99, 816, 1154, 300, 300,
					PDFiumRenderFlagEnum.FORCEHALFTONE, FreeImageQualityEnum.NORMAL, FreeImageConverToColorBitsEnum.BITS24);

			var pdfContentFORCEHALFTONE = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			ConvertPdfToJpgs convertPdfToJpgsFORCEHALFTONE = new ConvertPdfToJpgs(specificImageSplitterConverterOptionFORCEHALFTONE);
			IEnumerable<string> filesPathFORCEHALFTONE = convertPdfToJpgsFORCEHALFTONE.StartConvertPdfToJpgs(pdfContentFORCEHALFTONE, 0);

			IsValidResults.Add(("FORCEHALFTONE", SaveImages(filesPathFORCEHALFTONE, "PDFiumRenderFlagFORCEHALFTONE")));

			var isValidResults = IsValidResults.Any(r => r.res == false);

			if (isValidResults is true)
			{
				isValid = false;
			}

			isValid.Should().BeTrue();
		}

	}
}
