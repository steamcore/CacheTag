using System.Collections.Generic;
using System.Linq;

namespace CacheTag.Core.Resources.Images
{
	public class ImageList : List<IImageResource>
	{
		public ImageList()
		{
		}

		public ImageList(string image)
		{
			Add(new ImageFile(image));
		}

		public ImageList(IEnumerable<string> images)
		{
			AddRange(images.Select(x => new ImageFile(x)));
		}

		public ImageList(IEnumerable<IImageResource> images)
		{
			AddRange(images);
		}
	}
}
