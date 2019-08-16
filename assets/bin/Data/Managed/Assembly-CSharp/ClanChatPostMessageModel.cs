using System.Collections.Generic;

public class ClanChatPostMessageModel : BaseModel
{
	public class Param
	{
		public List<ClanChatMessageModel> messages = new List<ClanChatMessageModel>();
	}

	public class RequestSendForm
	{
		public string message;

		public string cLatestId;
	}

	public static string URL = "ajax/clan-message/postmessage";

	public Param result = new Param();
}
