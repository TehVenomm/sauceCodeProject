using System.Collections.Generic;

public class ClanChatMessageHistoryModel : BaseModel
{
	public class Param
	{
		public List<ClanChatMessageModel> messages = new List<ClanChatMessageModel>();

		public bool isRemaining;

		public int displayNum;

		public int updateInterval = 10;
	}

	public class RequestSendForm
	{
		public string fromId;
	}

	public static string URL = "ajax/clan-message/messagehistory";

	public Param result = new Param();
}
