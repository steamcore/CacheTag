using System.Text;
using CacheTag.Core.Filesystem;

namespace CacheTag.Core.Resources
{
	public abstract class PlainTextFileResource<T> : BaseFileResource<T>, IPlainTextResource
		where T : IPlainTextResource
	{
		protected PlainTextFileResource(string path, string mimeType)
			: base(path, mimeType)
		{
		}

		protected PlainTextFileResource(string path, string mimeType, IFileProvider fileProvider)
			: base(path, mimeType, fileProvider)
		{
		}

		public string Content
		{
			get { return Encoding.UTF8.GetString(BinaryContent); }
		}
	}
}
