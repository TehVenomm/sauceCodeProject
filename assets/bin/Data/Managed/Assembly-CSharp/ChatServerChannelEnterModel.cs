using Network;
using System;

public class ChatServerChannelEnterModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public ChatChannel channel;
	}

	public class RequestSendForm
	{
		public int channel;
	}

	public static string URL = "ajax/chat-server/channel-enter";

	public Param result = new Param();
}
