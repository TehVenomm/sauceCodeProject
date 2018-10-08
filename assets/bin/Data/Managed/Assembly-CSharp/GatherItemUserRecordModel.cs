using Network;
using System.Collections.Generic;

public class GatherItemUserRecordModel : BaseModel
{
	public class Param
	{
		public int totalNum;

		public List<GatherItemRecord> gatherItems = new List<GatherItemRecord>();
	}

	public class RequestSendForm
	{
		public int userId;

		public int eventId;
	}

	public static string URL = "ajax/gather-item/user-record";

	public Param result = new Param();
}
