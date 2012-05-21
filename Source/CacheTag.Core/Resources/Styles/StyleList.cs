using System.Collections.Generic;
using System.Linq;
using CacheTag.Core.Configuration;

namespace CacheTag.Core.Resources.Styles
{
	public class StyleList : List<IStyleResource>
	{
		public StyleList()
		{
		}

		public StyleList(string styles)
		{
			Add(Container.Resolve<IResourceFactory<IStyleResource>>().Create(styles));
		}

		public StyleList(IEnumerable<string> styles)
		{
			var factory = Container.Resolve<IResourceFactory<IStyleResource>>();
			AddRange(styles.Select(factory.Create));
		}
	}
}
