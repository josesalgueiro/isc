namespace SIBS.ISC.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using SIBS.ISC.Domain.ObjectValues;

	public static class FileUtils
	{
		public static void MoveFile(string pathFrom, string pathTo, bool preserveExisting)
		{
			if (File.Exists(pathTo))
			{
				if (preserveExisting)
					File.Move(pathTo, $"{pathTo}_{DateTime.Now.Ticks}");
				else
					File.Delete(pathTo);
			}

			File.Copy(pathFrom, pathTo);
		}

		public static string[] MoveGeneratedFiles(string[] filesToMove, FileConversionOptions options)
		{
			var finalFiles = new string[filesToMove.Length];
			if (filesToMove.Length == 1)
			{
				string filename = options.NameGenerator.GetFileName(options.To, null);
				finalFiles[0] = filename;
				MoveFile(filesToMove[0], filename, true);
			}
			else
			{
				var page = 1;
				foreach (var filePath in filesToMove)
				{
					string filename = options.NameGenerator.GetFileName(options.To, page);
					finalFiles[page - 1] = filename;
					MoveFile(filesToMove[page - 1], filename, true);

					page++;
				}
			}
			return finalFiles;
		}

		public static string GenerateFileName(DirectoryInfo saveOnDir, string fileName, FileFormat format, int? page)
		{
			if (page != null)
				return Path.Combine(saveOnDir.FullName, $"{Path.GetFileNameWithoutExtension(fileName)}_{page}.{format.AsString()}");
			else
				return Path.Combine(saveOnDir.FullName, $"{Path.GetFileNameWithoutExtension(fileName)}.{format.AsString()}");
		}

		public static string AsString(this FileFormat format)
		{
			return Enum.GetName(typeof(FileFormat), format).ToLower();
		}

		public static void GarbageCollector(Bitmap img)
		{
			img.Dispose();
		}

		public static void GarbageCollector(Bitmap[] imgs)
		{
			for (var i = 0; i < 2; i++)
			{
				imgs[i].Dispose();
			}
		}

		public static void GarbageCollector(Image img)
		{
			img.Dispose();
		}

		public static void GarbageCollector(Image[] imgs)
		{
			for (var i = 0; i < 2; i++)
			{
				imgs[i].Dispose();
			}
		}

		public static void GarbageCollector(Graphics img)
		{
			img.Dispose();
		}

		public static Tuple<int, int> ResizeFromOriginalImage(IList<SizeF> docPageSizes, int pageNumber, int maxWidth, int maxHeight)
		{
			//Calc Percentagem Imagem
			var calcWidthPercent = maxWidth / (float)docPageSizes[pageNumber].Width;
			var calcHeightPercent = maxHeight / (float)docPageSizes[pageNumber].Height;
			var perc = Math.Min(calcWidthPercent, calcHeightPercent);
			var newWidth = (int)(docPageSizes[pageNumber].Width * perc);
			var newHeight = (int)(docPageSizes[pageNumber].Height * perc);

			return Tuple.Create(newWidth, newHeight);
		}
	}
}
