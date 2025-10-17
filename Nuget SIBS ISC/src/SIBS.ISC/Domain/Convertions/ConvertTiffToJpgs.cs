namespace SIBS.ISC.Domain.Convertions
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Runtime.InteropServices.ComTypes;
	using System.Text;
	using FreeImageAPI;
	using SIBS.ISC.Exceptions;
	using SIBS.ISC.Options;

	public class ConvertTiffToJpgs : BaseConvert
	{
		private const string TRACE_NAME = "ConvertTiffToJpgs";
		protected string _jpgFreeImageQuality;

		public ConvertTiffToJpgs(SpecificImageSplitterConverterOption specificImageSplitterConverterOption)
			: base(specificImageSplitterConverterOption, "Jpg")
		{
			_jpgFreeImageQuality = specificImageSplitterConverterOption.JpgFreeImageQuality.ToString();
		}

		public IEnumerable<string> StartConvertTiffToJpgs(Stream originalFile, int partitionMode)
		{
			if (originalFile is null)
				throw new ImageSplitterConverterException("The originalFile is null.");

			if (!Enumerable.Range(0, 2).Contains(partitionMode))
				throw new ImageSplitterConverterException("The partition mode value not exists.");

			if (_freeImageConvertToColorBits is null)
				throw new ImageSplitterConverterException("_freeImageConvertToColorBits is null");

			string tempDataFolder = Path.GetTempPath();
			string outputNamePage = string.Empty;
			string outputNameNextPage = string.Empty;
			string outputNameBlankPage = string.Empty;

			FIMULTIBITMAP multiBitmap = FreeImage.OpenMultiBitmapFromStream(originalFile);


			int pageCount = FreeImage.GetPageCount(multiBitmap);

			if (partitionMode == 0)
			{
				for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
				{
					// Create Page Jpeg
					outputNamePage = Path.Combine(tempDataFolder, Guid.NewGuid().ToString());
					FIBITMAP fiBitmapPage = FreeImage.LockPage(multiBitmap, pageIndex);
					SetFreeImageDotsPerMeter(fiBitmapPage);
					fiBitmapPage = FreeImage.ConvertColorDepth(fiBitmapPage, FreeImageJpgColorDepth(_freeImageConvertToColorBits));
					var newValuesPageWidthHeight = GetResizesValuesFromOriginalImage((int)FreeImage.GetWidth(fiBitmapPage), (int)FreeImage.GetHeight(fiBitmapPage), _maxWidth, _maxHeight);
					fiBitmapPage = FreeImage.Rescale(fiBitmapPage, newValuesPageWidthHeight.Item1, newValuesPageWidthHeight.Item2, FREE_IMAGE_FILTER.FILTER_BOX);
					FreeImage.Save(FREE_IMAGE_FORMAT.FIF_JPEG, fiBitmapPage, outputNamePage, FreeImageJpgQuality(_jpgFreeImageQuality));
					_generatedFiles.Add(outputNamePage);

					// Create Next Page Jpeg
					if(pageIndex < pageCount - 1)
					{
						outputNameNextPage = Path.Combine(tempDataFolder, Guid.NewGuid().ToString());
						FIBITMAP fiBitmapNextPage = FreeImage.LockPage(multiBitmap, pageIndex + 1);
						SetFreeImageDotsPerMeter(fiBitmapNextPage);
						fiBitmapNextPage = FreeImage.ConvertColorDepth(fiBitmapNextPage, FreeImageJpgColorDepth(_freeImageConvertToColorBits));
						var newValuesNextPageWidthHeight = GetResizesValuesFromOriginalImage((int)FreeImage.GetWidth(fiBitmapNextPage), (int)FreeImage.GetHeight(fiBitmapNextPage), _maxWidth, _maxHeight);
						fiBitmapNextPage = FreeImage.Rescale(fiBitmapNextPage, newValuesNextPageWidthHeight.Item1, newValuesNextPageWidthHeight.Item2, FREE_IMAGE_FILTER.FILTER_BOX);
						FreeImage.Save(FREE_IMAGE_FORMAT.FIF_JPEG, fiBitmapNextPage, outputNameNextPage, FreeImageJpgQuality(_jpgFreeImageQuality));
						_generatedFiles.Add(outputNameNextPage);
					}
					else
					{
						// Create blank Jpeg
						outputNameBlankPage = Path.Combine(tempDataFolder, Guid.NewGuid().ToString());
						FIBITMAP freeImageBlankBitmap = FreeImage.CreateFromBitmap(CreateBlankPage(newValuesPageWidthHeight.Item1, newValuesPageWidthHeight.Item2));
						freeImageBlankBitmap = FreeImage.ConvertColorDepth(freeImageBlankBitmap, FREE_IMAGE_COLOR_DEPTH.FICD_08_BPP);
						FreeImage.Save(FREE_IMAGE_FORMAT.FIF_JPEG, freeImageBlankBitmap, outputNameBlankPage, FreeImageJpgQuality(_jpgFreeImageQuality));
						_generatedFiles.Add(outputNameBlankPage);
					}
					pageIndex++;
				}
			}

			if (partitionMode == 1)
			{
				for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
				{
					// Create Page Jpeg
					outputNamePage = Path.Combine(tempDataFolder, Guid.NewGuid().ToString());
					FIBITMAP fiBitmapPage = FreeImage.LockPage(multiBitmap, pageIndex);
					SetFreeImageDotsPerMeter(fiBitmapPage);
					fiBitmapPage = FreeImage.ConvertColorDepth(fiBitmapPage, FreeImageJpgColorDepth(_freeImageConvertToColorBits));
					var newValuesPageWidthHeight = GetResizesValuesFromOriginalImage((int)FreeImage.GetWidth(fiBitmapPage), (int)FreeImage.GetHeight(fiBitmapPage), _maxWidth, _maxHeight);
					fiBitmapPage = FreeImage.Rescale(fiBitmapPage, newValuesPageWidthHeight.Item1, newValuesPageWidthHeight.Item2, FREE_IMAGE_FILTER.FILTER_BOX);
					FreeImage.Save(FREE_IMAGE_FORMAT.FIF_JPEG, fiBitmapPage, outputNamePage, FreeImageJpgQuality(_jpgFreeImageQuality));
					_generatedFiles.Add(outputNamePage);

					// Create blank Jpeg
					outputNameBlankPage = Path.Combine(tempDataFolder, Guid.NewGuid().ToString());
					FIBITMAP freeImageBlankBitmap = FreeImage.CreateFromBitmap(CreateBlankPage(newValuesPageWidthHeight.Item1, newValuesPageWidthHeight.Item2));
					freeImageBlankBitmap = FreeImage.ConvertColorDepth(freeImageBlankBitmap, FREE_IMAGE_COLOR_DEPTH.FICD_08_BPP);
					FreeImage.Save(FREE_IMAGE_FORMAT.FIF_JPEG, freeImageBlankBitmap, outputNameBlankPage, FreeImageJpgQuality(_jpgFreeImageQuality));
					_generatedFiles.Add(outputNameBlankPage);
				}
			}

			return _generatedFiles.ToArray();
		}



	}
}
