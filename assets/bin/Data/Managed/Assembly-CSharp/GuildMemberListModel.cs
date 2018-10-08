using Network;
using System;
using System.Collections.Generic;

public class GuildMemberListModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public List<FriendCharaInfo> list = new List<FriendCharaInfo>();

		public List<FriendCharaInfo> requesters = new List<FriendCharaInfo>();

		public int clanMasterId;
	}

	public class RequestSendForm
	{
		public int clanId;
	}

	public static string URL = "clan/ClanMemberList.go";

	public Param result = new Param();
}
