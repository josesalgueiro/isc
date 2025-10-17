namespace SIBS.ISC.Tests
{
	using FluentAssertions;
	using SIBS.ISC.Abstractions;
	using SIBS.ISC.Domain.Factories;
	using SIBS.ISC.Exceptions;
	using Xunit;

	public class ImageSplitterConverterTests : BaseSplitterTest
	{
		[Fact]
		public void ImageSplitterConverter_Valid_ConvertPdfToJPGs()
		{
			bool isValid = true;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			var imageSplitterConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(GetSpecificImageConverterOption());

			var pdfContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			pdfContent.Position = 0;

			var filesPath = imageSplitterConverter.ConvertPdfToJpgs(pdfContent, 0);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
					break;
				}

				SaveImages("JPGs", $"Image_{imageIndex}.jpg", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ImageSplitterConverter_Valid_ConvertPdfToJPGsWithConfigFile()
		{
			bool isValid = true;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			var imageSplitterConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(2, "iscConfig" );

			var pdfContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			pdfContent.Position = 0;

			var filesPath = imageSplitterConverter.ConvertPdfToJpgs(pdfContent, 0);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
					break;
				}

				SaveImages("JPGs", $"Image_{imageIndex}.jpg", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ImageSplitterConverter_Valid_ConvertPdfToJPGsWithCQsConfigFile()
		{
			bool isValid = true;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			var imageSplitterConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(2, "iscConfig", "CQs");

			var pdfContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			pdfContent.Position = 0;

			var filesPath = imageSplitterConverter.ConvertPdfToJpgs(pdfContent, 0);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
					break;
				}

				SaveImages("JPGs", $"Image_{imageIndex}.jpg", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ImageSplitterConverter_Failed_ConvertPdfToJPGsInvalidFile()
		{
			bool isValid = true;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			var imageSplitterConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(GetSpecificImageConverterOption());

			try
			{
				var filesPath = imageSplitterConverter.ConvertPdfToJpgs(null!, 0);
			}
			catch (ImageSplitterConverterException)
			{
				isValid = false;
			}
			
			isValid.Should().BeFalse();
		}

		[Fact]
		public void ImageSplitterConverter_Valid_ConvertPdfToTIFFs()
		{
			bool isValid = true;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			var imageSplitterConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(GetSpecificImageConverterOption());

			var pdfContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			pdfContent.Position = 0;

			var filesPath = imageSplitterConverter.ConvertPdfToTiffs(pdfContent, 0);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
					break;
				}

				SaveImages("TIFFs", $"Image_{imageIndex}.tiff", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ImageSplitterConverter_Valid_ConvertPdfToTIFFsWithConfigFile()
		{
			bool isValid = true;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			var imageSplitterConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(2, "iscConfig");

			var pdfContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			pdfContent.Position = 0;

			var filesPath = imageSplitterConverter.ConvertPdfToTiffs(pdfContent, 0);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
					break;
				}

				SaveImages("TIFFs", $"Image_{imageIndex}.tiff", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ImageSplitterConverter_Valid_ConvertPdfToTIFFsWithCQsConfigFile()
		{
			bool isValid = true;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			var imageSplitterConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(2, "iscConfig", "CQs");

			var pdfContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			pdfContent.Position = 0;

			var filesPath = imageSplitterConverter.ConvertPdfToTiffs(pdfContent, 0);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
					break;
				}

				SaveImages("TIFFs", $"Image_{imageIndex}.tiff", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ImageSplitterConverter_Failed_ConvertPdfToTIFFsInvalidFile()
		{
			bool isValid = true;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			var imageSplitterConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(GetSpecificImageConverterOption());

			try
			{
				var filesPath = imageSplitterConverter.ConvertPdfToTiffs(null!, 0);
			}
			catch (ImageSplitterConverterException)
			{
				isValid = false;
			}

			isValid.Should().BeFalse();
		}

		[Fact]
		public void ImageSplitterConverter_Failed_InvalidConfigFile()
		{
			bool isValid = true;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			try
			{
				var imageSplitterConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(2, "XPTO_iscConfig");
			}
			catch (ShouldNotHappenIscException)
			{
				isValid = false;
			}

			isValid.Should().BeFalse();
		}

		[Fact]
		public void ImageSplitterConverter_Failed_InvalidRelativePathConfigFile()
		{
			bool isValid = true;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			try
			{
				var imageSplitterConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(2, "iscConfig", "XPTP_CQs");
			}
			catch (ImageSplitterConverterException)
			{
				isValid = false;
			}

			isValid.Should().BeFalse();
		}

		[Fact]
		public void ImageSplitterConverter_Failed_InvalidProductType()
		{
			bool isValid = true;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			try
			{
				var imageSplitterConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(999, "iscConfig");
			}
			catch (ImageSplitterConverterException)
			{
				isValid = false;
			}

			isValid.Should().BeFalse();
		}

		[Fact]
		public void ImageSplitterConverter_Failed_InvalidPdfFileName()
		{
			bool isValid = true;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			try
			{
				var pdfContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "XPTO_pdf_Example_1.pdf")));
				pdfContent.Position = 0;
			}
			catch
			{
				isValid = false;
				new ImageSplitterConverterException("Doesn't exist image splitter converter PDF file with name: XPTO_pdf_Example_1.pdf");
			}

			isValid.Should().BeFalse();
		}

		[Fact]
		public void ImageSplitterConverter_Failed_ConvertPdfToJpgsInvalidPdfFileType()
		{
			bool isValid = true;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			var imageSplitterConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(GetSpecificImageConverterOption());

			var pdfContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "Image_1.pdf")));
			pdfContent.Position = 0;

			IEnumerable<string> filesPath = new List<string>();
			
			try
			{
				filesPath = imageSplitterConverter.ConvertPdfToJpgs(pdfContent, 0);
			}
			catch (ImageSplitterConverterException)
			{
				isValid = false;
			}

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
					break;
				}

				SaveImages("JPGs", $"Image_{imageIndex}.jpg", filePath);

				imageIndex++;
			}

			isValid.Should().BeFalse();
		}

		[Fact]
		public void ImageSplitterConverter_Failed_ConvertPdfToTiffsInvalidPdfFileType()
		{
			bool isValid = true;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			var imageSplitterConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(GetSpecificImageConverterOption());

			var pdfContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "Image_1.pdf")));
			pdfContent.Position = 0;

			IEnumerable<string> filesPath = new List<string>();

			try
			{
				filesPath = imageSplitterConverter.ConvertPdfToTiffs(pdfContent, 0);
			}
			catch (ImageSplitterConverterException)
			{
				isValid = false;
			}

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
					break;
				}

				SaveImages("JPGs", $"Image_{imageIndex}.jpg", filePath);

				imageIndex++;
			}

			isValid.Should().BeFalse();
		}

		[Fact]
		public void ImageSplitterConverter_Failed_ConvertPdfToJPGs()
		{
			bool isValid = true;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			var imageSplitterConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(GetSpecificImageConverterOption());

			var pdfContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "pdf_Example_1.pdf")));
			pdfContent.Position = 0;

			var filesPath = imageSplitterConverter.ConvertPdfToJpgs(pdfContent, 0);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
					break;
				}

				SaveImages("JPGs", $"Image_{imageIndex}.jpg", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

	}
}
