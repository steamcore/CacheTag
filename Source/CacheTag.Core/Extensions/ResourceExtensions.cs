using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheTag.Core.Configuration;
using CacheTag.Core.Resources;
using CacheTag.Core.Resources.Html;

namespace CacheTag.Core.Extensions
{
	public static class ResourceExtensions
	{
		public static IEnumerable<T> Compile<T>(this IEnumerable<T> items)
			where T : IResource
		{
			return Settings.IsReleaseMode ? Container.Resolve<IResourceCompiler<T>>().Compile(items).Store() : items;
		}

		public static T Store<T>(this T item)
			where T : IResource
		{
			Container.Resolve<IResourceStorage>().Store(item);
			return item;
		}

		public static IEnumerable<T> Store<T>(this IEnumerable<T> items)
			where T : IResource
		{
			return items.Select(x => x.Store()).ToList();
		}

		public static string RenderHtml<T>(this IEnumerable<T> items)
			where T : IResource
		{
			return RenderItems(items, (renderer, item) => renderer.RenderHtml(item));
		}

		public static string RenderHtmlInline<T>(this IEnumerable<T> items)
			where T : IPlainTextResource
		{
			return RenderItems(items, (renderer, item) => renderer.RenderInlineHtml(item));
		}

		private static string RenderItems<T>(IEnumerable<T> items, Func<IHtmlRenderer<T>,T,string> render)
			where T : IResource
		{
			var renderer = Container.Resolve<IHtmlRenderer<T>>();
			var sb = new StringBuilder();
			foreach (var item in items)
			{
				sb.AppendLine(render(renderer, item));
			}
			return sb.ToString().Trim();
		}
	}
}
