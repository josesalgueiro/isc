namespace SIBS.ISC.TestsFramework
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using SIBS.ISC.Abstractions;
	using SIBS.ISC.Helpers;

	[TestClass]
	public class Tests
	{
		private IImageSplitterConverterFactory _imageSplitterConverterFactory;

		[TestMethod]
		public void TestMethod_Valid_PdfToJpgs()
		{
			bool isValid = true;

			var configFilePath = AppContext.BaseDirectory;

			_imageSplitterConverterFactory = new SIBS.ISC.Domain.Factories.ImageSplitterConverterFactory();

			var imageSplitterConverter2 = _imageSplitterConverterFactory.CreateImageSplitterConverter(2, "iscConfig", null);

			var pdfContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathPDFs(), "envelope.pdf")));

			IEnumerable<string> filesPath = imageSplitterConverter2.ConvertPdfToJpgs(pdfContent, 1);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("JPGs", $"PdfToJpgs_test01_{imageIndex}.jpg", filePath);

				imageIndex++;
			}

			if (!isValid)
			{
				new Exception("Someting wrong!");
			}

		}

		protected string GetBasePathPDFs()
		{
			return Path.Combine(AppContext.BaseDirectory, "Files", "PDFs");
		}

		protected void SaveImages(string outputFolder, string fileName, string filePathToSave)
		{
			var imageFile = ImageConverterHelper.ReadTargetFile(filePathToSave);
			if (imageFile is null)
				return;

			var savePDFsFiles = Path.Combine(GetBasePathImages(), "Tests", outputFolder);
			CreateDirectory(savePDFsFiles);

			SaveFile(savePDFsFiles, fileName, imageFile);
		}

		protected void CreateDirectory(string directoryPath)
		{
			if (string.IsNullOrWhiteSpace(directoryPath))
				return;

			if (Directory.Exists(directoryPath) == false)
				Directory.CreateDirectory(directoryPath);
		}

		protected string GetBasePathImages()
		{
			return Path.Combine(AppContext.BaseDirectory, "Images");
		}

		protected void SaveFile(string filePath, string fileName, byte[] file)
		{
			if (string.IsNullOrWhiteSpace(filePath))
				return;

			File.WriteAllBytes(Path.Combine(filePath, fileName), file);
		}
	}
}
