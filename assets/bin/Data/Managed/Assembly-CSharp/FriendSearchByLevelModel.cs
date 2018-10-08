using Network;

public class FriendSearchByLevelModel : BaseModel
{
	public class RequestSendForm
	{
		public int page;
	}

	public static string URL = "ajax/friend/searchbylevel";

	public FriendSearchResult result = new FriendSearchResult();
}
