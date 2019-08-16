using Network;

public class ClanCreateModel : BaseModel
{
	public class Param
	{
		public PartyModel.Party clanParty;

		public ClanServer clanServer;
	}

	public class RequestSendForm
	{
		public string name;

		public int jt;

		public int lbl;

		public string cmt;

		public string tag;

		public int crystalCL;
	}

	public static string URL = "ajax/clan/create";

	public Param result = new Param();
}
