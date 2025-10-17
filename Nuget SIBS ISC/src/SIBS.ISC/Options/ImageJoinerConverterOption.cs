namespace SIBS.ISC.Options
{
	public class ImageJoinerConverterOption : ImageConverterOption
	{
		public override void Merge(ImageConverterOption? priorityImageConverterOption)
		{
			if (priorityImageConverterOption is null)
				return;
		}
	}
}
