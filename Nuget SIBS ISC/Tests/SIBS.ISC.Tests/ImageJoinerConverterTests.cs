namespace SIBS.ISC.Tests
{
	using FluentAssertions;
	using SIBS.ISC.Abstractions;
	using SIBS.ISC.Domain.Factories;
	using SIBS.ISC.Helpers;
	using Xunit;

	public class ImageJoinerConverterTests : BaseJoinerTest
	{
		[Fact]
		public void ImageJoinerConverter_Fail_CreatePDFsFromJPGsOneFileInvalidPath2Arguments()
		{
			bool isValid = true;
			IImageJoinerConverterFactory imageJoinerConverterFactory = new ImageJoinerConverterFactory();

			var imageJoinerConverter = imageJoinerConverterFactory.CreateImageJoinerConverter(base.GetImageConverterOption());

			var pdfFile = imageJoinerConverter.ConvertImageToPDF("InvalidPath", "23311100000021_1.jpg");
			if (pdfFile is null)
				isValid = false;

			isValid.Should().BeFalse();
		}

		[Theory]
		[InlineData("GIFs", "cloud.gif")]
		[InlineData("JPGs", "23311100000021_1.jpg")]
		[InlineData("PNGs", "iText_1.png")]
		[InlineData("TIFs", "23311100000021_1.tif")]
		public void ImageJoinerConverter_Valid_CreatePDFsFromJPGsOneFile2Arguments(string path, string fileName)
		{
			bool isValid = true;
			IImageJoinerConverterFactory imageJoinerConverterFactory = new ImageJoinerConverterFactory();

			var imageJoinerConverter = imageJoinerConverterFactory.CreateImageJoinerConverter(base.GetImageConverterOption());
			var pdfFile = imageJoinerConverter.ConvertImageToPDF(Path.Combine(GetBasePathImages(), path), fileName);
			if (pdfFile is null)
			{
				isValid = false;
			}
			else
			{
				var savePDFsFiles = Path.Combine(GetBasePathImages(), "Tests", "PDFs");
				CreateDirectory(savePDFsFiles);

				SaveFile(savePDFsFiles, $"pdf_One_{path}_2.pdf", pdfFile);
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ImageJoinerConverter_Fail_CreatePDFsFromJPGsOneFileInvalidPath1Arguments()
		{
			bool isValid = true;
			IImageJoinerConverterFactory imageJoinerConverterFactory = new ImageJoinerConverterFactory();

			var imageJoinerConverter = imageJoinerConverterFactory.CreateImageJoinerConverter(base.GetImageConverterOption());

			var imagePathFullName = Path.Combine(GetBasePathImages(), "InvalidPath", "23311100000021_1.jpg");

			var pdfFile = imageJoinerConverter.ConvertImageToPDF(imagePathFullName);
			if (pdfFile is null)
				isValid = false;

			isValid.Should().BeFalse();
		}

		[Theory]
		[InlineData("JPGs", "23311100000021_1.jpg")]
		[InlineData("PNGs", "iText_1.png")]
		[InlineData("TIFs", "23311100000021_1.tif")]
		public void ImageJoinerConverter_Valid_CreatePDFsFromJPGsOneFile1Arguments(string path, string fileName)
		{
			bool isValid = true;
			IImageJoinerConverterFactory imageJoinerConverterFactory = new ImageJoinerConverterFactory();

			var imagePathFullName = Path.Combine(GetBasePathImages(), path, fileName);

			if (File.Exists(imagePathFullName) == false)
				isValid = false;

			var imageJoinerConverter = imageJoinerConverterFactory.CreateImageJoinerConverter(base.GetImageConverterOption());
			var pdfFile = imageJoinerConverter.ConvertImageToPDF(imagePathFullName);
			if (pdfFile is null)
			{
				isValid = false;
			}
			else
			{
				var savePDFsFiles = Path.Combine(GetBasePathImages(), "Tests", "PDFs");
				CreateDirectory(savePDFsFiles);

				SaveFile(savePDFsFiles, $"pdf_One_{path}_1.pdf", pdfFile);
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ImageJoinerConverter_Fail_CreatePDFsFromJPGsTwoOrMoreFilesInvalidPath()
		{
			bool isValid = true;
			IImageJoinerConverterFactory imageJoinerConverterFactory = new ImageJoinerConverterFactory();

			var imageJoinerConverter = imageJoinerConverterFactory.CreateImageJoinerConverter(base.GetImageConverterOption());

			var imagePath = Path.Combine(GetBasePathImages(), "InvalidPath");
			var pdfFile = imageJoinerConverter.ConvertImagesToPDF(imagePath);
			if (pdfFile is null)
				isValid = false;

			isValid.Should().BeFalse();
		}

		[Theory]
		[InlineData("JPGs")]
		[InlineData("PNGs")]
		[InlineData("TIFs")]
		public void ImageJoinerConverter_Valid_CreatePDFsFromJPGsTwoOrMoreFiles(string path)
		{
			bool isValid = true;
			IImageJoinerConverterFactory imageJoinerConverterFactory = new ImageJoinerConverterFactory();

			var imagePath = Path.Combine(GetBasePathImages(), path);

			if (Directory.Exists(imagePath) == false)
				isValid = false;

			var imageJoinerConverter = imageJoinerConverterFactory.CreateImageJoinerConverter(base.GetImageConverterOption());
			var pdfFile = imageJoinerConverter.ConvertImagesToPDF(imagePath);
			if (pdfFile is null)
			{
				isValid = false;
			}
			else
			{
				var savePDFsFiles = Path.Combine(GetBasePathImages(), "Tests", "PDFs");
				CreateDirectory(savePDFsFiles);

				SaveFile(savePDFsFiles, $"pdf_TwoOrMore_{path}.pdf", pdfFile);
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ImageJoinerConverter_Fail_CreatePDFsFromJPGsOneFile1ArgumentsByteArrayInvalidPath()
		{
			bool isValid = true;
			IImageJoinerConverterFactory imageJoinerConverterFactory = new ImageJoinerConverterFactory();

			var imagePathFullName = Path.Combine(GetBasePathImages(), "invalidPath", "file.jpg");

			if (File.Exists(imagePathFullName) == false)
				isValid = false;

			var file = ImageConverterHelper.ReadTargetFile(imagePathFullName);
			if (file is null)
			{
				isValid = false;
			}

			isValid.Should().BeFalse();
		}

		[Theory]
		[InlineData("JPGs", "23311100000021_1.jpg")]
		[InlineData("PNGs", "iText_1.png")]
		[InlineData("TIFs", "23311100000021_1.tif")]
		public void ImageJoinerConverter_Valid_CreatePDFsFromJPGsOneFile1ArgumentsByteArray(string path, string fileName)
		{
			bool isValid = true;
			IImageJoinerConverterFactory imageJoinerConverterFactory = new ImageJoinerConverterFactory();

			var imagePathFullName = Path.Combine(GetBasePathImages(), path, fileName);

			if (File.Exists(imagePathFullName) == false)
				isValid = false;

			var imageJoinerConverter = imageJoinerConverterFactory.CreateImageJoinerConverter(base.GetImageConverterOption());

			var file = ImageConverterHelper.ReadTargetFile(imagePathFullName);
			if (file is null)
			{
				isValid = false;
			}
			else
			{
				var pdfFile = imageJoinerConverter.ConvertImageToPDF(file);
				if (pdfFile is null)
				{
					isValid = false;
				}
				else
				{
					var savePDFsFiles = Path.Combine(GetBasePathImages(), "Tests", "PDFs");
					CreateDirectory(savePDFsFiles);

					SaveFile(savePDFsFiles, $"pdf_One_{path}_byteArray.pdf", pdfFile);
				}
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ImageJoinerConverter_Fail_CreatePDFsFromJPGsTwoOrMoreFilesByteArrayInvalidPath()
		{
			bool isValid = true;
			IImageJoinerConverterFactory imageJoinerConverterFactory = new ImageJoinerConverterFactory();

			var imagePath = Path.Combine(GetBasePathImages(), "InvalidPath");

			if (Directory.Exists(imagePath) == false)
				isValid = false;

			var imageJoinerConverter = imageJoinerConverterFactory.CreateImageJoinerConverter(base.GetImageConverterOption());
			var files = ImageConverterHelper.ReadTargetFiles(imagePath);
			if (files is null || files.Any() == false)
				isValid = false;

			isValid.Should().BeFalse();
		}

		[Theory]
		[InlineData("JPGs")]
		[InlineData("PNGs")]
		[InlineData("TIFs")]
		public void ImageJoinerConverter_Valid_CreatePDFsFromJPGsTwoOrMoreFilesByteArray(string path)
		{
			bool isValid = true;
			IImageJoinerConverterFactory imageJoinerConverterFactory = new ImageJoinerConverterFactory();

			var imagePath = Path.Combine(GetBasePathImages(), path);

			if (Directory.Exists(imagePath) == false)
				isValid = false;

			var imageJoinerConverter = imageJoinerConverterFactory.CreateImageJoinerConverter(base.GetImageConverterOption());
			var files = ImageConverterHelper.ReadTargetFiles(imagePath);
			if (files is null || files.Any() == false)
				isValid = false;
			else
			{
				var pdfFile = imageJoinerConverter.ConvertImagesToPDF(files);
				if (pdfFile is null)
				{
					isValid = false;
				}
				else
				{
					var savePDFsFiles = Path.Combine(GetBasePathImages(), "Tests", "PDFs");
					CreateDirectory(savePDFsFiles);

					SaveFile(savePDFsFiles, $"pdf_TwoOrMore_{path}_byteArray.pdf", pdfFile);
				}
			}

			isValid.Should().BeTrue();
		}
	}
}
