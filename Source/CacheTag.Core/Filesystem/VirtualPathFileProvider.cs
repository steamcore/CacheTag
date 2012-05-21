using System;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using CacheTag.Core.Extensions;

namespace CacheTag.Core.Filesystem
{
	public class VirtualPathFileProvider : IFileProvider
	{
		private readonly string path;

		public VirtualPathFileProvider(string path)
		{
			this.path = path;
		}

		public DateTime? AbsoluteExpiration
		{
			get { return null; }
		}

		public string AppRelativePath
		{
			get { return VirtualPathUtility.ToAbsolute(path, HostingEnvironment.ApplicationVirtualPath); }
		}

		public CacheDependency CacheDependency
		{
			get { return HostingEnvironment.VirtualPathProvider.GetCacheDependency(path, new[] { path }, DateTime.UtcNow); }
		}

		public byte[] ReadContent()
		{
			using (var stream = HostingEnvironment.VirtualPathProvider.GetFile(path).Open())
			{
				return stream.ReadAllBytes();
			}
		}
	}
}
