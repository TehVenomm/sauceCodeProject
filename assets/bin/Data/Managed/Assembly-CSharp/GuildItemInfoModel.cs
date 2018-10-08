using System.Collections.Generic;

public class GuildItemInfoModel : BaseModel
{
	public class Param
	{
		public List<EmblemInfo> emblem;
	}

	public class EmblemInfo
	{
		public int id;

		public string image;

		public int price;

		public int currency;

		public int type;

		public string unlock;
	}

	public class RequestAllItemInfo
	{
		public static string path = "clan/ClanAllInfo.go";

		public string token;
	}

	public Param result = new Param();
}
