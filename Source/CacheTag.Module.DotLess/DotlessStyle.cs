using CacheTag.Core.Resources.Styles;

namespace CacheTag.Module.DotLess
{
	public class DotlessStyle : StyleFile
	{
		public DotlessStyle(string path)
			: base(path, new DotlessFileProvider(path))
		{
		}
	}
}
