using System;

public class FriendDeleteFollowerModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public int success;
	}

	public class RequestSendForm
	{
		public int followerUserId;
	}

	public static string URL = "ajax/friend/deletefollower";

	public Param result = new Param();
}
