public class GuildSearchModelWithID : BaseModel
{
	public class Param
	{
		public GuildSearchModel.GuildSearchInfo guildInfo;
	}

	public class RequestSearchWithID
	{
		public static string path = "clan/ClanInfo.go";

		public string token;

		public int clanId;
	}

	public Param result = new Param();
}
