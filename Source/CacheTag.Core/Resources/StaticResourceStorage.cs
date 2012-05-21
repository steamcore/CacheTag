using System.Collections.Concurrent;

namespace CacheTag.Core.Resources
{
	public class StaticResourceStorage : IResourceStorage
	{
		private static readonly ConcurrentDictionary<string, IResource> Storage = new ConcurrentDictionary<string, IResource>();

		public string Store(IResource resource)
		{
			if (Storage.ContainsKey(resource.Hash))
				return resource.Hash;

			Storage.AddOrUpdate(resource.Hash, resource, (key, res) => resource);
			return resource.Hash;
		}

		public IResource Retrieve(string id)
		{
			return Storage[id];
		}

		public T Retrieve<T>(string id)
			where T : IResource
		{
			return (T)Storage[id];
		}
	}
}
