public class ClanCancelInviteModel : BaseModel
{
	public class RequestSendForm
	{
		public int uId;
	}

	public static string URL = "ajax/clan/cancel-invite";
}
