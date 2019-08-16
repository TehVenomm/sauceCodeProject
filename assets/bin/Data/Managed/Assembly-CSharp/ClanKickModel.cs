public class ClanKickModel : BaseModel
{
	public class Param
	{
		public PartyModel.Party clanParty;
	}

	public class RequestSendForm
	{
		public int uId;
	}

	public static string URL = "ajax/clan/kick";

	public Param result = new Param();
}
