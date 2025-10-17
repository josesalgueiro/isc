namespace SIBS.ISC.Tests
{
	using FluentAssertions;
	using SIBS.ISC.Abstractions;
	using SIBS.ISC.Domain;
	using SIBS.ISC.Domain.Factories;
	using SIBS.ISC.Helpers;
	using Xunit;

	public  class ImageByteArrayTests : BaseByteArrayTest
	{
		[Fact]
		public void ImageByteArrayConverter_Valid_ConvertJpgtoTiff()
		{
			bool isValid = true;
			IImageByteArrayConverterFactory imageByteArrayConverterFactory = new ImageByteArrayConverterFactory();

			var imageByteArrayConverter = imageByteArrayConverterFactory.CreateByteArrayConverter(base.GetImageConverterOption());

			var imagePathFullName = Path.Combine(GetBasePathImages(), "JPGs", "23311100000021_1.jpg");

			//var file = ImageConverterHelper.ReadTargetFile(imagePathFullName);

			
			//if (file is null)
			//{
			//	isValid = false;
			//}
			//else
			//{
				var jpgFile = imageByteArrayConverter.ConvertToTiff(imagePathFullName);
				if (jpgFile is null)
				{
					isValid = false;
				}
				else
				{
					var saveTiffFile = Path.Combine(GetBasePathImages(), "Tests", "TIFFs");
					CreateDirectory(saveTiffFile);

					SaveFile(saveTiffFile, $"JPGtoTIFF_byteArray.tiff", jpgFile);
				}
			//}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ImageByteArrayConverter_Valid_ConvertTifftoJpg()
		{
			bool isValid = true;
			IImageByteArrayConverterFactory imageByteArrayConverterFactory = new ImageByteArrayConverterFactory();

			var imageByteArrayConverter = imageByteArrayConverterFactory.CreateByteArrayConverter(base.GetImageConverterOption());

			var imagePathFullName = Path.Combine(GetBasePathImages(), "TIFs", "23311100000021_1.tif");

			//var file = ImageConverterHelper.ReadTargetFile(imagePathFullName);


			//if (file is null)
			//{
			//	isValid = false;
			//}
			//else
			//{
				var tiffFile = imageByteArrayConverter.ConvertToJpg(imagePathFullName);
				if (tiffFile is null)
				{
					isValid = false;
				}
				else
				{
					var saveJpgFile = Path.Combine(GetBasePathImages(), "Tests", "JPGs");
					CreateDirectory(saveJpgFile);

					SaveFile(saveJpgFile, $"TIFFtoJPG_byteArray.jpg", tiffFile);
				}
			//}

			isValid.Should().BeTrue();
		}

		[Fact]
		public void ImageByteArrayConverter_Valid_ConvertJpgtoGif()
		{
			bool isValid = true;
			IImageByteArrayConverterFactory imageByteArrayConverterFactory = new ImageByteArrayConverterFactory();

			var imageByteArrayConverter = imageByteArrayConverterFactory.CreateByteArrayConverter(base.GetImageConverterOption());

			var imagePathFullName = Path.Combine(GetBasePathImages(), "JPGs", "23311100000021_1.jpg");

			//var file = ImageConverterHelper.ReadTargetFile(imagePathFullName);


			//if (file is null)
			//{
			//	isValid = false;
			//}
			//else
			//{
			var gifFile = imageByteArrayConverter.ConvertToGif(imagePathFullName);
			if (gifFile is null)
			{
				isValid = false;
			}
			else
			{
				var saveGifFile = Path.Combine(GetBasePathImages(), "Tests", "GIFs");
				CreateDirectory(saveGifFile);

				SaveFile(saveGifFile, $"JPGtoGIF_byteArray.gif", gifFile);
			}
			//}

			isValid.Should().BeTrue();
		}

	}
}
