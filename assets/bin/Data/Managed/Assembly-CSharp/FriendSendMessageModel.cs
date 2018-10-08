using System;

public class FriendSendMessageModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public int success;
	}

	public class RequestSendForm
	{
		public int toUserId;

		public string message;
	}

	public static string URL = "ajax/friend/sendmessage";

	public Param result = new Param();
}
