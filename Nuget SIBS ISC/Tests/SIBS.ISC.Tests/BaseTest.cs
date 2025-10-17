namespace SIBS.ISC.Tests
{
	using SIBS.ISC.Helpers;
	using SIBS.ISC.Options;

	public abstract class BaseTest
	{
		protected string GetBasePathImages()
		{
			return Path.Combine(AppContext.BaseDirectory, "Images");
		}

		protected string GetBasePathPDFs()
		{
			return Path.Combine(AppContext.BaseDirectory, "Files", "PDFs");
		}

		protected void CreateDirectory(string directoryPath)
		{
			if (string.IsNullOrWhiteSpace(directoryPath))
				return;

			if (Directory.Exists(directoryPath) == false)
				Directory.CreateDirectory(directoryPath);
		}

		protected void SaveFile(string filePath, string fileName, byte[] file)
		{
			if (string.IsNullOrWhiteSpace(filePath))
				return;

			File.WriteAllBytes(Path.Combine(filePath, fileName), file);
		}

		protected abstract ImageConverterOption GetImageConverterOption();

	}

	public class BaseSplitterTest : BaseTest
	{
		protected override ImageSplitterConverterOption GetImageConverterOption()
		{
			return new ImageSplitterConverterOption();
		}

		protected SpecificImageSplitterConverterOption GetSpecificImageConverterOption()
		{
			SpecificImageSplitterConverterOption specificImageSplitterConverterOption = new SpecificImageSplitterConverterOption();
			specificImageSplitterConverterOption.ProductType = 99;
			specificImageSplitterConverterOption.JpgMaxWidth = 816;
			specificImageSplitterConverterOption.JpgMaxHeight = 1154;
			specificImageSplitterConverterOption.JpgWidthDPI = 300;
			specificImageSplitterConverterOption.JpgHeightDPI = 300;
			specificImageSplitterConverterOption.JpgPDFiumRenderFlag = PDFiumRenderFlagEnum.NONE;
			specificImageSplitterConverterOption.JpgFreeImageQuality = FreeImageQualityEnum.NORMAL;
			specificImageSplitterConverterOption.JpgFreeImageConverToColorBits = FreeImageConverToColorBitsEnum.BITS24;

			specificImageSplitterConverterOption.TiffMaxWidth = 1080;
			specificImageSplitterConverterOption.TiffMaxHeight = 1920;
			specificImageSplitterConverterOption.TiffWidthDPI = 300;
			specificImageSplitterConverterOption.TiffHeightDPI = 300;
			specificImageSplitterConverterOption.TiffPdfiumRenderFlag = PDFiumRenderFlagEnum.NONE;
			specificImageSplitterConverterOption.TiffFreeImageConverToColorBits = FreeImageConverToColorBitsEnum.BITS24;
			specificImageSplitterConverterOption.TiffFreeImageColorDepth = FreeImageColorDepthEnum.GREYSCALE_BPPDITHER;
			specificImageSplitterConverterOption.TiffFreeImageCompression = FreeImageCompressionEnum.FAX_LZW;
			specificImageSplitterConverterOption.TiffIsUsingColorCorrect = false;

			return specificImageSplitterConverterOption;
		}

		protected void SaveImages(string outputFolder, string fileName, string filePathToSave)
		{
			var imageFile = ImageConverterHelper.ReadTargetFile(filePathToSave);
			if (imageFile is null)
				return;

			var savePDFsFiles = Path.Combine(GetBasePathImages(), "Tests", outputFolder);
			CreateDirectory(savePDFsFiles);

			SaveFile(savePDFsFiles, fileName, imageFile);
		}
	}

	public class BaseJoinerTest : BaseTest
	{
		protected override ImageJoinerConverterOption GetImageConverterOption()
		{
			return new ImageJoinerConverterOption();
		}
	}

	public class BaseByteArrayTest : BaseTest
	{
		protected override ImageByteArrayConverterOption GetImageConverterOption()
		{
			return new ImageByteArrayConverterOption();
		}
	}
}
