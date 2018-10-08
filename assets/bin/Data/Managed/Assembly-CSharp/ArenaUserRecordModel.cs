using System;
using System.Collections.Generic;

public class ArenaUserRecordModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public int userRank;

		public List<int> clearMilliSecList;

		public int totalMilliSec;
	}

	public class RequestSendForm
	{
		public int userId;

		public int eventId;
	}

	public static string URL = "ajax/arena/user-record";

	public Param result = new Param();
}
