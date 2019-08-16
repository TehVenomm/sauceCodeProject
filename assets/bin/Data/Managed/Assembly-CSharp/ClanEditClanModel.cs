using Network;

public class ClanEditClanModel : BaseModel
{
	public class RequestSendForm
	{
		public string name;

		public int iId;

		public int jt;

		public int lbl;

		public string cmt;

		public string tag;
	}

	public static string URL = "ajax/clan/edit-clan";

	public ClanData result = new ClanData();
}
