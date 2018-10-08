using Network;

public class FriendMutualFollowModel : BaseModel
{
	public class RequestSendForm
	{
		public string targetCode;
	}

	public static string URL = "ajax/friend/mutualfollow";

	public FriendMutualFollowResult result = new FriendMutualFollowResult();
}
