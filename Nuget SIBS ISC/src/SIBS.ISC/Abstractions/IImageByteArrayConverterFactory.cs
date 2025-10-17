namespace SIBS.ISC.Abstractions
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using SIBS.ISC.Options;

	public interface IImageByteArrayConverterFactory
	{
		/// <summary>
		/// Create the abstraction to use the image byte array converter.
		/// </summary>
		/// <returns>The abstraction interface IImageByteArrayConverter.</returns>
		IImageByteArrayConverter CreateByteArrayConverter();

		/// <summary>
		/// Create the abstraction to use the image byte array converter.
		/// </summary>
		/// <param name="imageByteArrayConverterOption">The options to configurate the image byte array converter.</param>
		/// <returns>The abstraction interface IImageByteArrayConverter.</returns>
		IImageByteArrayConverter CreateByteArrayConverter(ImageByteArrayConverterOption imageByteArrayConverterOption);

		/// <summary>
		/// Create the abstraction to use the image byte array converter.
		/// </summary>
		/// <param name="imageConverterConfigFileName">The config file to configurate the image byte array converter.</param>
		/// <param name="relativePath">The config file relative path.</param>
		/// <returns>The abstraction interface IImageByteArrayConverter.</returns>
		IImageByteArrayConverter CreateByteArrayConverter(string imageConverterConfigFileName, string? relativePath = null);
	}
}
