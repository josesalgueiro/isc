namespace SIBS.ISC.Options
{
	using System;
	using System.Runtime.Serialization;

	public class ImageSplitterConverterOption : ImageConverterOption
	{
		public List<SpecificImageSplitterConverterOption> ProductOptions { get; set; } = new();

		public override void Merge(ImageConverterOption? priorityImageConverterOption)
		{
			if (priorityImageConverterOption is null)
				return;


			if (priorityImageConverterOption is ImageSplitterConverterOption imageSplitterConverterOption)
			{
				var result = new List<SpecificImageSplitterConverterOption>();
				foreach (var productOption in this.ProductOptions)
				{
					var priorityProductOption = imageSplitterConverterOption.ProductOptions.SingleOrDefault(p => p.ProductType == productOption.ProductType);
					if (priorityProductOption is null)
						continue;

					productOption.Merge(priorityProductOption);

					result.Add(productOption);
				}

				foreach (var priorityProductOption in imageSplitterConverterOption.ProductOptions)
				{
					var productOption = this.ProductOptions.SingleOrDefault(p => p.ProductType == priorityProductOption.ProductType);
					if (productOption is null)
						result.Add(priorityProductOption);
				}

				this.ProductOptions = result;
			}
		}
	}

	public class SpecificImageSplitterConverterOption
	{
		public int ProductType { get; set; }
		//public int PageSheetPartitionMode { get; set; }
		public int JpgMaxWidth { get; set; }
		public int JpgMaxHeight { get; set; }
		public uint JpgWidthDPI { get; set; }
		public uint JpgHeightDPI { get; set; }
		public PDFiumRenderFlagEnum JpgPDFiumRenderFlag { get; set; } = PDFiumRenderFlagEnum.FORCEHALFTONE;
		public FreeImageQualityEnum JpgFreeImageQuality { get; set; } = FreeImageQualityEnum.SUPERB;
		public FreeImageConverToColorBitsEnum JpgFreeImageConverToColorBits { get; set; } = FreeImageConverToColorBitsEnum.BITS24;
		public int TiffMaxWidth { get; set; }
		public int TiffMaxHeight { get; set; }
		public uint TiffWidthDPI { get; set; }
		public uint TiffHeightDPI { get; set; }
		public PDFiumRenderFlagEnum TiffPdfiumRenderFlag { get; set; } = PDFiumRenderFlagEnum.NONE;
		public FreeImageConverToColorBitsEnum TiffFreeImageConverToColorBits { get; set; } = FreeImageConverToColorBitsEnum.BITS24;
		public FreeImageColorDepthEnum TiffFreeImageColorDepth { get; set; } = FreeImageColorDepthEnum.GREYSCALE_BPPDITHER;
		public FreeImageCompressionEnum TiffFreeImageCompression { get; set; } = FreeImageCompressionEnum.FAX_LZW;
		public bool TiffIsUsingColorCorrect { get; set; }

		internal void Merge(SpecificImageSplitterConverterOption priorityProductOption)
		{
			//PageSheetPartitionMode = priorityProductOption.PageSheetPartitionMode;

			JpgMaxWidth = priorityProductOption.JpgMaxWidth;
			JpgMaxHeight = priorityProductOption.JpgMaxHeight;
			JpgWidthDPI = priorityProductOption.JpgWidthDPI;
			JpgHeightDPI = priorityProductOption.JpgHeightDPI;
			JpgPDFiumRenderFlag = priorityProductOption.JpgPDFiumRenderFlag;
			JpgFreeImageQuality = priorityProductOption.JpgFreeImageQuality;
			JpgFreeImageConverToColorBits = priorityProductOption.JpgFreeImageConverToColorBits;

			TiffMaxWidth = priorityProductOption.TiffMaxWidth;
			TiffMaxHeight = priorityProductOption.TiffMaxHeight;
			TiffWidthDPI = priorityProductOption.TiffWidthDPI;
			TiffHeightDPI = priorityProductOption.TiffHeightDPI;
			TiffPdfiumRenderFlag = priorityProductOption.TiffPdfiumRenderFlag;
			TiffFreeImageColorDepth = priorityProductOption.TiffFreeImageColorDepth;
			TiffFreeImageCompression = priorityProductOption.TiffFreeImageCompression;
			TiffFreeImageConverToColorBits = priorityProductOption.TiffFreeImageConverToColorBits;
			TiffIsUsingColorCorrect = priorityProductOption.TiffIsUsingColorCorrect;
		}

		//public uint GetJpgMaxWidth()
		//{
		//	uint.TryParse(JpgMaxWidth, out uint result);

		//	return result;
		//}

	}

	public enum PDFiumRenderFlagEnum
	{
		[EnumMember(Value = "NONE")]
		NONE = 0,
		[EnumMember(Value = "FORCEHALFTONE")]
		FORCEHALFTONE = 1,
		[EnumMember(Value = "GRAYSCALE")]
		GRAYSCALE,
		[EnumMember(Value = "GREYSCALE")]
		GREYSCALE

	}

	public enum FreeImageQualityEnum
	{
		[EnumMember(Value = "SUPERB")]
		SUPERB = 0,
		[EnumMember(Value = "BAD")]
		BAD = 1,
		[EnumMember(Value = "AVERAGE")]
		AVERAGE,
		[EnumMember(Value = "NORMAL")]
		NORMAL,
		[EnumMember(Value = "GOOD")]
		GOOD
	}

	public enum FreeImageConverToColorBitsEnum
	{
		[EnumMember(Value = "BITS24")]
		BITS24 = 0,
		[EnumMember(Value = "GREYSCALE")]
		GREYSCALE = 1,
		[EnumMember(Value = "GRAYSCALE")]
		GRAYSCALE,
		[EnumMember(Value = "BITS8")]
		BITS8,
		[EnumMember(Value = "BITS16")]
		BITS16
	}

	public enum FreeImageColorDepthEnum
	{
		[EnumMember(Value = "NONE")]
		NONE = 0,
		[EnumMember(Value = "GREYSCALE_BPPDITHER")]
		GREYSCALE_BPPDITHER = 1
	}

	public enum FreeImageCompressionEnum
	{
		[EnumMember(Value = "NONE")]
		NONE = 0,
		[EnumMember(Value = "FAX_LZW")]
		FAX_LZW = 1
	}



}
