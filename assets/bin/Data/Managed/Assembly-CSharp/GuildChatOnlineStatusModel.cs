using Network;
using System;
using System.Collections.Generic;

public class GuildChatOnlineStatusModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public List<GuildMemberChatStatus> online = new List<GuildMemberChatStatus>();
	}

	public static string URL = "clan/ClanOnline.go";

	public Param result = new Param();
}
