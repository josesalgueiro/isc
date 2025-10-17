namespace SIBS.ISC.Tests.ConsoleApplication
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using SIBS.ISC.Exceptions;

	public class IscSplitter
	{
		public void Run()
		{
			bool hasThrowException = false;

			try
			{
				for (int i = 0; i < 1000000; i++)
				{
					var startDate = DateTime.Now;
					Console.WriteLine($"Index of the FOR: '{i}'");

					var fullFilePath = Path.Combine(AppContext.BaseDirectory, "pdf_4k_10pages.pdf");

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
						var resultingTiffs = splitter.ConvertPdfToTiffs(fs, (int)1/*ConverterPartitionModeEnum.Sheet*/).ToList();
					}

					using (var fs = File.OpenRead(fullFilePath))
					{
						var resultingJpgs = splitter.ConvertPdfToJpgs(fs, (int)1/*ConverterPartitionModeEnum.Sheet*/).ToList();
					}

					Console.WriteLine($"Time to generate the TIFFs and JPGs: '{(DateTime.Now - startDate)}'");

				}
			}
			catch (ShouldNotHappenIscException)
			{
				hasThrowException = true;
			}
			catch (ImageSplitterConverterException e)
			{
				hasThrowException = true;
			}
		}
		public void RunSingle()
		{
			bool hasThrowException = false;

			try
			{
				List<string> resultJpgs = new List<string>();
				List<string> resultTiff = new List<string>();

				var startDate = DateTime.Now;
				//Console.WriteLine($"Index of the FOR: '{i}'");

				var fullFilePath = Path.Combine(AppContext.BaseDirectory, "pdf_4k_10pages.pdf");

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
					var resultingTiffs = splitter.ConvertPdfToTiffs(fs, (int)1/*ConverterPartitionModeEnum.Sheet*/).ToList();
					resultTiff.AddRange(resultingTiffs);

					//Console.WriteLine($"Number of images TIFFs generated: '{resultTiff.Count}'");
				}

				using (var fs = File.OpenRead(fullFilePath))
				{
					var resultingJpgs = splitter.ConvertPdfToJpgs(fs, (int)1/*ConverterPartitionModeEnum.Sheet*/).ToList();
					resultJpgs.AddRange(resultingJpgs);

					//Console.WriteLine($"Number of images JPGs generated: '{resultJpgs.Count}'");
				}

				//Console.WriteLine($"Time to generate the TIFFs and JPGs: '{(DateTime.Now - startDate)}'");


				var count = resultJpgs.Count;
			}
			catch (ShouldNotHappenIscException)
			{
				hasThrowException = true;
			}
			catch (ImageSplitterConverterException e)
			{
				hasThrowException = true;
			}
		}
	}
}
