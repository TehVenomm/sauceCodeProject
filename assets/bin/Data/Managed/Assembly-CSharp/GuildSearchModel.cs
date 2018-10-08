using System.Collections.Generic;

public class GuildSearchModel : BaseModel
{
	public class Param
	{
		public List<GuildSearchInfo> clanList;
	}

	public class GuildSearchInfo
	{
		public int level;

		public string name;

		public string admin;

		public int privacy;

		public int currentMem;

		public int memCap;

		public int[] emblem;

		public int clanId;

		public string tag;
	}

	public class RequestSearch
	{
		public static string path = "clan/ClanList.go";

		public string token;
	}

	public class RequestSearchWithKeyword
	{
		public static string path = "clan/ClanSearch.go";

		public string token;

		public string keyword;
	}

	public Param result = new Param();
}
