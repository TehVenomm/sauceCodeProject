using System.Collections.Generic;

public class GuildInviteModel : BaseModel
{
	public class RequestSendForm
	{
		public string id;

		public List<int> inviteList = new List<int>();
	}

	public static string URL = "clan/AtvInvite.go";
}
