using Network;
using System.Collections.Generic;

public class OnceGuildRequestItemListModel : BaseModel
{
	public static string URL = "ajax/once/task";

	public List<GuildRequestItem> result = new List<GuildRequestItem>();
}
