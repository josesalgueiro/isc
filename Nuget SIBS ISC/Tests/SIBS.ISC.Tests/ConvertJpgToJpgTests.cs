namespace SIBS.ISC.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using FluentAssertions;
	using SIBS.ISC.Domain.Convertions;
	using SIBS.ISC.Options;
	using Xunit;

	public  class ConvertJpgToJpgTests : BaseSplitterTest
	{
		private SpecificImageSplitterConverterOption ConverterOptionJpg(int productType, /*int partitionMode,*/
			int jpgMaxWidth, int jpgMaxHeight, uint jpgWidthDPI, uint jpgHeightDPI,
			PDFiumRenderFlagEnum jpgPDFiumRenderFlag, FreeImageQualityEnum jpgFreeImageQuality, FreeImageConverToColorBitsEnum jpgFreeImageConverToColorBits)
		{
			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = new SpecificImageSplitterConverterOption
			{
				ProductType = productType,
				//PageSheetPartitionMode = partitionMode,
				JpgMaxWidth = jpgMaxWidth,
				JpgMaxHeight = jpgMaxHeight,
				JpgWidthDPI = jpgWidthDPI,
				JpgHeightDPI = jpgHeightDPI,
				JpgPDFiumRenderFlag = jpgPDFiumRenderFlag,
				JpgFreeImageQuality = jpgFreeImageQuality,
				JpgFreeImageConverToColorBits = jpgFreeImageConverToColorBits
			};

			return specificImageSplitterConverterOption;
		}

		[Fact]
		public void ConvertJpgToJpg_Valid_ConvertJpgToJpg()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionJpg(99, 816, 1154, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.NORMAL, FreeImageConverToColorBitsEnum.BITS24);

			var tiffContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "JPGs", "23311100000021_1.jpg")));
			ConvertJpgToJpg convertJpgToJpg = new ConvertJpgToJpg(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertJpgToJpg.StartConvertJpgToJpg(tiffContent, 0);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("JPGs", $"JpgToJpg_Test01_{imageIndex}.jpg", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

	}
}
