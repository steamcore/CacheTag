using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CacheTag.Core.Extensions;

namespace CacheTag.Core.Network
{
	public static class HttpUtility
	{
		private static readonly Regex CacheControlRegex = new Regex(@"max-age=(\d+)");

		public static Task<RemoteFile> AsyncDownload(string url)
		{
			var task = new Task<RemoteFile>(() => Download(url));
			task.Start();
			return task;
		}

		private static RemoteFile Download(string url)
		{
			var request = (HttpWebRequest)WebRequest.Create(url);

			using (var response = (HttpWebResponse)request.GetResponse())
			using (var stream = request.GetResponse().GetResponseStream())
			{
				if (response.StatusCode != HttpStatusCode.OK || stream == null)
					throw new WebException("Error downloading " + url);

				return new RemoteFile
				{
					Content = stream.ReadAllBytes(),
					Expires = GetExpires(response.Headers)
				};
			}
		}

		private static DateTime? GetExpires(WebHeaderCollection headers)
		{
			DateTime expires;

			if (headers["Expires"] != null && DateTime.TryParse(headers["Expires"], out expires))
			{
				return expires;
			}

			if (headers["Cache-Control"] != null && CacheControlRegex.IsMatch(headers["Cache-Control"]))
			{
				var maxAgeStr = CacheControlRegex.Match(headers["Cache-Control"]).Groups[1].Value;
				int maxAge;
				if (int.TryParse(maxAgeStr, out maxAge))
				{
					return DateTime.Now.AddSeconds(maxAge);
				}
			}

			return null;
		}
	}
}
