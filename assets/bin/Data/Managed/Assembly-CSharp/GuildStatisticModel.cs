using Network;

public class GuildStatisticModel : BaseModel
{
	public class Form
	{
		public int clanId;
	}

	public static string URL = "clan/ClanStatistics.go";

	public GuildStatisticInfo result;
}
