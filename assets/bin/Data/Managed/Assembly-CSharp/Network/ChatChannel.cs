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
			string uri = $"ws://{host}/{path}";
			UriBuilder uriBuilder = new UriBuilder(uri);
			uriBuilder.Port = port;
			return uriBuilder.Uri.ToString();
		}

		public override bool Equals(object obj)
		{
			ChatChannel chatChannel = obj as ChatChannel;
			if (chatChannel != null)
			{
				return channel == chatChannel.channel && host == chatChannel.host && path == chatChannel.path && port == chatChannel.port;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
