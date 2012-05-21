using System.IO;

namespace CacheTag.Core.Extensions
{
	public static class StreamExtensions
	{
		public static byte[] ReadAllBytes(this Stream stream)
		{
			const int chunkSize = 65536;
			using (var memoryStream = new MemoryStream(chunkSize))
			{
				var byteCount = 0;

				while (true)
				{
					var buffer = new byte[chunkSize];
					var bytesRead = stream.Read(buffer, 0, chunkSize);

					if (bytesRead == 0)
						break;

					memoryStream.Write(buffer, 0, bytesRead);
					byteCount += bytesRead;
				}

				var fileBuffer = new byte[byteCount];
				memoryStream.Seek(0, SeekOrigin.Begin);
				memoryStream.Read(fileBuffer, 0, byteCount);
				return fileBuffer;
			}
		}
	}
}
