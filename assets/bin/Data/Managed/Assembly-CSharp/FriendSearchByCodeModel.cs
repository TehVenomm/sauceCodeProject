using Network;

public class FriendSearchByCodeModel : BaseModel
{
	public class RequestSendForm
	{
		public string code;
	}

	public static string URL = "ajax/friend/searchbycode";

	public FriendSearchResult result = new FriendSearchResult();
}
