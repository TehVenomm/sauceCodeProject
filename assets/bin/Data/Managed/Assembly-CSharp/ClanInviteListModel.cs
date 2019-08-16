using Network;
using System.Collections.Generic;

public class ClanInviteListModel : BaseModel
{
	public static string URL = "ajax/clan/invite-list";

	public List<ClanData> result = new List<ClanData>();
}
