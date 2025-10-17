namespace SIBS.ISC.Domain.Convertions
{
	using System.Collections.Generic;
	using System.Drawing;
	using FreeImageAPI;
	using PdfiumViewer;
	using SIBS.ISC.Options;

	public class BaseConvert
	{
		protected string? _freeImageConvertToColorBits, _pdfIumRenderFlag;
		protected IList<string> _generatedFiles = new List<string>();
		protected PdfRenderFlags _pdfRenderFlags;
		protected int _maxWidth, _maxHeight;
		protected uint _widthDPI, _heightDPI;
		/*protected int _pageSheetPartitionMode;*/ //0- Folha ou 1- Página

		//protected ArrayList _panelContainer = new ArrayList();


		public BaseConvert(SpecificImageSplitterConverterOption specificImageSplitterConverterOption, string convertPdfTo)
		{
			//_pageSheetPartitionMode = specificImageSplitterConverterOption.PageSheetPartitionMode;

			if (convertPdfTo == "Jpg")
			{
				_maxWidth = specificImageSplitterConverterOption.JpgMaxWidth;
				_maxHeight = specificImageSplitterConverterOption.JpgMaxHeight;
				_widthDPI = specificImageSplitterConverterOption.JpgWidthDPI;
				_heightDPI = specificImageSplitterConverterOption.JpgHeightDPI;
				_pdfIumRenderFlag = specificImageSplitterConverterOption.JpgPDFiumRenderFlag.ToString();
				_freeImageConvertToColorBits = specificImageSplitterConverterOption.JpgFreeImageConverToColorBits.ToString();
			}

			if(convertPdfTo == "Tiff")
			{
				_maxWidth = specificImageSplitterConverterOption.TiffMaxWidth;
				_maxHeight = specificImageSplitterConverterOption.TiffMaxHeight;
				_widthDPI = specificImageSplitterConverterOption.TiffWidthDPI;
				_heightDPI = specificImageSplitterConverterOption.TiffHeightDPI;
				_pdfIumRenderFlag = specificImageSplitterConverterOption.TiffPdfiumRenderFlag.ToString();
				_freeImageConvertToColorBits = specificImageSplitterConverterOption.TiffFreeImageConverToColorBits.ToString();
			}

		}

		protected void SetFreeImageDotsPerMeter(FIBITMAP pageBitmap)
		{
			FreeImage.SetDotsPerMeterX(pageBitmap, (uint)(_widthDPI * 39.3701));
			FreeImage.SetDotsPerMeterY(pageBitmap, (uint)(_heightDPI * 39.3701));
		}

		protected FreeImageAPI.FIBITMAP FreeImageConvertToColorBits(FreeImageAPI.FIBITMAP freeImageBitmap)
		{
			switch (_freeImageConvertToColorBits)
			{
				case "GREYSCALE":
				case "GRAYSCALE":
					return FreeImageAPI.FreeImage.ConvertToGreyscale(freeImageBitmap);

				case "BITS8":
					return FreeImageAPI.FreeImage.ConvertTo8Bits(freeImageBitmap);

				case "BITS16":
					return FreeImageAPI.FreeImage.ConvertTo16Bits565(freeImageBitmap);

				case "BITS24":
					return FreeImageAPI.FreeImage.ConvertTo24Bits(freeImageBitmap);

				default:
					return FreeImageAPI.FreeImage.ConvertTo24Bits(freeImageBitmap);
			}
		}

		protected PdfRenderFlags PdfiumRenderFlags() 
		{
			switch (_pdfIumRenderFlag)
			{
				case "GRAYSCALE":
				case "GREYSCALE":
					return PdfRenderFlags.CorrectFromDpi | PdfRenderFlags.Annotations | PdfRenderFlags.Grayscale;

				case "FORCEHALFTONE":
					return PdfRenderFlags.CorrectFromDpi | PdfRenderFlags.Annotations | PdfRenderFlags.Grayscale;

				default:
					return PdfRenderFlags.CorrectFromDpi | PdfRenderFlags.Annotations;
			}

		}

		protected FREE_IMAGE_COLOR_DEPTH FreeImageJpgColorDepth(string _freeImageConvertToColorBits)
		{
			switch (_freeImageConvertToColorBits)
			{
				case "GREYSCALE":
				case "GRAYSCALE":
					return FREE_IMAGE_COLOR_DEPTH.FICD_FORCE_GREYSCALE;

				case "BITS8":
					return FREE_IMAGE_COLOR_DEPTH.FICD_08_BPP;

				case "BITS16":
					return FREE_IMAGE_COLOR_DEPTH.FICD_16_BPP;

				case "BITS24":
					return FREE_IMAGE_COLOR_DEPTH.FICD_24_BPP;

				default:
					return FREE_IMAGE_COLOR_DEPTH.FICD_24_BPP;
			}
		}

		protected FREE_IMAGE_COLOR_DEPTH FreeImageTiffColorDepth(string _tiffFreeImageColorDepth)
		{
			switch (_tiffFreeImageColorDepth)
			{
				case "GREYSCALE_BPPDITHER":
				case "GRAYSCALE_BPPDITHER":
					return FREE_IMAGE_COLOR_DEPTH.FICD_01_BPP_DITHER | FREE_IMAGE_COLOR_DEPTH.FICD_FORCE_GREYSCALE;

				default:
					return FREE_IMAGE_COLOR_DEPTH.FICD_UNKNOWN;
			}
		}

		protected static Tuple<int, int> GetResizesValuesFromOriginalImage(int originalWidth, int originalHeight, int maxWidth, int maxHeight)
		{
			//Calc Percentagem Imagem
			var calcWidthPercent = maxWidth / (float)originalWidth;
			var calcHeightPercent = maxHeight / (float)originalHeight;
			var perc = Math.Min(calcWidthPercent, calcHeightPercent);
			var newWidth = (int)(originalWidth * perc);
			var newHeight = (int)(originalHeight * perc);

			return Tuple.Create(newWidth, newHeight);
		}

		protected Bitmap CreateBlankPage(int width, int height)
		{
			Bitmap imgBitmap = new Bitmap(width, height);
			using (Graphics graph = Graphics.FromImage(imgBitmap))
			{
				Rectangle ImageSize = new Rectangle(0, 0, width, height);
				graph.FillRectangle(Brushes.White, ImageSize);
			}
			return imgBitmap;
		}

		protected FreeImageAPI.FREE_IMAGE_SAVE_FLAGS FreeImageJpgQuality(string _jpgFreeImageQuality)
		{
			switch (_jpgFreeImageQuality)
			{
				case "BAD":
					return FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYBAD;

				case "AVERAGE":
					return FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYAVERAGE;

				case "NORMAL":
					return FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYNORMAL;

				case "GOOD":
					return FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYGOOD;

				case "SUPERB":
					return FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYSUPERB;
				default:
					return FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYNORMAL;
			}
		}

		protected FREE_IMAGE_SAVE_FLAGS FreeImageTiffCompression(string _tiffFreeImageCompression)
		{
			switch (_tiffFreeImageCompression)
			{
				case "FAX_LZW":
					return FREE_IMAGE_SAVE_FLAGS.TIFF_LZW | FREE_IMAGE_SAVE_FLAGS.TIFF_CCITTFAX4;

				default:
					return FREE_IMAGE_SAVE_FLAGS.DEFAULT;
			}
		}


	}
}
