using System.Collections.Generic;

public class LoungeSearchFollowerRoomModel : BaseModel
{
	public class Param
	{
		public List<LoungeFollowerModel> lounges = new List<LoungeFollowerModel>();

		public List<int> firstMetUserIds = new List<int>();
	}

	public class LoungeFollowerModel : LoungeModel.Lounge
	{
		public int followerUserId;
	}

	public static string URL = "ajax/lounge/followerlounge";

	public Param result = new Param();
}
