using Network;

public class ClanEnterBaseModel : BaseModel
{
	public class Param
	{
		public PartyModel.Party clanParty;

		public ClanServer clanServer;

		public ClanNoticeBoardData clanNoticeBoard;
	}

	public static string URL = "ajax/clan/enter-base";

	public Param result = new Param();
}
