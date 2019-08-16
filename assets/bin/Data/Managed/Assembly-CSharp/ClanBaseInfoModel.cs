using Network;

public class ClanBaseInfoModel : BaseModel
{
	public class Param
	{
		public PartyModel.Party clanParty;

		public ClanServer clanServer;
	}

	public static string URL = "ajax/clan/base-info";

	public Param result = new Param();
}
