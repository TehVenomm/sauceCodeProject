public class ClanAcceptRequestModel : BaseModel
{
	public class RequestSendForm
	{
		public int uId;
	}

	public static string URL = "ajax/clan/accept-request";
}
