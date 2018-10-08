using System.Collections.Generic;

public class LoungeInviteModel : BaseModel
{
	public class RequestSendForm
	{
		public string id;

		public List<int> ids = new List<int>();
	}

	public static string URL = "ajax/lounge/invite";

	public List<int> result;
}
