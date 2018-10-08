using System;
using System.Collections.Generic;

public class FriendFollowModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public List<int> success = new List<int>();

		public List<int> err = new List<int>();
	}

	public class RequestSendForm
	{
		public List<int> ids = new List<int>();
	}

	public static string URL = "ajax/friend/follow";

	public Param result = new Param();
}
