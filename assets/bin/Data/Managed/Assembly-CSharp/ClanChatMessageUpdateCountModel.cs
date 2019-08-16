public class ClanChatMessageUpdateCountModel : BaseModel
{
	public class Param
	{
		public int updateCount;
	}

	public class RequestSendForm
	{
		public string cLatestId;
	}

	public static string URL = "ajax/clan-message/messageupdatecount";

	public Param result = new Param();
}
