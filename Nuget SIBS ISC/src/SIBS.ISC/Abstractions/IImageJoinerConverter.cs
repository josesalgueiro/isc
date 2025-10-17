namespace SIBS.ISC.Abstractions
{
	/// <summary>
	/// Abstraction to join images.
	/// </summary>
	public interface IImageJoinerConverter
	{
		/// <summary>
		/// Converts a list of images in byte array collection to a byte array PDF file.
		/// </summary>
		/// <param name="imagesBytes">Images byte array collection.</param>
		/// <returns>Return a PDF byte array.</returns>
		byte[] ConvertImagesToPDF(IEnumerable<byte[]> imagesBytes);

		/// <summary>
		/// Converts a list of images in a directory to a byte array PDF file.
		/// </summary>
		/// <param name="fullFilesPath">The directory path.</param>
		/// <returns>Return a nullable PDF byte array.</returns>
		byte[]? ConvertImagesToPDF(string fullFilesPath);

		/// <summary>
		/// Converts a byte array image to a byte array PDF file.
		/// </summary>
		/// <param name="imageByte">Byte array image.</param>
		/// <returns>Return a PDF byte array.</returns>
		byte[] ConvertImageToPDF(byte[] imageByte);

		/// <summary>
		/// Converts an image file in a specified directory to a PDF byte array.
		/// </summary>
		/// <param name="fullFilePath">The directory path containing the image.</param>
		/// <param name="fullFileName">The image file name with extension.</param>
		/// <returns>Returns a nullable PDF byte array representing the image as a PDF, or null if the file is not found.</returns>
		byte[]? ConvertImageToPDF(string fullFilePath, string fullFileName);

		/// <summary>
		/// Converts an image file specified by its full path to a PDF byte array.
		/// </summary>
		/// <param name="fullPathFileName">The full path of the image file including name and extension.</param>
		/// <returns>Returns a nullable PDF byte array representing the image as a PDF, or null if the file is not found.</returns>
		byte[]? ConvertImageToPDF(string fullPathFileName);

		/// <summary>
		/// Converts multiple image files specified by their full paths to a single PDF byte array.
		/// </summary>
		/// <param name="fullFilesPath">A collection of full file paths for the images.</param>
		/// <returns>Returns a nullable PDF byte array representing the combined images as a PDF, or null if no files are found.</returns>
		byte[]? ConvertImagesToPDF(IEnumerable<string> fullFilesPath);
	}
}
