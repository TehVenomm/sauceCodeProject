using System.Collections.Generic;

public class ClanChatMessageUpdateModel : BaseModel
{
	public class Param
	{
		public List<ClanChatMessageModel> messages = new List<ClanChatMessageModel>();

		public int displayNum;

		public int updateInterval = 10;
	}

	public class RequestSendForm
	{
		public string cLatestId;
	}

	public static string URL = "ajax/clan-message/messageupdate";

	public Param result = new Param();
}
