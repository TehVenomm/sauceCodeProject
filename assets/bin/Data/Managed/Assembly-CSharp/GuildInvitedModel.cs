using System.Collections.Generic;

public class GuildInvitedModel : BaseModel
{
	public class Param
	{
		public List<GuildInvitedInfo> list;
	}

	public class RequestInvited
	{
		public static string path = "clan/AtvUserRequestList.go";
	}

	public class GuildInvitedInfo
	{
		public int requestId;

		public int level;

		public string name;

		public string admin;

		public int privacy;

		public int currentMem;

		public int memCap;

		public int[] emblem;

		public int clanId;

		public string sender;
	}

	public Param result = new Param();
}
