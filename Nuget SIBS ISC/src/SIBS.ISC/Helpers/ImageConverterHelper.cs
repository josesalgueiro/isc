namespace SIBS.ISC.Helpers
{
	public static class ImageConverterHelper
	{
		/// <summary>
		/// Reads the target file from directory path and full file name with extension.
		/// </summary>
		/// <param name="fullFilePath">The full file path.</param>
		/// <param name="fullFileName">Full name of the file with extension.</param>
		/// <returns>Return a nullable byte array file.</returns>
		public static byte[]? ReadTargetFile(string fullFilePath, string fullFileName)
		{
			return ReadTargetFile(Path.Combine(fullFilePath, fullFileName));
		}

		/// <summary>
		/// Reads the target file from directory path and full file name with extension.
		/// </summary>
		/// <param name="fullPathFileName">The full file path and full file name with extension.</param>
		/// <returns>Return a nullable byte array file.</returns>
		public static byte[]? ReadTargetFile(string fullPathFileName)
		{
			if (File.Exists(fullPathFileName) == false)
				return null;

			return File.ReadAllBytes(fullPathFileName);
		}

		/// <summary>
		/// Reads the targets files from directory path.
		/// </summary>
		/// <param name="fullFilesPath">The full files path.</param>
		/// <returns>Return a collection of byte array files.</returns>
		public static IEnumerable<byte[]> ReadTargetFiles(string fullFilesPath)
		{
			List<byte[]> result = new();

			if (Directory.Exists(fullFilesPath) == false)
				return result;

			// Get all files on directory
			var filesInDirectory = Directory.GetFiles(fullFilesPath);
			foreach (var file in filesInDirectory)
			{
				if (File.Exists(file) == false)
					continue;

				result.Add(File.ReadAllBytes(file));
			}

			return result;
		}

		/// <summary>
		/// Reads the targets files from collection of directory paths.
		/// </summary>
		/// <param name="fullFilesPath">The collection of files path.</param>
		/// <returns>Return a collection of byte array files.</returns>
		public static IEnumerable<byte[]> ReadTargetFiles(IEnumerable<string> fullFilesPath)
		{
			List<byte[]> result = new();

			// Get all files on directory
			foreach (var file in fullFilesPath)
			{
				if (File.Exists(file) == false)
					continue;

				result.Add(File.ReadAllBytes(file));
			}

			return result;
		}
	}
}
