using System;
using System.IO;
using System.Web;
using System.Web.Caching;

namespace CacheTag.Core.Filesystem
{
	public class DefaultFileProvider : IFileProvider
	{
		private readonly Lazy<IFileProvider> actualFileProvider;

		public DefaultFileProvider(string resource)
		{
			actualFileProvider = new Lazy<IFileProvider>(() => GetFileProvider(resource));
		}

		/// <summary>
		/// The default implementation of VirtualPathProvider does not return a CacheDependency as expected,
		/// therefore use the PhysicalFileProvider when a matching file exists
		/// See: http://msdn.microsoft.com/en-us/library/system.web.hosting.virtualpathprovider.getcachedependency.aspx
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		private IFileProvider GetFileProvider(string resource)
		{
			if (resource.StartsWith("http://") || resource.StartsWith("https://"))
			{
				return new RemoteFileProvider(resource);
			}

			if (File.Exists(HttpContext.Current.Server.MapPath(resource)))
			{
				return new PhysicalFileProvider(resource);
			}

			return new VirtualPathFileProvider(resource);
		}

		public DateTime? AbsoluteExpiration
		{
			get { return actualFileProvider.Value.AbsoluteExpiration; }
		}

		public string AppRelativePath
		{
			get { return actualFileProvider.Value.AppRelativePath; }
		}

		public CacheDependency CacheDependency
		{
			get { return actualFileProvider.Value.CacheDependency; }
		}

		public byte[] ReadContent()
		{
			return actualFileProvider.Value.ReadContent();
		}
	}
}
