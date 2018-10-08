using Network;
using System;

public class ChatServerChannelListModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public ChatChannelInfo chat;
	}

	public static string URL = "ajax/chat-server/channel-list";

	public Param result = new Param();
}
