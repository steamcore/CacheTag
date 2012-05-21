namespace CacheTag.Core
{
	public interface ICacheTagModule
	{
		void PreApplicationStart();
		void PostApplicationStart();
	}
}
