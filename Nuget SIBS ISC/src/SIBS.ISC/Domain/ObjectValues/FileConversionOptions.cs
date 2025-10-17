namespace SIBS.ISC.Domain.ObjectValues
{
	using System.Text;

	public struct FileConversionOptions
	{
		public FileConversionOptions(
			FileFormat from,
			FileFormat to,
			IFileNameGenerator nameGenerator,
			ImageConversionOptionsDetails imageConversionOptions,
			int productType,
			int partitionMode)
			: this()
		{
			From = from;
			To = to;
			NameGenerator = nameGenerator;
			ImageConversionOptions = imageConversionOptions;
			ProductType = productType;
			PartitionMode = partitionMode;
		}

		public FileFormat From { get; private set; }

		public FileFormat To { get; private set; }

		public IFileNameGenerator NameGenerator { get; private set; }

		public int ProductType { get; private set; }

		public int PartitionMode { get; private set; }

		public ImageConversionOptionsDetails ImageConversionOptions { get; private set; }
	}

	public class ImageConversionOptionsDetails
	{
		public uint? WidthDPI { get; set; }

		public uint? HeightDPI { get; set; }

		public uint? MaxWidth { get; set; }

		public uint? MaxHeight { get; set; }

		public bool AdjustDPI
		{ get { return WidthDPI.HasValue && HeightDPI.HasValue; } }

		public bool AdjustSize
		{ get { return MaxWidth.HasValue && MaxHeight.HasValue; } }

		public override string ToString()
		{
			StringBuilder output = new StringBuilder();
			output.AppendFormat("AdjustDPI? {0}", AdjustDPI);
			if (AdjustDPI)
			{
				output.AppendFormat("[WidthDPI - {0} | HeightDPI - {1}]", WidthDPI, HeightDPI);
			}

			output.AppendFormat("| AdjustSize? {0}", AdjustSize);
			if (AdjustSize)
			{
				output.AppendFormat("[MaxWidth - {0} | MaxHeight - {1}]", MaxWidth, MaxHeight);
			}
			return output.ToString();
		}
	}

	public interface IFileConverter<in T>
	{
		string[] Convert(T originalFile, FileConversionOptions options);
	}

	public interface IFileStreamConverter : IFileConverter<Stream>
	{
	}

	public interface IMultipleFileStreamConverter : IFileConverter<IEnumerable<Stream>>
	{
	}
}
