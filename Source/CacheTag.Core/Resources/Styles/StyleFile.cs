using CacheTag.Core.Filesystem;

namespace CacheTag.Core.Resources.Styles
{
	public class StyleFile : PlainTextFileResource<IStyleResource>, IStyleResource
	{
		public StyleFile(string path)
			: base(path, "text/css")
		{
		}

		public StyleFile(string path, IFileProvider fileProvider)
			: base(path, "text/css", fileProvider)
		{
		}
	}
}
