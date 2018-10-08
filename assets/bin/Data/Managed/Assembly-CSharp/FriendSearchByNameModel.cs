using Network;

public class FriendSearchByNameModel : BaseModel
{
	public class RequestSendForm
	{
		public string name;

		public int page;
	}

	public static string URL = "ajax/friend/searchbyname";

	public FriendSearchResult result = new FriendSearchResult();
}
