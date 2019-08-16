public class ClanRejectRequestModel : BaseModel
{
	public class RequestSendForm
	{
		public int uId;
	}

	public static string URL = "ajax/clan/reject-request";
}
