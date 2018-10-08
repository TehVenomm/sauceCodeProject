using System.Collections.Generic;

public class DebugSetDeliveryCntModel : BaseModel
{
	public class RequestSendForm
	{
		public string uId;

		public List<int> cnts = new List<int>();
	}

	public static string URL = "ajax/debug/setdeliverycnt";
}
