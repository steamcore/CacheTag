namespace CacheTag.Core.Resources.Images
{
	public interface IImageResource : IResource
	{
		string AlternateText { get; }
		int Height { get; }
		int Width { get; }
	}
}
