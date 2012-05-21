namespace CacheTag.Core.Resources
{
	public interface IResourceStorage
	{
		string Store(IResource resource);
		IResource Retrieve(string id);
		T Retrieve<T>(string id) where T : IResource;
	}
}