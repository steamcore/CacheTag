using System;
using System.Web.Caching;
using CacheTag.Core.Network;

namespace CacheTag.Core.Filesystem
{
	public class RemoteFileProvider : IFileProvider
	{
		private readonly string url;
		private readonly Lazy<RemoteFile> lazyResult;

		public RemoteFileProvider(string url)
		{
			this.url = url;
			lazyResult = new Lazy<RemoteFile>(() => HttpUtility.AsyncDownload(url).Result);
		}

		public DateTime? AbsoluteExpiration
		{
			get { return lazyResult.Value.Expires ?? DateTime.Now.AddDays(1); }
		}

		public string AppRelativePath
		{
			get { return url; }
		}

		public CacheDependency CacheDependency
		{
			get { return null; }
		}

		public byte[] ReadContent()
		{
			return lazyResult.Value.Content;
		}
	}
}
