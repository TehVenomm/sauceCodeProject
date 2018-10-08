using Network;
using System.Collections.Generic;

public class LoungeInviteListModel : BaseModel
{
	public class RequestSendForm
	{
		public string id;
	}

	public static string URL = "ajax/lounge/invitelist";

	public List<LoungeInviteCharaInfo> result = new List<LoungeInviteCharaInfo>();
}
