public class FriendFollowerListModel : BaseModel
{
	public class RequestSendForm
	{
		public int page;
	}

	public static string URL = "ajax/friend/followerlist";

	public FriendFollowListModel.Param result = new FriendFollowListModel.Param();
}
