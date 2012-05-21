using System.Security.Cryptography;
using System.Text;

namespace CacheTag.Core.Extensions
{
	public static class HashAlgorithmExtensions
	{
		public static string ComputeStringHash(this HashAlgorithm algorithm, string content)
		{
			if (string.IsNullOrWhiteSpace(content))
				return string.Empty;

			return ComputeStringHash(algorithm, Encoding.UTF8.GetBytes(content));
		}

		public static string ComputeStringHash(this HashAlgorithm algorithm, byte[] content)
		{
			var computedHash = algorithm.ComputeHash(content);
			var sb = new StringBuilder();
			for (var i = 0; i < computedHash.Length; i++)
			{
				sb.Append(computedHash[i].ToString("x2"));
			}
			return sb.ToString();
		}
	}
}
