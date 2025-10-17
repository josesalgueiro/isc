namespace SIBS.ISC.Domain.ObjectValues
{
	public interface IFileNameGenerator
	{
		string GetFileName(FileFormat format, int? page);
	}
}
