namespace SIBS.ISC.Domain.Convertions
{
	using System;
	using System.Collections.Generic;
	using System.Drawing.Imaging;
	using System.Drawing;
	using PdfiumViewer;
	using SIBS.ISC.Helpers;
	using FreeImageAPI;
	using SIBS.ISC.Exceptions;
	using SIBS.ISC.Options;
	//using Org.BouncyCastle.Bcpg;

	public class ConvertPdfToTiffs : BaseConvert
	{
		protected string _tiffFreeImageColorDepth, _tiffFreeImageCompression;
		protected bool _tiffIsUsingColorCorrect;

		public ConvertPdfToTiffs(SpecificImageSplitterConverterOption specificImageSplitterConverterOption)
			: base(specificImageSplitterConverterOption, "Tiff")
		{
			_tiffFreeImageColorDepth = specificImageSplitterConverterOption.TiffFreeImageColorDepth.ToString();
			_tiffFreeImageCompression = specificImageSplitterConverterOption.TiffFreeImageCompression.ToString();
			_tiffIsUsingColorCorrect = specificImageSplitterConverterOption.TiffIsUsingColorCorrect;
		}

		public IEnumerable<string> StartConvertPdfToTiffs(Stream originalFile, int partitionMode)
		{
			if (originalFile is null)
				throw new ImageSplitterConverterException("The originalFile is null.");

			if (!Enumerable.Range(0, 2).Contains(partitionMode))
				throw new ImageSplitterConverterException("The partition mode value not exists.");

			var documentPageCount = 0;
			int tiffDoc = 1;
			string tempDataFolder = Path.GetTempPath();
			string outputName = Path.Combine(tempDataFolder, Guid.NewGuid().ToString());

			using (PdfDocument pdfDocument = ReadPdfDocument(originalFile))
			{
				documentPageCount = pdfDocument.PageCount;

				if (partitionMode == 0)
				{
					for (int documentPage = 0; documentPage < documentPageCount; documentPage++)
					{
						SaveTiffFolhaMode(pdfDocument, documentPage, outputName, tiffDoc, documentPageCount);
						documentPage++;
						tiffDoc++;
					}
				}

				if (partitionMode == 1)
				{
					for (int documentPage = 0; documentPage < documentPageCount; documentPage++)
					{
						SaveTiffPaginaMode(pdfDocument, documentPage, outputName, tiffDoc);
						tiffDoc++;
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

		private void SaveTiffPaginaMode(PdfDocument pdfDocument, int documentPage, string outputName, int tiffDocument)
		{
			PdfRenderFlags pdfRenderFlags = PdfiumRenderFlags();
			Bitmap[] imgsBitmap = new Bitmap[2];
			IList<SizeF> docPageSizes = pdfDocument.PageSizes;

			//------------ Page 1 e 2(Blank)
			try
			{
				if (docPageSizes[documentPage].Width > 4000)
				{
					int newWidth2 = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, 14339, _maxHeight).Item1;
					int newHeight2 = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, 14339, _maxHeight).Item2;
					using Image image = pdfDocument.Render(documentPage, _widthDPI, _heightDPI, pdfRenderFlags);
					imgsBitmap[0] = new Bitmap(image, newWidth2, newHeight2);
					imgsBitmap[1] = CreateBlankPage(newWidth2, newHeight2);
					FileUtils.GarbageCollector(image);
				}

				else if (docPageSizes[documentPage].Height > 4000)
				{
					int newWidth2 = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, _maxWidth, 14339).Item1;
					int newHeight2 = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, _maxWidth, 14339).Item2;
					using Image image = pdfDocument.Render(documentPage, _widthDPI, _heightDPI, pdfRenderFlags);
					imgsBitmap[0] = new Bitmap(image, newWidth2, newHeight2);
					imgsBitmap[1] = CreateBlankPage(newWidth2, newHeight2);
					FileUtils.GarbageCollector(image);
				}
				else
				{
					int newWidth2 = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, _maxWidth, _maxHeight).Item1;
					int newHeight2 = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, _maxWidth, _maxHeight).Item2;
					using Image image = pdfDocument.Render(documentPage, _widthDPI, _heightDPI, pdfRenderFlags);
					imgsBitmap[0] = new Bitmap(image, newWidth2, newHeight2);
					imgsBitmap[1] = CreateBlankPage(newWidth2, newHeight2);
					FileUtils.GarbageCollector(image);
				}
			}
			catch (Exception ex)
			{
				int newWidth2 = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, _maxWidth, _maxHeight).Item1;
				int newHeight2 = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, _maxWidth, _maxHeight).Item2;
				using Image image = pdfDocument.Render(documentPage, newWidth2, newHeight2, _widthDPI, _heightDPI, pdfRenderFlags);
				imgsBitmap[0] = new Bitmap(image, newWidth2, newHeight2);
				imgsBitmap[1] = CreateBlankPage(newWidth2, newHeight2);
				FileUtils.GarbageCollector(image);
			}

			if (_tiffIsUsingColorCorrect == true)
			{
				Bitmap[] bwImgsBitmap = new Bitmap[2];
				bwImgsBitmap[0] = new Bitmap(ImageColorCorrect(imgsBitmap[0]));
				bwImgsBitmap[1] = imgsBitmap[1];

				string outputFileName = outputName + "_" + tiffDocument;

				CreateMultiFrameTiff(bwImgsBitmap, outputFileName);
				_generatedFiles.Add(outputFileName);

				FileUtils.GarbageCollector(imgsBitmap);
				FileUtils.GarbageCollector(bwImgsBitmap);
			}
			else
			{
				string outputFileName = outputName + "_" + tiffDocument;

				CreateMultiFrameTiff(imgsBitmap, outputFileName);
				_generatedFiles.Add(outputFileName);

				FileUtils.GarbageCollector(imgsBitmap);
			}

		}

		private void SaveTiffFolhaMode(PdfDocument pdfDocument, int documentPage, string outputName, int tiffDocument, int documentPageCount)
		{
			PdfRenderFlags pdfRenderFlags = PdfiumRenderFlags();
			Bitmap[] imgsBitmap = new Bitmap[2];
			IList<SizeF> docPageSizes = pdfDocument.PageSizes;

			//--------- Page 1 ---------
			try
			{
				if (docPageSizes[documentPage].Width > 4000)
				{
					int newWidthPag1 = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, 14339, _maxHeight).Item1;
					int newHeightPag1 = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, 14339, _maxHeight).Item2;
					Image imagePagina1 = pdfDocument.Render(documentPage, _widthDPI, _heightDPI, pdfRenderFlags);
					imgsBitmap[0] = new Bitmap(imagePagina1, newWidthPag1, newHeightPag1);
					FileUtils.GarbageCollector(imagePagina1);
				}
				else if (docPageSizes[documentPage].Height > 4000)
				{
					int newWidthPag1 = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, _maxWidth, 14339).Item1;
					int newHeightPag1 = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, _maxWidth, 14339).Item2;
					using Image imagePagina1 = pdfDocument.Render(documentPage, _widthDPI, _heightDPI, pdfRenderFlags);
					imgsBitmap[0] = new Bitmap(imagePagina1, newWidthPag1, newHeightPag1);
					FileUtils.GarbageCollector(imagePagina1);
				}
				else
				{
					int newWidthPag1 = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, _maxWidth, _maxHeight).Item1;
					int newHeightPag1 = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, _maxWidth, _maxHeight).Item2;
					using Image imagePagina1 = pdfDocument.Render(documentPage, _widthDPI, _heightDPI, pdfRenderFlags);
					imgsBitmap[0] = new Bitmap(imagePagina1, newWidthPag1, newHeightPag1);
					FileUtils.GarbageCollector(imagePagina1);
				}
			}
			catch (Exception ex)
			{
				int newWidthPag1 = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, _maxWidth, _maxHeight).Item1;
				int newHeightPag1 = FileUtils.ResizeFromOriginalImage(docPageSizes, documentPage, _maxWidth, _maxHeight).Item2;
				using Image imagePagina1 = pdfDocument.Render(documentPage, newWidthPag1, newHeightPag1, _widthDPI, _heightDPI, pdfRenderFlags);
				imgsBitmap[0] = new Bitmap(imagePagina1, newWidthPag1, newHeightPag1);
				FileUtils.GarbageCollector(imagePagina1);
			}

			//---------- Page 2 ----------
			int pag2 = documentPage + 1;
			if (pag2 < documentPageCount)
			{
				try
				{
					if (docPageSizes[pag2].Width > 4000)
					{
						int newWidthPage2 = FileUtils.ResizeFromOriginalImage(docPageSizes, pag2, 14339, _maxHeight).Item1;
						int newHeightPage2 = FileUtils.ResizeFromOriginalImage(docPageSizes, pag2, 14339, _maxHeight).Item2;
						using Image imagePagina2 = pdfDocument.Render(pag2, _widthDPI, _heightDPI, pdfRenderFlags);
						imgsBitmap[1] = new Bitmap(imagePagina2, newWidthPage2, newHeightPage2);
						FileUtils.GarbageCollector(imagePagina2);
					}
					else if (docPageSizes[pag2].Height > 4000)
					{
						int newWidthPage2 = FileUtils.ResizeFromOriginalImage(docPageSizes, pag2, _maxWidth, 14339).Item1;
						int newHeightPage2 = FileUtils.ResizeFromOriginalImage(docPageSizes, pag2, _maxWidth, 14339).Item2;
						using Image imagePagina2 = pdfDocument.Render(pag2, _widthDPI, _heightDPI, pdfRenderFlags);
						imgsBitmap[1] = new Bitmap(imagePagina2, newWidthPage2, newHeightPage2);
						FileUtils.GarbageCollector(imagePagina2);
					}
					else
					{
						int newWidthPage2 = FileUtils.ResizeFromOriginalImage(docPageSizes, pag2, _maxWidth, _maxHeight).Item1;
						int newHeightPage2 = FileUtils.ResizeFromOriginalImage(docPageSizes, pag2, _maxWidth, _maxHeight).Item2;
						using Image imagePagina2 = pdfDocument.Render(pag2, _widthDPI, _heightDPI, pdfRenderFlags);
						imgsBitmap[1] = new Bitmap(imagePagina2, newWidthPage2, newHeightPage2);
						FileUtils.GarbageCollector(imagePagina2);
					}
				}
				catch (Exception ex)
				{
					int newWidthPage2 = FileUtils.ResizeFromOriginalImage(docPageSizes, pag2, _maxWidth, _maxHeight).Item1;
					int newHeightPage2 = FileUtils.ResizeFromOriginalImage(docPageSizes, pag2, _maxWidth, _maxHeight).Item2;
					using Image imagePagina2 = pdfDocument.Render(pag2, newWidthPage2, newHeightPage2, _widthDPI, _heightDPI, pdfRenderFlags);
					imgsBitmap[1] = new Bitmap(imagePagina2, newWidthPage2, newHeightPage2);
					FileUtils.GarbageCollector(imagePagina2);
				}

			}
			else
			{
				imgsBitmap[1] = CreateBlankPage(_maxWidth, _maxHeight);
			}

			if (_tiffIsUsingColorCorrect == true)
			{
				Bitmap[] bwImgsBitmap = new Bitmap[2];
				bwImgsBitmap[0] = new Bitmap(ImageColorCorrect(imgsBitmap[0]));
				bwImgsBitmap[1] = new Bitmap(ImageColorCorrect(imgsBitmap[1]));

				string outputFileName = outputName + "_" + tiffDocument;

				CreateMultiFrameTiff(bwImgsBitmap, outputFileName);
				_generatedFiles.Add(outputFileName);

				FileUtils.GarbageCollector(imgsBitmap);
				FileUtils.GarbageCollector(bwImgsBitmap);
			}
			else
			{
				string outputFileName = outputName + "_" + tiffDocument;

				CreateMultiFrameTiff(imgsBitmap, outputFileName);
				_generatedFiles.Add(outputFileName);

				FileUtils.GarbageCollector(imgsBitmap);
			}

		}

		//private Bitmap BlankTiff()
		//{
		//	Bitmap imgBitmap = new Bitmap(_maxWidth, _maxHeight);
		//	using (Graphics graph = Graphics.FromImage(imgBitmap))
		//	{
		//		Rectangle ImageSize = new Rectangle(0, 0, _maxWidth, _maxHeight);
		//		graph.FillRectangle(System.Drawing.Brushes.White, ImageSize);
		//	}

		//	return imgBitmap;
		//}

		private void CreateMultiFrameTiff(Bitmap[] images, string outputPath)
		{
			FIMULTIBITMAP tiffBitmap = FIMULTIBITMAP.Zero;
			FIBITMAP pageBitmap = FIBITMAP.Zero;
			FREE_IMAGE_COLOR_DEPTH freeImageColorDepth = FreeImageTiffColorDepth(_tiffFreeImageColorDepth);
			FREE_IMAGE_SAVE_FLAGS freeImageCompression = FreeImageTiffCompression(_tiffFreeImageCompression);

			for (int i = 0; i < images.Count(); i++)
			{
				if (tiffBitmap == FIMULTIBITMAP.Zero)
				{
					images[0] = new Bitmap(images[0]);

					FIBITMAP freeImageBitmap = FreeImage.CreateFromBitmap(images[0]);
					FIBITMAP freeImageConverted = FreeImageConvertToColorBits(freeImageBitmap);
					pageBitmap = FreeImage.ConvertColorDepth(freeImageConverted, freeImageColorDepth);
					SetFreeImageDotsPerMeter(pageBitmap);
					FreeImage.Save(FREE_IMAGE_FORMAT.FIF_TIFF, pageBitmap, outputPath, freeImageCompression);
					tiffBitmap = FreeImage.OpenMultiBitmap(FREE_IMAGE_FORMAT.FIF_TIFF, outputPath, false, false, false, FREE_IMAGE_LOAD_FLAGS.DEFAULT);

					FreeImage.Unload(freeImageBitmap);
					FreeImage.Unload(freeImageConverted);

				}
				else
				{
					images[i] = new Bitmap(images[i]);
					FIBITMAP freeImageBitmap = FreeImage.CreateFromBitmap(images[i]);
					FIBITMAP freeImageConverted = FreeImageConvertToColorBits(freeImageBitmap);
					pageBitmap = FreeImage.ConvertColorDepth(freeImageConverted, freeImageColorDepth);
					SetFreeImageDotsPerMeter(pageBitmap);
					FreeImage.AppendPage(tiffBitmap, pageBitmap);

					FreeImage.Unload(freeImageBitmap);
					FreeImage.Unload(freeImageConverted);
				}

			}

			FreeImage.CloseMultiBitmap(tiffBitmap, freeImageCompression);
		}

		//private static ImageCodecInfo GetTiffEncoderInfo()
		//{
		//	ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
		//	foreach (ImageCodecInfo codec in codecs)
		//	{
		//		if (codec.FormatID == ImageFormat.Tiff.Guid)
		//		{
		//			return codec;
		//		}
		//	}
		//	return null!;
		//}

		//private static EncoderParameters GetJpgEnconderParameters()
		//{
		//	EncoderParameters jpgEncoderParameters;

		//	jpgEncoderParameters = new EncoderParameters(1);
		//	jpgEncoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 20L);

		//	return jpgEncoderParameters;
		//}

		private Bitmap ImageColorCorrect(Bitmap inputBitmap)
		{
			// Create a new Bitmap for the output
			using Bitmap outputBitmap = new Bitmap(inputBitmap.Width, inputBitmap.Height);

			// Perform any processing or manipulation on the inputBitmap here
			using (Graphics graphics = Graphics.FromImage(outputBitmap))
			{
				//ColorMatrix colorMatrix = new ColorMatrix(
				//    new float[][]{
				//new float[]{0.299f, 0.299f, 0.299f, 0, 0},
				//new float[]{0.587f, 0.587f, 0.587f, 0, 0},
				//new float[]{0.114f, 0.114f, 0.114f, 0, 0},
				//new float[]{0, 0, 0, 1, 0},
				//new float[]{0, 0, 0, 0, 1}

				ColorMatrix colorMatrix = new ColorMatrix(
					new float[][]{
				new float[]{0.175f, 0.175f, 0.175f, 0, 0},   // RED
				new float[]{0.587f, 0.587f, 0.587f, 0, 0},   // GREEN
				new float[]{0.114f, 0.114f, 0.114f, 0, 0},   // BLUE
				new float[]{0, 0, 0, 1, 0},
				new float[]{0, 0, 0, 0, 1}
			});

				ImageAttributes attributes = new ImageAttributes();
				attributes.SetColorMatrix(colorMatrix);

				graphics.DrawImage(inputBitmap, new Rectangle(0, 0, inputBitmap.Width, inputBitmap.Height),
					0, 0, inputBitmap.Width, inputBitmap.Height, GraphicsUnit.Pixel, attributes);

				FileUtils.GarbageCollector(graphics);
			}
			outputBitmap.SetResolution((float)_widthDPI, (float)_heightDPI);
			return outputBitmap;
		}



	}
}
