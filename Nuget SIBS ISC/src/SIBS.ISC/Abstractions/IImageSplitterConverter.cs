namespace SIBS.ISC.Abstractions
{
	using System.Collections.Generic;
	using System.IO;

	/// <summary>
	/// Abstraction image splitter converter
	/// </summary>
	public interface IImageSplitterConverter
	{
		IEnumerable<string> ConvertPdfToJpgs(Stream originalFile, int partitionMode);

		IEnumerable<string> ConvertPdfToTiffs(Stream originalFile, int partitionMode);

		IEnumerable<string> ConvertJpgToJpg(Stream originalFile, int partitionMode);

		IEnumerable<string> ConvertTiffToJpgs(Stream originalFile, int partitionMode);

		IEnumerable<string> ConvertJpgsToTiffs(Stream originalFile, int partitionMode);

		IEnumerable<string> ConvertJpgsToTiffs(IList<Stream> originalFiles, int partitionMode);

		IEnumerable<string> ConvertTiffToTiffs(Stream originalFile, int partitionMode);
	}
}
