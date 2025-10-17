namespace SIBS.ISC.Domain.Factories
{
	using SIBS.ISC.Exceptions;
	using SIBS.ISC.Helpers;
	using SIBS.ISC.Options;

	public class BaseFactory
	{
		protected const string ExtensionNameConfigFile = "sibs.isc.json";

		protected T? CreateMergedImageConverterOption<T>(string? configFileName, string? relativePath = null)
			where T : ImageConverterOption
		{
			var imageConverterConfigObject = ReadImageConverterOption<T>(configFileName, relativePath);

			// Validate if exist a file CQ.sibs.isc.json and override option file
			var priorityImageConverterConfigObject = ReadImageConverterOption<T>(configFileName, relativePath, "CQ");

			if (imageConverterConfigObject is not null && priorityImageConverterConfigObject is null)
				return imageConverterConfigObject;

			if (imageConverterConfigObject is null && priorityImageConverterConfigObject is not null)
				return priorityImageConverterConfigObject;

			if (imageConverterConfigObject is not null && priorityImageConverterConfigObject is not null)
				imageConverterConfigObject.Merge(priorityImageConverterConfigObject);

			return imageConverterConfigObject;
		}

		private T? ReadImageConverterOption<T>(string? configFileName, string? relativePath = null, string? priorityExtension = null)
			where T : ImageConverterOption
		{
			// Get base directory for image converter config file (project root).
			var configFilePath = AppContext.BaseDirectory;
			if (relativePath is not null)
				configFilePath = Path.Combine(configFilePath, relativePath);

			if (Directory.Exists(configFilePath) == false)
				return null;

			// String contains the full path with configuration file name.
			string? configFileFullPath = null;

			T? imageConverterConfigObject = null;

			// Get all files on directory
			var filesInDirectory = Directory.GetFiles(configFilePath);

			if (configFileName is not null)
			{
				// Create the file full name
				var configFileFullName = $"{configFileName}.{ExtensionNameConfigFile}";
				if (string.IsNullOrWhiteSpace(priorityExtension) == false)
					configFileFullName = $"{configFileName}.{priorityExtension}.{ExtensionNameConfigFile}";

				// Get the full path for the file
				if (filesInDirectory.Any(f => f.Contains(configFileFullName)))
					configFileFullPath = filesInDirectory.FirstOrDefault(f => f.Contains(configFileFullName));
			}
			else
			{
				// Create the priority file full name
				var priorityExtensionNameConfigFile = ExtensionNameConfigFile;
				if (string.IsNullOrWhiteSpace(priorityExtension) == false)
					priorityExtensionNameConfigFile = $".{priorityExtension}.{ExtensionNameConfigFile}";

				// Get the full path for the file
				configFileFullPath = filesInDirectory.FirstOrDefault(f => f.Contains(priorityExtensionNameConfigFile));
			}

			// Read the configuration file.
			if (configFileFullPath is not null)
			{
				var configFile = File.ReadAllText(configFileFullPath);
				imageConverterConfigObject = SerializationExtension.JsonDeserialize<T>(configFile);
			}
			else
			{
				// The priority file is not mandatory.
				if (string.IsNullOrWhiteSpace(priorityExtension))
					throw new ShouldNotHappenIscException($"No SIBS Image Converter Option configuration json file found for path: '{configFileFullPath}' with name '{configFileName}'.");
			}

			return imageConverterConfigObject;
		}
	}
}
