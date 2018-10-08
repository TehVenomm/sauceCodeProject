using Network;
using System.Collections.Generic;

public class GuildSearchFollowerRoomModel : BaseModel
{
	public class Param
	{
		public List<GuildFollowerModel> list = new List<GuildFollowerModel>();
	}

	public class GuildFollowerModel
	{
		public FriendCharaInfo charInfo;

		public GuildModel.Guild clanData;
	}

	public static string URL = "clan/ClanFollowingList.go";

	public Param result = new Param();
}
