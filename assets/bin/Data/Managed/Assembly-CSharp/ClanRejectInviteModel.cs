public class ClanRejectInviteModel : BaseModel
{
	public class RequestSendForm
	{
		public int cId;
	}

	public static string URL = "ajax/clan/reject-invite";
}
