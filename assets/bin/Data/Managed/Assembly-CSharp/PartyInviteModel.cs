using System.Collections.Generic;

public class PartyInviteModel : BaseModel
{
	public class RequestSendForm
	{
		public string id;

		public List<int> ids = new List<int>();
	}

	public static string URL = "ajax/party/invite";

	public List<int> result;
}
