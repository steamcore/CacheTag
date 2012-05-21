using System.Collections.Generic;
using System.Web.Caching;
using CacheTag.Core.Cache;
using CacheTag.Core.Configuration;
using CacheTag.Core.Extensions;
using CacheTag.Core.Filesystem;

namespace CacheTag.Core.Resources
{
	public abstract class BaseFileResource<T> : IResource
		where T : IResource
	{
		private readonly IFileProvider fileProvider;
		private readonly string path;
		private Dictionary<string, object> internalProperties;

		protected Dictionary<string, object> Properties
		{
			get
			{
				if (internalProperties != null)
					return internalProperties;

				var cacheKey = "cachetag_" + path;
				var cacheProvider = Container.Resolve<ICacheProvider>();

				internalProperties = cacheProvider.Get<Dictionary<string, object>>(cacheKey);

				if (internalProperties != null)
					return internalProperties;

				internalProperties = new Dictionary<string, object>();
				LoadFileProperties(internalProperties);

				cacheProvider.Put(cacheKey, internalProperties, CacheDependency, fileProvider.AbsoluteExpiration);
				return internalProperties;
			}
		}

		public CacheDependency CacheDependency { get { return fileProvider.CacheDependency; } }
		public byte[] BinaryContent { get { return (byte[])Properties["BinaryContent"]; } }
		public string Hash { get { return (string)Properties["Hash"]; } }
		public string MimeType { get; private set; }
		public string Name { get { return path; } }

		public string Url { get { return (string)Properties["Url"]; } }

		protected BaseFileResource(string path, string mimeType)
			: this(path, mimeType, new DefaultFileProvider(path))
		{
		}

		protected BaseFileResource(string path, string mimeType, IFileProvider fileProvider)
		{
			this.fileProvider = fileProvider;
			this.path = path;

			MimeType = mimeType;
		}

		protected virtual void LoadFileProperties(Dictionary<string, object> properties)
		{
			var binaryContent = fileProvider.ReadContent();
			properties["BinaryContent"] = binaryContent;
			properties["Hash"] = Settings.HashAlgorithm.ComputeStringHash(binaryContent);
			properties["Url"] = Settings.IsReleaseMode
				? Container.Resolve<IUrlResolver>().GetResourceUrl(this)
				: string.Format("{0}?{1}", fileProvider.AppRelativePath, Hash);
		}
	}
}
