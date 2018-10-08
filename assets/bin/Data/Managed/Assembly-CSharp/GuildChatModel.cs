using Network;
using System;
using System.Collections.Generic;

public class GuildChatModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public List<ClanChatLogMessageData> array = new List<ClanChatLogMessageData>();

		public PinData pin;

		public ClanAdvisaryData advisory;
	}

	public class PinData
	{
		public int fromUserId;

		public int id;

		public int type;

		public string message;

		public string uuid;

		public CharaInfo charInfo;
	}

	public static string URL = "clan/ChatLog.go";

	public Param result = new Param();
}
