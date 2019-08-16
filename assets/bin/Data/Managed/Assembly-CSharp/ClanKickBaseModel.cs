public class ClanKickBaseModel : BaseModel
{
	public class Param
	{
		public PartyModel.Party clanParty;
	}

	public class RequestSendForm
	{
		public int uId;
	}

	public static string URL = "ajax/clan/kick-base";

	public Param result = new Param();
}
