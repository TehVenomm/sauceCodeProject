public class FriendFollowerListModel : BaseModel
{
	public class Param : FriendFollowListModel.Param
	{
		public int chunkSize = 100;
	}

	public class RequestSendForm
	{
		public int page;

		public int sortType;
	}

	public static string URL = "ajax/friend/followerlist";

	public Param result = new Param();
}
