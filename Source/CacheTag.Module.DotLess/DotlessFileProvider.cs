using System;
using System.Linq;
using System.Text;
using System.Web.Caching;
using CacheTag.Core.Filesystem;

namespace CacheTag.Module.DotLess
{
	public class DotlessFileProvider : PhysicalFileProvider
	{
		private readonly Lazy<ParseResult> lazyResult;

		public override CacheDependency CacheDependency
		{
			get { return new CacheDependency(lazyResult.Value.FileDependencies.ToArray()); }
		}

		public override byte[] ReadContent()
		{
			return Encoding.UTF8.GetBytes(lazyResult.Value.Content);
		}

		public DotlessFileProvider(string path)
			: base(path)
		{
			lazyResult = new Lazy<ParseResult>(() => new DotlessParser().ParseFile(path));
		}
	}
}
