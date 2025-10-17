namespace SIBS.ISC.Abstractions
{
	using SIBS.ISC.Options;

	/// <summary>
	/// Factory to create the abstraction to use image splitter converter.
	/// </summary>
	public interface IImageSplitterConverterFactory
	{
		/// <summary>
		/// Create the abstraction to use the image splitter converter.
		/// </summary>
		/// <returns>The abstraction interface IImageSplitterConverter.</returns>
		IImageSplitterConverter CreateImageSplitterConverter();

		/// <summary>
		/// Create the abstraction to use the image splitter converter.
		/// </summary>
		/// <param name="specificImageSplitterConverterOption">The options to configurate the image splitter converter.</param>
		/// <returns>The abstraction interface IImageSplitterConverter.</returns>
		IImageSplitterConverter CreateImageSplitterConverter(SpecificImageSplitterConverterOption specificImageSplitterConverterOption);

		/// <summary>
		/// Create the abstraction to use the image splitter converter.
		/// </summary>
		/// <param name="productType">The product type.</param>
		/// <param name="imageSplitterConverterConfigFileName">The config file to configurate the image splitter converter.</param>
		/// <param name="relativePath">The config file relative path.</param>
		/// <returns>The abstraction interface IImageSplitterConverter.</returns>
		IImageSplitterConverter CreateImageSplitterConverter(int productType, string imageSplitterConverterConfigFileName, string? relativePath = null);
	}
}
