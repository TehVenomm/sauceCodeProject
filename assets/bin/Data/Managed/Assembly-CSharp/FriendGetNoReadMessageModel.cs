public class FriendGetNoReadMessageModel : BaseModel
{
	public class RequestSendForm
	{
		public int userId;
	}

	public static string URL = "ajax/friend/getnoreadmessage";
}
