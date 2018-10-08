using Network;
using System;
using System.Collections.Generic;

public class GuildPrivateChatModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public List<ClanChatLogMessageData> array = new List<ClanChatLogMessageData>();
	}

	public class SendForm
	{
		public int toUserId;
	}

	public static string URL = "clan/ChatPrivateLog.go";

	public Param result = new Param();
}
