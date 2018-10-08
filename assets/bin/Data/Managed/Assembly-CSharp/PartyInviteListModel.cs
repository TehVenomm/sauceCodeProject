using Network;
using System.Collections.Generic;

public class PartyInviteListModel : BaseModel
{
	public class RequestSendForm
	{
		public string id;
	}

	public static string URL = "ajax/party/invitelist";

	public List<PartyInviteCharaInfo> result = new List<PartyInviteCharaInfo>();
}
