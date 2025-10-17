namespace SIBS.ISC.Abstractions
{
	using SIBS.ISC.Options;

	/// <summary>
	/// Factory to create the abstraction to join images.
	/// </summary>
	public interface IImageJoinerConverterFactory
	{
		/// <summary>
		/// Create the abstraction to use the image joiner converter.
		/// </summary>
		/// <param name="imageJoinerConverterOption">The options to configurate the image joiner converter.</param>
		/// <returns>The abstraction interface ISpecificLog.</returns>
		IImageJoinerConverter CreateImageJoinerConverter(ImageJoinerConverterOption imageJoinerConverterOption);

		/// <summary>
		/// Create the abstraction to use the image joiner converter.
		/// </summary>
		/// <param name="imageJoinerConverterConfigFileName">The config file to configurate the image joiner converter.</param>
		/// <param name="relativePath">The relative path to find the config file.</param>
		/// <returns>The abstraction interface ISpecificLog.</returns>
		IImageJoinerConverter CreateImageJoinerConverter(string imageJoinerConverterConfigFileName, string? relativePath = null);
	}
}
