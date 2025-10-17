namespace SIBS.ISC.Tests
{
	using FluentAssertions;
	using SIBS.ISC.Abstractions;
	using SIBS.ISC.Domain.Factories;
	using SIBS.ISC.Exceptions;
	using Xunit;

	public class SpecificSplitterFactoryTests : BaseSplitterTest
	{
		[Fact]
		public void ImageSplitterConverterFactory_Valid_GetImageSplitterConverterNoImageSplitterConverterOptionObject()
		{
			bool hasThrowException = false;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			try
			{
				IImageSplitterConverter joinerConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(specificImageSplitterConverterOption: null!);
			}
			catch (ShouldNotHappenIscException)
			{
				hasThrowException = true;
			}
			catch (ImageSplitterConverterException)
			{
				hasThrowException = true;
			}

			// Get the json config file
			hasThrowException.Should().BeFalse();
		}

		[Fact]
		public void ImageSplitterConverterFactory_Fail_GetImageSplitterConverterNoConfigFileNameAndNoConfigFile()
		{
			bool hasThrowException = false;
			int productType = 1;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			try
			{
				IImageSplitterConverter joinerConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(productType, imageSplitterConverterConfigFileName: null!, relativePath: "relativePath");
			}
			catch (ShouldNotHappenIscException)
			{
				hasThrowException = true;
			}
			catch (ImageSplitterConverterException)
			{
				hasThrowException = true;
			}

			// Get the json config file
			hasThrowException.Should().BeTrue();
		}

		[Fact]
		public void ImageSplitterConverterFactory_Valid_GetImageSplitterConverterNoConfigFileName()
		{
			bool hasThrowException = false;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			try
			{
				IImageSplitterConverter joinerConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(specificImageSplitterConverterOption: null!);
			}
			catch (ShouldNotHappenIscException)
			{
				hasThrowException = true;
			}
			catch (ImageSplitterConverterException)
			{
				hasThrowException = true;
			}

			// Get the json config file
			hasThrowException.Should().BeFalse();
		}

		[Fact]
		public void ImageSplitterConverterFactory_Fail_GetImageSplitterConverterWrongConfigFileName()
		{
			bool hasThrowException = false;
			int productType = 1;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			try
			{
				IImageSplitterConverter joinerConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(productType, imageSplitterConverterConfigFileName: "WrongFileName");
			}
			catch (ShouldNotHappenIscException)
			{
				hasThrowException = true;
			}
			catch (ImageSplitterConverterException)
			{
				hasThrowException = true;
			}

			// Get the json config file
			hasThrowException.Should().BeTrue();
		}

		[Fact]
		public void ImageSplitterConverterFactory_Valid_GetImageSplitterConverterCorrectConfigFileName()
		{
			bool hasThrowException = false;
			int productType = 2;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			try
			{
				IImageSplitterConverter joinerConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(productType, imageSplitterConverterConfigFileName: "iscConfig");
			}
			catch (ShouldNotHappenIscException)
			{
				hasThrowException = true;
			}
			catch (ImageSplitterConverterException)
			{
				hasThrowException = true;
			}

			// Get the json config file
			hasThrowException.Should().BeFalse();
		}

		[Fact]
		public void ImageSplitterConverterFactory_Valid_GetImageSplitterConverterCorrectConfigFileNameWithRelativePath()
		{
			bool hasThrowException = false;
			int productType = 2;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			try
			{
				IImageSplitterConverter joinerConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(productType, imageSplitterConverterConfigFileName: "iscConfig");
			}
			catch (ShouldNotHappenIscException)
			{
				hasThrowException = true;
			}
			catch (ImageSplitterConverterException)
			{
				hasThrowException = true;
			}
			catch (Exception ex)
			{
				hasThrowException = true;
			}

			hasThrowException.Should().BeFalse();
		}

		[Fact]
		public void ImageSplitterConverterFactory_Valid_GetImageSplitterConverterCorrectConfigFileNameWithCQs()
		{
			bool hasThrowException = false;
			int productType = 2;
			IImageSplitterConverterFactory imageSplitterConverterFactory = new ImageSplitterConverterFactory();

			try
			{
				IImageSplitterConverter joinerConverter = imageSplitterConverterFactory.CreateImageSplitterConverter(productType, imageSplitterConverterConfigFileName: "iscConfig", relativePath: "CQs");
			}
			catch (ShouldNotHappenIscException)
			{
				hasThrowException = true;
			}
			catch (ImageSplitterConverterException)
			{
				hasThrowException = true;
			}

			// Get the json config file
			hasThrowException.Should().BeFalse();
		}

	}
}
