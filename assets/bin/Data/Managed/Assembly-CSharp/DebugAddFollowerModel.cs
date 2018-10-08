using System.Collections.Generic;

public class DebugAddFollowerModel : BaseModel
{
	public class RequestSendForm
	{
		public List<int> ids = new List<int>();
	}

	public static string URL = "ajax/debug/addfollower";
}
