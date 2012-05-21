using System;

namespace CacheTag.Core.Network
{
	public class RemoteFile
	{
		public byte[] Content { get; set; }
		public DateTime? Expires { get; set; }
	}
}
