namespace SIBS.ISC.Tests
{
	using FluentAssertions;
	using SIBS.ISC.Abstractions;
	using SIBS.ISC.Domain.Factories;
	using SIBS.ISC.Exceptions;
	using Xunit;

	public class SpecificJoinerFactoryTests : BaseJoinerTest
	{
		[Fact]
		public void ImageJoinerConverterFactory_Valid_GetImageJoinerConverterNoImageJoinerConverterOptionObject()
		{
			bool hasThrowException = false;
			IImageJoinerConverterFactory imageJoinerConverterFactory = new ImageJoinerConverterFactory();

			try
			{
				IImageJoinerConverter joinerConverter = imageJoinerConverterFactory.CreateImageJoinerConverter(imageJoinerConverterOption: null!);
			}
			catch (ShouldNotHappenIscException)
			{
				hasThrowException = true;
			}
			catch (ImageJoinerConverterException)
			{
				hasThrowException = true;
			}

			// Get the json config file
			hasThrowException.Should().BeFalse();
		}

		[Fact]
		public void ImageJoinerConverterFactory_Fail_GetImageJoinerConverterNoConfigFileNameAndNoConfigFile()
		{
			bool hasThrowException = false;
			IImageJoinerConverterFactory imageJoinerConverterFactory = new ImageJoinerConverterFactory();

			try
			{
				IImageJoinerConverter joinerConverter = imageJoinerConverterFactory.CreateImageJoinerConverter(imageJoinerConverterConfigFileName: null!, relativePath: "relativePath");
			}
			catch (ShouldNotHappenIscException)
			{
				hasThrowException = true;
			}
			catch (ImageJoinerConverterException)
			{
				hasThrowException = true;
			}

			// Get the json config file
			hasThrowException.Should().BeTrue();
		}

		[Fact]
		public void ImageJoinerConverterFactory_Valid_GetImageJoinerConverterNoConfigFileName()
		{
			bool hasThrowException = false;
			IImageJoinerConverterFactory imageJoinerConverterFactory = new ImageJoinerConverterFactory();

			try
			{
				IImageJoinerConverter joinerConverter = imageJoinerConverterFactory.CreateImageJoinerConverter(imageJoinerConverterConfigFileName: null!);
			}
			catch (ShouldNotHappenIscException)
			{
				hasThrowException = true;
			}
			catch (ImageJoinerConverterException)
			{
				hasThrowException = true;
			}

			// Get the json config file
			hasThrowException.Should().BeFalse();
		}

		[Fact]
		public void ImageJoinerConverterFactory_Fail_GetImageJoinerConverterWrongConfigFileName()
		{
			bool hasThrowException = false;
			IImageJoinerConverterFactory imageJoinerConverterFactory = new ImageJoinerConverterFactory();

			try
			{
				IImageJoinerConverter joinerConverter = imageJoinerConverterFactory.CreateImageJoinerConverter(imageJoinerConverterConfigFileName: "WrongFileName");
			}
			catch (ShouldNotHappenIscException)
			{
				hasThrowException = true;
			}
			catch (ImageJoinerConverterException)
			{
				hasThrowException = true;
			}

			// Get the json config file
			hasThrowException.Should().BeTrue();
		}

		[Fact]
		public void ImageJoinerConverterFactory_Valid_GetImageJoinerConverterCorrectConfigFileName()
		{
			bool hasThrowException = false;
			IImageJoinerConverterFactory imageJoinerConverterFactory = new ImageJoinerConverterFactory();

			try
			{
				IImageJoinerConverter joinerConverter = imageJoinerConverterFactory.CreateImageJoinerConverter(imageJoinerConverterConfigFileName: "iscConfig");
			}
			catch (ShouldNotHappenIscException)
			{
				hasThrowException = true;
			}
			catch (ImageJoinerConverterException)
			{
				hasThrowException = true;
			}

			// Get the json config file
			hasThrowException.Should().BeFalse();
		}

		[Fact]
		public void ImageJoinerConverterFactory_Valid_GetImageJoinerConverterCorrectConfigFileNameWithCQs()
		{
			bool hasThrowException = false;
			IImageJoinerConverterFactory imageJoinerConverterFactory = new ImageJoinerConverterFactory();

			try
			{
				IImageJoinerConverter joinerConverter = imageJoinerConverterFactory.CreateImageJoinerConverter(imageJoinerConverterConfigFileName: "iscConfig", relativePath: "CQs");
			}
			catch (ShouldNotHappenIscException)
			{
				hasThrowException = true;
			}
			catch (ImageJoinerConverterException)
			{
				hasThrowException = true;
			}

			// Get the json config file
			hasThrowException.Should().BeFalse();
		}
	}
}
