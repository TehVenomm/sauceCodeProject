using Network;
using System.Collections.Generic;

public class ClanRequestListModel : BaseModel
{
	public static string URL = "ajax/clan/request-list";

	public List<FriendCharaInfo> result;
}
