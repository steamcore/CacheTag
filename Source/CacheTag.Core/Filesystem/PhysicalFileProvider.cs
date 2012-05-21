using System;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace CacheTag.Core.Filesystem
{
	public class PhysicalFileProvider : IFileProvider
	{
		private readonly string path;

		public PhysicalFileProvider(string path)
		{
			this.path = path;
		}

		public virtual byte[] ReadContent()
		{
			var physicalPath = path;

			if (path.StartsWith("~"))
				physicalPath = HttpContext.Current.Server.MapPath(path);

			return File.ReadAllBytes(physicalPath);
		}

		public virtual DateTime? AbsoluteExpiration
		{
			get { return null; }
		}

		public virtual string AppRelativePath
		{
			get { return VirtualPathUtility.ToAbsolute(path, HostingEnvironment.ApplicationVirtualPath); }
		}

		public virtual CacheDependency CacheDependency
		{
			get { return new CacheDependency(HttpContext.Current.Server.MapPath(path)); }
		}
	}
}
