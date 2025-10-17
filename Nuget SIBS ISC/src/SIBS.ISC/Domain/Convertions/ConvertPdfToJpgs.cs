namespace SIBS.ISC.Domain.Convertions
{
	using System;
	using System.Collections.Generic;
	using System.Drawing.Imaging;
	using System.Drawing;
	using PdfiumViewer;
	using SIBS.ISC.Exceptions;
	using SIBS.ISC.Helpers;
	using FreeImageAPI;
	using SIBS.ISC.Options;
	using System.IO;

	public class ConvertPdfToJpgs : BaseConvert
	{
		protected string _jpgFreeImageQuality;
		public ConvertPdfToJpgs(SpecificImageSplitterConverterOption specificImageSplitterConverterOption)
			: base(specificImageSplitterConverterOption, "Jpg")
		{
			_jpgFreeImageQuality = specificImageSplitterConverterOption.JpgFreeImageQuality.ToString();
		}

		public IEnumerable<string> StartConvertPdfToJpgs(Stream originalFile, int partitionMode)
		{
			if (originalFile is null)
				throw new ImageSplitterConverterException("The originalFile is null.");

			if (!Enumerable.Range(0, 2).Contains(partitionMode))
				throw new ImageSplitterConverterException("The partition mode value not exists.");

			int documentPageCount = 0;
			int tiffDocument = 1;
			int pdfCurrentPage = 1;
			string tempDataFolder = Path.GetTempPath();
			string outputName = Path.Combine(tempDataFolder, Guid.NewGuid().ToString());

			using (PdfDocument pdfDocument = ReadPdfDocument(originalFile))
			{
				documentPageCount = pdfDocument.PageCount;

				if (partitionMode == 0)
				{
					for (int documentPage = 0; documentPage < documentPageCount; documentPage++)
					{
						ImageAdjust(pdfDocument, documentPage, outputName, tiffDocument, pdfCurrentPage);
						pdfCurrentPage++;

						if (documentPage + 1 < documentPageCount)
						{
							ImageAdjust(pdfDocument, documentPage + 1, outputName, tiffDocument, pdfCurrentPage);
							pdfCurrentPage++;
						}

						tiffDocument++;
						documentPage++;
					}

					if (documentPageCount % 2 != 0)
					{
						tiffDocument--;
						SaveBlankJpg(outputName, tiffDocument);
					}
				}

				if (partitionMode == 1)
				{
					for (int documentPage = 0; documentPage < documentPageCount; documentPage++)
					{
						ImageAdjust(pdfDocument, documentPage, outputName, tiffDocument, pdfCurrentPage);
						pdfCurrentPage++;

						SaveBlankJpg(outputName, tiffDocument);
						pdfCurrentPage++;
						tiffDocument++;
					}
				}
			}

			return _generatedFiles;
		}

		private PdfDocument ReadPdfDocument(Stream originalFile)
		{
			try
			{
				return PdfDocument.Load(originalFile);
			}
			catch (Exception ex)
			{
				throw new ImageSplitterConverterException("Can't access to the document file! The document is not a PDF file or is protected by password.", ex);
			}
		}


		private void SaveJpg(Bitmap imageBitmap, string outputFileName)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				imageBitmap.Save(stream, ImageFormat.Bmp);
				stream.Seek(0, SeekOrigin.Begin);
				FreeImageAPI.FIBITMAP dib = FreeImageAPI.FreeImage.LoadFromStream(stream);
				FreeImageAPI.FIBITMAP freeImageConverted = FreeImageConvertToColorBits(dib);
				SetFreeImageDotsPerMeter(freeImageConverted);
				FreeImageAPI.FreeImage.Save(FreeImageAPI.FREE_IMAGE_FORMAT.FIF_JPEG, freeImageConverted, outputFileName, FreeImageJpgQuality(_jpgFreeImageQuality));
				FreeImageAPI.FreeImage.UnloadEx(ref dib);
				FreeImage.Unload(dib);
				FreeImage.Unload(freeImageConverted);
			}
		}

		private void SaveBlankJpg(string outputName, int tiffDoc)
		{
			using Bitmap img = new Bitmap(_maxWidth, _maxHeight);
			using (Graphics graph = Graphics.FromImage(img))
			{
				Rectangle ImageSize = new Rectangle(0, 0, _maxWidth, _maxHeight);
				graph.FillRectangle(Brushes.White, ImageSize);
			}

			string outputFileName = $"{outputName}_{tiffDoc}_2";
			SaveJpg(img, outputFileName);
			_generatedFiles.Add(outputFileName);
		}

		private void ImageAdjust(PdfDocument pdfDocument, int documentPage, string outputName, int tiffDocument, int pdfCurrentPage)
		{
			PdfRenderFlags pdfRenderFlags = PdfiumRenderFlags();
			IList<SizeF> docPageSizes = pdfDocument.PageSizes;
			Bitmap? imgBitmap = null;

			try
			{
				if (docPageSizes[documentPage].Width > 4000)
				{
					int newWidthPag = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, 14339, _maxHeight).Item1;
					int newHeightPag = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, 14339, _maxHeight).Item2;
					using Image image = pdfDocument.Render(documentPage, _widthDPI, _heightDPI, pdfRenderFlags);
					imgBitmap = new Bitmap(image, newWidthPag, newHeightPag);
				}
				else if (docPageSizes[documentPage].Height > 4000)
				{
					int newWidthPag = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, _maxWidth, 14339).Item1;
					int newHeightPag = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, _maxWidth, 14339).Item2;
					using Image image = pdfDocument.Render(documentPage, _widthDPI, _heightDPI, pdfRenderFlags);
					imgBitmap = new Bitmap(image, newWidthPag, newHeightPag);
				}
				else
				{
					int newWidthPag = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, _maxWidth, _maxHeight).Item1;
					int newHeightPag = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, _maxWidth, _maxHeight).Item2;
					using Image image = pdfDocument.Render(documentPage, _widthDPI, _heightDPI, pdfRenderFlags);
					imgBitmap = new Bitmap(image, newWidthPag, newHeightPag);
				}
			}
			catch (Exception ex)
			{
				int newWidthPag = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, _maxWidth, _maxHeight).Item1;
				int newHeightPag = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, _maxWidth, _maxHeight).Item2;
				using Image image = pdfDocument.Render(documentPage, newWidthPag, newHeightPag, _widthDPI, _heightDPI, pdfRenderFlags);
				imgBitmap = new Bitmap(image, newWidthPag, newHeightPag);
			}

			if (pdfCurrentPage % 2 != 0) // page odd
			{
				string outputFileName = $"{outputName}_{tiffDocument}_1";

				SaveJpg(imgBitmap, outputFileName);

				_generatedFiles.Add(outputFileName);
				FileUtils.GarbageCollector(imgBitmap);
			}
			else //page even
			{
				string outputFileName = $"{outputName}_{tiffDocument}_2";
				SaveJpg(imgBitmap, outputFileName);
				_generatedFiles.Add(outputFileName);
				FileUtils.GarbageCollector(imgBitmap);
			}
		}


	}
}
