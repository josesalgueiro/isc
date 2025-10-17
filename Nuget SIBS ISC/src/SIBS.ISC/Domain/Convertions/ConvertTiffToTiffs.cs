namespace SIBS.ISC.Domain.Convertions
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.IO;
	using System.Text;
	using FreeImageAPI;
	using SIBS.ISC.Exceptions;
	using SIBS.ISC.Options;

	public class ConvertTiffToTiffs : BaseConvert
	{
		private const string TRACE_NAME = "ConvertTiffToTiffs";
		protected string _tiffFreeImageColorDepth, _tiffFreeImageCompression;
		protected bool _tiffIsUsingColorCorrect;

		public ConvertTiffToTiffs(SpecificImageSplitterConverterOption specificImageSplitterConverterOption)
		: base(specificImageSplitterConverterOption, "Tiff")
		{
			_tiffFreeImageColorDepth = specificImageSplitterConverterOption.TiffFreeImageColorDepth.ToString();
			_tiffFreeImageCompression = specificImageSplitterConverterOption.TiffFreeImageCompression.ToString();
			_tiffIsUsingColorCorrect = specificImageSplitterConverterOption.TiffIsUsingColorCorrect;
		}

		public IEnumerable<string> StartConvertTiffToTiffs(Stream originalFile, int partitionMode)
		{
			if (originalFile is null)
				throw new ImageSplitterConverterException("The originalFile is null.");

			if (!Enumerable.Range(0, 2).Contains(partitionMode))
				throw new ImageSplitterConverterException("The partition mode value not exists.");

			string tempDataFolder = Path.GetTempPath();
			string outputName = string.Empty;

			FIMULTIBITMAP multiBitmap = FreeImage.OpenMultiBitmapFromStream(originalFile);

			
			int pageCount = FreeImage.GetPageCount(multiBitmap);

			if(partitionMode == 0)
			{
				for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
				{
					outputName = Path.Combine(tempDataFolder, Guid.NewGuid().ToString());

					// Create a new multi-page TIFF to store the split pages
					FIMULTIBITMAP outputBitmap = FreeImage.OpenMultiBitmap(FREE_IMAGE_FORMAT.FIF_TIFF, outputName, true, false, false, FREE_IMAGE_LOAD_FLAGS.DEFAULT);

					// Add two pages to the new multi-page TIFF
					FIBITMAP fiBitmapPag1 = FreeImage.LockPage(multiBitmap, pageIndex);
					SetFreeImageDotsPerMeter(fiBitmapPag1);
					fiBitmapPag1 = FreeImage.ConvertColorDepth(fiBitmapPag1, FreeImageTiffColorDepth(_tiffFreeImageCompression));
					fiBitmapPag1 = FreeImageConvertToColorBits(fiBitmapPag1);
					var newValuesPage1WidthHeight = GetResizesValuesFromOriginalImage((int)FreeImage.GetWidth(fiBitmapPag1), (int)FreeImage.GetHeight(fiBitmapPag1), _maxWidth, _maxHeight);
					fiBitmapPag1 = FreeImage.Rescale(fiBitmapPag1, newValuesPage1WidthHeight.Item1, newValuesPage1WidthHeight.Item2, FREE_IMAGE_FILTER.FILTER_BOX);
					FreeImage.AppendPage(outputBitmap, fiBitmapPag1);

					if (pageIndex < pageCount - 1)
					{
						FIBITMAP fiBitmapPag2 = FreeImage.LockPage(multiBitmap, pageIndex + 1);
						SetFreeImageDotsPerMeter(fiBitmapPag2);
						fiBitmapPag2 = FreeImage.ConvertColorDepth(fiBitmapPag2, FreeImageTiffColorDepth(_tiffFreeImageCompression));
						fiBitmapPag2 = FreeImageConvertToColorBits(fiBitmapPag2);
						var newValuesPage2WidthHeight = GetResizesValuesFromOriginalImage((int)FreeImage.GetWidth(fiBitmapPag2), (int)FreeImage.GetHeight(fiBitmapPag2), _maxWidth, _maxHeight);
						fiBitmapPag2 = FreeImage.Rescale(fiBitmapPag2, newValuesPage2WidthHeight.Item1, newValuesPage2WidthHeight.Item2, FREE_IMAGE_FILTER.FILTER_BOX);
						FreeImage.AppendPage(outputBitmap, fiBitmapPag2);
					}
					else
					{
						FIBITMAP freeImageBlankBitmap = FreeImage.CreateFromBitmap(CreateBlankPage(newValuesPage1WidthHeight.Item1, newValuesPage1WidthHeight.Item2));
						freeImageBlankBitmap = FreeImage.ConvertColorDepth(freeImageBlankBitmap, FREE_IMAGE_COLOR_DEPTH.FICD_08_BPP);
						FreeImage.AppendPage(outputBitmap, freeImageBlankBitmap);
					}

					// Save the new multi-page TIFF
					FreeImage.CloseMultiBitmap(outputBitmap, FreeImageTiffCompression(_tiffFreeImageCompression));

					_generatedFiles.Add(outputName);

					pageIndex++;
				}

				// Close the input multi-page TIFF
				FreeImage.CloseMultiBitmap(multiBitmap, FreeImageTiffCompression(_tiffFreeImageCompression));
			}

			if(partitionMode == 1)
			{
				for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
				{
					outputName = Path.Combine(tempDataFolder, Guid.NewGuid().ToString());

					// Create a new multi-page TIFF to store the split pages
					FIMULTIBITMAP outputBitmap = FreeImage.OpenMultiBitmap(FREE_IMAGE_FORMAT.FIF_TIFF, outputName, true, false, false, FREE_IMAGE_LOAD_FLAGS.DEFAULT);

					// Add page to the new multi-page TIFF
					FIBITMAP fiBitmapPag1 = FreeImage.LockPage(multiBitmap, pageIndex);
					SetFreeImageDotsPerMeter(fiBitmapPag1);
					fiBitmapPag1 = FreeImage.ConvertColorDepth(fiBitmapPag1, FreeImageTiffColorDepth(_tiffFreeImageColorDepth));
					fiBitmapPag1 = FreeImageConvertToColorBits(fiBitmapPag1);
					var newValuesPage1WidthHeight = GetResizesValuesFromOriginalImage((int)FreeImage.GetWidth(fiBitmapPag1), (int)FreeImage.GetHeight(fiBitmapPag1), _maxWidth, _maxHeight);
					fiBitmapPag1 = FreeImage.Rescale(fiBitmapPag1, newValuesPage1WidthHeight.Item1, newValuesPage1WidthHeight.Item2, FREE_IMAGE_FILTER.FILTER_BOX);
					FreeImage.AppendPage(outputBitmap, fiBitmapPag1);

					// Add blank page to the new multi-page TIFF
					FIBITMAP freeImageBlankBitmap = FreeImage.CreateFromBitmap(CreateBlankPage(newValuesPage1WidthHeight.Item1, newValuesPage1WidthHeight.Item2));
					freeImageBlankBitmap = FreeImage.ConvertColorDepth(freeImageBlankBitmap, FREE_IMAGE_COLOR_DEPTH.FICD_08_BPP);
					FreeImage.AppendPage(outputBitmap, freeImageBlankBitmap);

					// Save the new multi-page TIFF
					FreeImage.CloseMultiBitmap(outputBitmap, FreeImageTiffCompression(_tiffFreeImageCompression));

					_generatedFiles.Add(outputName);

				}

				// Close the input multi-page TIFF
				FreeImage.CloseMultiBitmap(multiBitmap, FreeImageTiffCompression(_tiffFreeImageCompression));
			}


			return _generatedFiles.ToArray();
		}

		

		

		//public static FIBITMAP ConvertTIFFBlackWhite(FIBITMAP bitmap)
		//{
		//	FIBITMAP bwBitmap = FreeImage.ConvertColorDepth(bitmap, FREE_IMAGE_COLOR_DEPTH.FICD_01_BPP_THRESHOLD | FREE_IMAGE_COLOR_DEPTH.FICD_FORCE_GREYSCALE);
		//	EnsureWhiteBackgroundTIFF(bwBitmap);
		//	return bwBitmap;
		//}

		//public static FIBITMAP ConvertTIFFBlackWhite(FIBITMAP bitmap, FREE_IMAGE_COLOR_DEPTH colorDepth)
		//{
		//	FIBITMAP bwBitmap = FreeImage.ConvertColorDepth(bitmap, colorDepth);
		//	EnsureWhiteBackgroundTIFF(bwBitmap);
		//	return bwBitmap;
		//}

		//public static void EnsureWhiteBackgroundTIFF(FIBITMAP bitmap)
		//{
		//	FreeImageAPI.Palette colorPalette = new Palette(bitmap);
		//	if (FreeImage.GetColorType(bitmap) == FREE_IMAGE_COLOR_TYPE.FIC_MINISBLACK)
		//	{
		//		colorPalette.Reverse();
		//		FreeImage.Invert(bitmap);
		//	}
		//}

		//public static void GenerateTIFFPage(ref FIMULTIBITMAP tiffBitmap, FIBITMAP pageBitmap, bool createNew, string filename)
		//{
		//	if (createNew)
		//	{
		//		FreeImage.Save(FREE_IMAGE_FORMAT.FIF_TIFF, pageBitmap, filename, FREE_IMAGE_SAVE_FLAGS.TIFF_LZW | FREE_IMAGE_SAVE_FLAGS.TIFF_CCITTFAX4);

		//		tiffBitmap = FreeImage.OpenMultiBitmap(FREE_IMAGE_FORMAT.FIF_TIFF, filename, false, false, false, FREE_IMAGE_LOAD_FLAGS.DEFAULT);
		//	}
		//	else
		//	{
		//		FreeImage.AppendPage(tiffBitmap, pageBitmap);
		//	}
		//}

		//public static bool ResizeImage(ref FIBITMAP bitmap, int maxWidth, int maxHeight)
		//{
		//	var originalWidth = FreeImage.GetWidth(bitmap);
		//	var originalHeight = FreeImage.GetHeight(bitmap);

		//	if (originalWidth < maxWidth && originalHeight < maxHeight)
		//		return false;

		//	var targetWidth = maxWidth;
		//	var targetHeight = (originalHeight * maxWidth) / originalWidth;

		//	var resizedBitmap = FreeImage.Rescale(bitmap, (int)targetWidth, (int)targetHeight, FREE_IMAGE_FILTER.FILTER_CATMULLROM);
		//	FreeImage.Unload(bitmap);

		//	bitmap = resizedBitmap;
		//	return true;
		//}

	}
}
