namespace SIBS.ISC.Options
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	public class ImageByteArrayConverterOption : ImageConverterOption
	{
		public override void Merge(ImageConverterOption? priorityImageConverterOption)
		{
			if (priorityImageConverterOption is null)
				return;
		}
	}
}
