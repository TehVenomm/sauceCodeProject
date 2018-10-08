using Network;
using System;

public class GuildChatChannelEnterModel : BaseModel
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

	public static string URL = "clan/ChatChannelEnter.go";

	public Param result = new Param();
}
