using Network;

public class ClanApplyModel : BaseModel
{
	public class Param
	{
		public PartyModel.Party clanParty;

		public ClanServer clanServer;
	}

	public class RequestSendForm
	{
		public string cId;
	}

	public static string URL = "ajax/clan/apply";

	public Param result = new Param();
}
