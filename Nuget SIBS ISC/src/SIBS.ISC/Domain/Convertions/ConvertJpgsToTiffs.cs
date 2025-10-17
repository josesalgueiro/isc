namespace SIBS.ISC.Domain.Convertions
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;
	using FreeImageAPI;
	using SIBS.ISC.Exceptions;
	using SIBS.ISC.Helpers;
	using SIBS.ISC.Options;

	public class ConvertJpgsToTiffs : BaseConvert
	{
		private const string TRACE_NAME = "ConvertJpgToTiffs";
		protected string _tiffFreeImageColorDepth, _tiffFreeImageCompression;
		protected bool _tiffIsUsingColorCorrect;

		public ConvertJpgsToTiffs(SpecificImageSplitterConverterOption specificImageSplitterConverterOption)
			: base(specificImageSplitterConverterOption, "Tiff")
		{
			_tiffFreeImageColorDepth = specificImageSplitterConverterOption.TiffFreeImageColorDepth.ToString();
			_tiffFreeImageCompression = specificImageSplitterConverterOption.TiffFreeImageCompression.ToString();
			_tiffIsUsingColorCorrect = specificImageSplitterConverterOption.TiffIsUsingColorCorrect;
		}
		public IEnumerable<string> StartConvertJpgsToTiffs(Stream originalFile, int partitionMode)
		{ 
			IList<Stream> originalFiles = new List<Stream>
			{
				originalFile
			};
			return StartConvertJpgsToTiffs(originalFiles, partitionMode);
		}

			public IEnumerable<string> StartConvertJpgsToTiffs(IList<Stream> originalFiles, int partitionMode)
		{
			if (originalFiles is null)
				throw new ImageSplitterConverterException("The originalFiles is null.");

			string tempDataFolder = Path.GetTempPath();
			string outputName = Path.Combine(tempDataFolder, Guid.NewGuid().ToString());

			if (partitionMode == 0)
			{
				for (int jpgIndex = 0; jpgIndex < originalFiles.Count(); jpgIndex++)
				{
					outputName = Path.Combine(tempDataFolder, Guid.NewGuid().ToString());

					// Create a new multi-page TIFF to store the split pages
					FIMULTIBITMAP outputBitmap = FreeImage.OpenMultiBitmap(FREE_IMAGE_FORMAT.FIF_TIFF, outputName, true, false, false, FREE_IMAGE_LOAD_FLAGS.DEFAULT);

					// Add two pages to the new multi-page TIFF
					Bitmap bitmap = new Bitmap(originalFiles[jpgIndex]);
					FIBITMAP fiBitmapPag1 = FreeImage.CreateFromBitmap(bitmap);
					SetFreeImageDotsPerMeter(fiBitmapPag1);
					fiBitmapPag1 = FreeImage.ConvertColorDepth(fiBitmapPag1, FreeImageTiffColorDepth(_tiffFreeImageColorDepth));
					fiBitmapPag1 = FreeImageConvertToColorBits(fiBitmapPag1);
					var newValuesPage1WidthHeight = GetResizesValuesFromOriginalImage((int)FreeImage.GetWidth(fiBitmapPag1), (int)FreeImage.GetHeight(fiBitmapPag1), _maxWidth, _maxHeight);
					fiBitmapPag1 = FreeImage.Rescale(fiBitmapPag1, newValuesPage1WidthHeight.Item1, newValuesPage1WidthHeight.Item2, FREE_IMAGE_FILTER.FILTER_BOX);
					FreeImage.AppendPage(outputBitmap, fiBitmapPag1);

					if (jpgIndex < originalFiles.Count() - 1)
					{
						Bitmap nextBitmap = new Bitmap(originalFiles[jpgIndex + 1]);
						FIBITMAP fiBitmapNextPag = FreeImage.CreateFromBitmap(nextBitmap);
						SetFreeImageDotsPerMeter(fiBitmapNextPag);
						fiBitmapNextPag = FreeImage.ConvertColorDepth(fiBitmapNextPag, FreeImageTiffColorDepth(_tiffFreeImageColorDepth));
						fiBitmapNextPag = FreeImageConvertToColorBits(fiBitmapNextPag);
						var newValuesNextPageWidthHeight = GetResizesValuesFromOriginalImage((int)FreeImage.GetWidth(fiBitmapNextPag), (int)FreeImage.GetHeight(fiBitmapNextPag), _maxWidth, _maxHeight);
						fiBitmapNextPag = FreeImage.Rescale(fiBitmapNextPag, newValuesNextPageWidthHeight.Item1, newValuesNextPageWidthHeight.Item2, FREE_IMAGE_FILTER.FILTER_BOX);
						FreeImage.AppendPage(outputBitmap, fiBitmapNextPag);
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

					jpgIndex++;
				}

				// Close the input multi-page TIFF
				//FreeImage.CloseMultiBitmap(multiBitmap, FreeImageCompression());
			}

			if (partitionMode == 1)
			{
				for (int jpgIndex = 0; jpgIndex < originalFiles.Count(); jpgIndex++)
				{
					outputName = Path.Combine(tempDataFolder, Guid.NewGuid().ToString());

					// Create a new multi-page TIFF to store the split pages
					FIMULTIBITMAP outputBitmap = FreeImage.OpenMultiBitmap(FREE_IMAGE_FORMAT.FIF_TIFF, outputName, true, false, false, FREE_IMAGE_LOAD_FLAGS.DEFAULT);

					// Add Jpg to page 1
					Bitmap bitmap = new Bitmap(originalFiles[jpgIndex]);
					FIBITMAP fiBitmapPag1 = FreeImage.CreateFromBitmap(bitmap);
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
				//FreeImage.CloseMultiBitmap(multiBitmap, FreeImageCompression());
			}

			return _generatedFiles.ToArray();
		}


	}
}
