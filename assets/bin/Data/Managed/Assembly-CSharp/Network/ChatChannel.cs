using System;

namespace Network
{
	[Serializable]
	public class ChatChannel
	{
		public int channel;

		public string host;

		public string path;

		public int port;

		public int connections;

		public int messages;

		public string CreateUrl()
		{
			return new UriBuilder($"ws://{host}/{path}")
			{
				Port = port
			}.Uri.ToString();
		}

		public override bool Equals(object obj)
		{
			ChatChannel chatChannel = obj as ChatChannel;
			if (chatChannel != null)
			{
				if (channel == chatChannel.channel && host == chatChannel.host && path == chatChannel.path)
				{
					return port == chatChannel.port;
				}
				return false;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
