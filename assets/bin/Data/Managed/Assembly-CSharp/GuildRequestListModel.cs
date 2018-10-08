using Network;

public class GuildRequestListModel : BaseModel
{
	public static string URL = "ajax/guild-request/list";

	public GuildRequest result = new GuildRequest();
}
