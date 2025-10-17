namespace SIBS.ISC.Domain
{
	//using iText.IO.Image;
	//using iText.Kernel.Pdf;
	//using iText.Kernel.Utils;
	//using iText.Layout;
	//using iText.Layout.Element;
	//using iText.Layout.Properties;
	using PdfSharpCore.Drawing;
	using PdfSharpCore.Pdf;
	using SIBS.ISC.Abstractions;
	using SIBS.ISC.Helpers;

	public class ImageJoinerConverter : IImageJoinerConverter
	{
		public static bool IsTiff(byte[] data)
		{
			if (data.Length < 4)
				return false;

			bool isLittleEndian = data[0] == 73 && data[1] == 73;
			bool isBigEndian = data[0] == 77 && data[1] == 77;

			if (isLittleEndian)
				return data[2] == 42 && data[3] == 0;
			else if (isBigEndian)
				return data[2] == 0 && data[3] == 42;
			else
				return false;
		}

		public byte[] ConvertImagesToPDF(IEnumerable<byte[]> imagesBytes)
		{
			// Create a new PDF document
			PdfDocument document = new PdfDocument();

			// Define margins
			const double margin = 50; // 50 point margin on each side

			foreach (var imageBytes in imagesBytes)
			{
				// Create an empty page in this document
				PdfPage page = document.AddPage();

				//If tiff image convert to Jpg because PdfSharp no support Tiffs
				byte[] imageBytesToAdd;
				if (IsTiff(imageBytes))
				{
					ImageByteArrayConverter imageByteArrayConverter = new ImageByteArrayConverter();
					imageBytesToAdd = imageByteArrayConverter.ConvertTo(imageBytes, FreeImageAPI.FREE_IMAGE_FORMAT.FIF_JPEG);
				}
				else
				{
					imageBytesToAdd = imageBytes;
				}

				// Create XImage from byte array
				XImage img;
				using (MemoryStream ms = new MemoryStream(imageBytesToAdd))
				{
					img = XImage.FromStream(() => new MemoryStream(ms.ToArray()));
				}

				// Get an XGraphics object for drawing
				XGraphics gfx = XGraphics.FromPdfPage(page);

				// Calculate scale factors to fit the image within the page
				double scaleX = (page.Width - 2 * margin) / img.PixelWidth;
				double scaleY = (page.Height - 2 * margin) / img.PixelHeight;
				double scale = Math.Min(scaleX, scaleY);

				// Calculate new width and height
				double width = img.PixelWidth * scale;
				double height = img.PixelHeight * scale;

				// Draw the image centered on the page with margins
				//gfx.DrawImage(img, (page.Width - width) / 2, (page.Height - height) / 2, width, height);

				// Draw the image at the top of the page with margins
				gfx.DrawImage(img, (page.Width - width) / 2, margin, width, height);
			}

			// Save the document to a MemoryStream
			MemoryStream pdfStream = new MemoryStream();
			document.Save(pdfStream, false);

			// Return the PDF as a byte array
			return pdfStream.ToArray();
		}

		public byte[]? ConvertImagesToPDF(string fullFilesPath)
		{
			var files = ImageConverterHelper.ReadTargetFiles(fullFilesPath);
			if (files is null || files.Any() == false)
				return null;

			return ConvertImagesToPDF(imagesBytes: files);
		}

		public byte[] ConvertImageToPDF(byte[] imageByte)
		{
			return ConvertImagesToPDF(new List<byte[]>() { imageByte });
		}

		public byte[]? ConvertImageToPDF(string fullFilePath, string fullFileName)
		{
			return ConvertImageToPDF(Path.Combine(fullFilePath, fullFileName));
		}

		public byte[]? ConvertImageToPDF(string fullPathFileName)
		{
			var file = ImageConverterHelper.ReadTargetFile(fullPathFileName);
			if (file is null)
				return null;

			List<byte[]> files = new List<byte[]>() { file };

			return ConvertImagesToPDF(files);
		}

		public byte[]? ConvertImagesToPDF(IEnumerable<string> fullFilesPath)
		{
			var files = ImageConverterHelper.ReadTargetFiles(fullFilesPath);
			if (files is null || files.Any() == false)
				return null;

			return ConvertImagesToPDF(files);
		}
	}
}
