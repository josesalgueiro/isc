namespace SIBS.ISC.Domain.Convertions
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using FreeImageAPI;
	using SIBS.ISC.Exceptions;
	using SIBS.ISC.Options;

	public class ConvertJpgToJpg : BaseConvert
	{
		private const string TRACE_NAME = "ConvertJpgToJpg";
		protected string _jpgFreeImageQuality;

		public ConvertJpgToJpg(SpecificImageSplitterConverterOption specificImageSplitterConverterOption)
			: base(specificImageSplitterConverterOption, "Jpg")
		{
			_jpgFreeImageQuality = specificImageSplitterConverterOption.JpgFreeImageQuality.ToString();
		}

		public IEnumerable<string> StartConvertJpgToJpg(Stream originalFile, int partitionMode)
		{
			if (originalFile is null)
				throw new ImageSplitterConverterException("The originalFile is null.");

			if (!Enumerable.Range(0, 2).Contains(partitionMode))
				throw new ImageSplitterConverterException("The partition mode value not exists.");

			if (_freeImageConvertToColorBits is null)
				throw new ImageSplitterConverterException("_freeImageConvertToColorBits is null");

			string tempDataFolder = Path.GetTempPath();

				
			// Create Page Jpeg
			string outputNamePage = Path.Combine(tempDataFolder, Guid.NewGuid().ToString());
			FIBITMAP fiBitmapPage = FreeImage.LoadFromStream(originalFile);
			SetFreeImageDotsPerMeter(fiBitmapPage);
			fiBitmapPage = FreeImage.ConvertColorDepth(fiBitmapPage, FreeImageJpgColorDepth(_freeImageConvertToColorBits));
			var newValuesPageWidthHeight = GetResizesValuesFromOriginalImage((int)FreeImage.GetWidth(fiBitmapPage), (int)FreeImage.GetHeight(fiBitmapPage), _maxWidth, _maxHeight);
			fiBitmapPage = FreeImage.Rescale(fiBitmapPage, newValuesPageWidthHeight.Item1, newValuesPageWidthHeight.Item2, FREE_IMAGE_FILTER.FILTER_BOX);
			FreeImage.Save(FREE_IMAGE_FORMAT.FIF_JPEG, fiBitmapPage, outputNamePage, FreeImageJpgQuality(_jpgFreeImageQuality));
			_generatedFiles.Add(outputNamePage);
					
			// Create blank Jpeg
			string outputNameBlankPage = Path.Combine(tempDataFolder, Guid.NewGuid().ToString());
			FIBITMAP freeImageBlankBitmap = FreeImage.CreateFromBitmap(CreateBlankPage(newValuesPageWidthHeight.Item1, newValuesPageWidthHeight.Item2));
			freeImageBlankBitmap = FreeImage.ConvertColorDepth(freeImageBlankBitmap, FREE_IMAGE_COLOR_DEPTH.FICD_08_BPP);
			FreeImage.Save(FREE_IMAGE_FORMAT.FIF_JPEG, freeImageBlankBitmap, outputNameBlankPage, FreeImageJpgQuality(_jpgFreeImageQuality));
			_generatedFiles.Add(outputNameBlankPage);
					
			return _generatedFiles.ToArray();
		}
	}
}
