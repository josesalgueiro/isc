namespace SIBS.ISC.Tests
{
	using System.Collections.Generic;
	using FluentAssertions;
	using SIBS.ISC.Abstractions;
	using SIBS.ISC.Domain.Convertions;
	using SIBS.ISC.Domain.Factories;
	using SIBS.ISC.Exceptions;
	using SIBS.ISC.Options;
	using Xunit;

	public class SplitterStressTests : BaseSplitterTest
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

		[Fact]
		public void SplitterStressTests_Valid_PdfToJpg()
		{
			bool hasThrowException = false;
			int productType = 15;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			try
			{
				List<string> result = new List<string>();



				for (int i = 0; i < 10000; i++)
				{
					Console.WriteLine($"Idex of the FOR: '{i}'");

					IImageSplitterConverter joinerConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(productType, imageSplitterConverterConfigFileName: "iscConfig");

					using MemoryStream pdfContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
					result.AddRange(joinerConverter.ConvertPdfToJpgs(pdfContent, 0));
				}

				var count = result.Count;
			}
			catch (ShouldNotHappenIscException)
			{
				hasThrowException = true;
			}
			catch (ImageSplitterConverterException e)
			{
				hasThrowException = true;
			}

			// Get the json config file
			hasThrowException.Should().BeFalse();
		}

		[Fact]
		public void SplitterStressTests_Valid_HhsPdfToJpg()
		{
			bool hasThrowException = false;

			try
			{
				List<string> result = new List<string>();

				for (int i = 0; i < 10000; i++)
				{
					Console.WriteLine($"Index of the FOR: '{i}'");

					var fullFilePath = Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf");

					var factory = new SIBS.ISC.Domain.Factories.ImageSplitterConverterFactory();
					var splitter = factory.CreateImageSplitterConverter(new SIBS.ISC.Options.SpecificImageSplitterConverterOption
					{
						ProductType = 15,
						JpgMaxWidth = 816,
						JpgMaxHeight = 1154,
						JpgWidthDPI = 300,
						JpgHeightDPI = 300,
						JpgPDFiumRenderFlag = SIBS.ISC.Options.PDFiumRenderFlagEnum.NONE,
						JpgFreeImageQuality = SIBS.ISC.Options.FreeImageQualityEnum.SUPERB,
						JpgFreeImageConverToColorBits = SIBS.ISC.Options.FreeImageConverToColorBitsEnum.BITS24,
						TiffMaxWidth = 1080,
						TiffMaxHeight = 1920,
						TiffWidthDPI = 300,
						TiffHeightDPI = 300,
						TiffPdfiumRenderFlag = SIBS.ISC.Options.PDFiumRenderFlagEnum.NONE,
						TiffFreeImageColorDepth = SIBS.ISC.Options.FreeImageColorDepthEnum.GREYSCALE_BPPDITHER,
						TiffFreeImageConverToColorBits = SIBS.ISC.Options.FreeImageConverToColorBitsEnum.BITS24,
						TiffFreeImageCompression = SIBS.ISC.Options.FreeImageCompressionEnum.FAX_LZW,
						TiffIsUsingColorCorrect = false
					});

					using (var fs = File.OpenRead(fullFilePath))
					{
						var resultingJpgs = splitter.ConvertPdfToJpgs(fs, (int)1/*ConverterPartitionModeEnum.Sheet*/).ToList();
						result.AddRange(resultingJpgs);


						Console.WriteLine($"Number of paths: '{result.Count}'");
					}

				}

				var count = result.Count;
			}
			catch (ShouldNotHappenIscException)
			{
				hasThrowException = true;
			}
			catch (ImageSplitterConverterException e)
			{
				hasThrowException = true;
			}

			// Get the json config file
			hasThrowException.Should().BeFalse();
		}

	}
}
