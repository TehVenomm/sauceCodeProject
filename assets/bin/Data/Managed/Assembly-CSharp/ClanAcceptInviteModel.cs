using Network;

public class ClanAcceptInviteModel : BaseModel
{
	public class Param
	{
		public PartyModel.Party clanParty;

		public ClanServer clanServer;
	}

	public class RequestSendForm
	{
		public int cId;
	}

	public static string URL = "ajax/clan/accept-invite";

	public Param result = new Param();
}
