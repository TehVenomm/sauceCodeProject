using Network;

public class ClanLeaveBaseModel : BaseModel
{
	public class Param
	{
		public PartyModel.Party clanParty;

		public ClanServer clanServer;
	}

	public static string URL = "ajax/clan/leave-base";

	public Param result = new Param();
}
