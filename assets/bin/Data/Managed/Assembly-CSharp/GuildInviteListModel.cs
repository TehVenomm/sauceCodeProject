using Network;
using System.Collections.Generic;

public class GuildInviteListModel : BaseModel
{
	public class GuildInviteList
	{
		public List<GuildInviteCharaInfo> list;
	}

	public class RequestSendForm
	{
		public string id;

		public string page;
	}

	public static string URL = "clan/ClanInviteList.go";

	public GuildInviteList result;
}
