using System;

public class FriendUnfollowModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public int success;
	}

	public class RequestSendForm
	{
		public int followUserId;
	}

	public static string URL = "ajax/friend/unfollow";

	public Param result = new Param();
}
