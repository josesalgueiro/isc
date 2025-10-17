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

	public  class ConvertTiffToJpgsTests : BaseSplitterTest
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
		public void ConvertTiffToJpgs_Valid_ConvertTiffToJpgs()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionJpg(99, 816, 1154, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.NORMAL, FreeImageConverToColorBitsEnum.BITS24);

			var tiffContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "TIFs", "ERRO_23286800024601.tiff")));
			ConvertTiffToJpgs convertTiffToJpgs = new ConvertTiffToJpgs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertTiffToJpgs.StartConvertTiffToJpgs(tiffContent, 0);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("JPGs", $"TIFFtoJPGs_Test01_{imageIndex}.jpg", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertTiffToJpgs_Valid_ConvertTiffToJpgs_PartitiomMode1_ImageQualityNormal_ColorBits24()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionJpg(99, 816, 1154, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.NORMAL, FreeImageConverToColorBitsEnum.BITS24);

			var tiffContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "TIFs", "ERRO_23286800024601.tiff")));
			ConvertTiffToJpgs convertTiffToJpgs = new ConvertTiffToJpgs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertTiffToJpgs.StartConvertTiffToJpgs(tiffContent, 1);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("JPGs", $"TIFFtoJPGs_Test02_{imageIndex}.jpg", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertTiffToJpgs_Valid_ConvertTiffToJpgs_PartitiomMode1_ImageQualityGood_ColorBitsGREYSCALE()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionJpg(99, 816, 1154, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.GOOD, FreeImageConverToColorBitsEnum.GREYSCALE);

			var tiffContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "TIFs", "ERRO_23286800024601.tiff")));
			ConvertTiffToJpgs convertTiffToJpgs = new ConvertTiffToJpgs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertTiffToJpgs.StartConvertTiffToJpgs(tiffContent, 1);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("JPGs", $"TIFFtoJPGs_Test03_{imageIndex}.jpg", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertTiffToJpgs_Valid_ConvertTiffToJpgs_PartitiomMode1_ImageQualitySuperb_ColorBits8()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionJpg(99, 816, 1154, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.SUPERB, FreeImageConverToColorBitsEnum.BITS8);

			var tiffContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "TIFs", "ERRO_23286800024601.tiff")));
			ConvertTiffToJpgs convertTiffToJpgs = new ConvertTiffToJpgs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertTiffToJpgs.StartConvertTiffToJpgs(tiffContent, 1);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("JPGs", $"TIFFtoJPGs_Test04_{imageIndex}.jpg", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertTiffToJpgs_Valid_ConvertTiffToJpgs_PartitiomMode1_ImageQualityGood_ColorBits16()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionJpg(99, 816, 1154, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.GOOD, FreeImageConverToColorBitsEnum.BITS16);

			var tiffContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "TIFs", "ERRO_23286800024601.tiff")));
			ConvertTiffToJpgs convertTiffToJpgs = new ConvertTiffToJpgs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertTiffToJpgs.StartConvertTiffToJpgs(tiffContent, 1);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("JPGs", $"TIFFtoJPGs_Test05_{imageIndex}.jpg", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ConvertTiffToJpgs_Valid_ConvertTiffToJpgs_PartitiomMode1_ImageQualityBad_ColorBits16()
		{
			bool isValid = true;

			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = ConverterOptionJpg(99, 816, 1154, 300, 300,
				PDFiumRenderFlagEnum.NONE, FreeImageQualityEnum.BAD, FreeImageConverToColorBitsEnum.BITS16);

			var tiffContent = new MemoryStream(File.ReadAllBytes(Path.Combine(GetBasePathImages(), "TIFs", "ERRO_23286800024601.tiff")));
			ConvertTiffToJpgs convertTiffToJpgs = new ConvertTiffToJpgs(specificImageSplitterConverterOption);
			IEnumerable<string> filesPath = convertTiffToJpgs.StartConvertTiffToJpgs(tiffContent, 1);

			int imageIndex = 1;
			foreach (var filePath in filesPath)
			{
				if (File.Exists(filePath) == false)
				{
					isValid = false;
				}

				SaveImages("JPGs", $"TIFFtoJPGs_Test06_{imageIndex}.jpg", filePath);

				imageIndex++;
			}

			isValid.Should().BeTrue();
		}


	}
}
