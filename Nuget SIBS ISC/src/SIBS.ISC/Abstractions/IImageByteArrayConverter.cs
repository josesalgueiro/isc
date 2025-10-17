namespace SIBS.ISC.Abstractions
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	public interface IImageByteArrayConverter
	{
		byte[]? ConvertToTiff(string fullPathFileName);
		public byte[]? ConvertToJpg(string fullPathFileName);
		public byte[]? ConvertToJpg(byte[] byteArray);
		public byte[]? ConvertToPng(string fullPathFileName);
		public byte[]? ConvertToBmp(string fullPathFileName);
		public byte[]? ConvertToGif(string fullPathFileName);
	}
}
