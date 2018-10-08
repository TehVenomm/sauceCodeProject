public class InviteInputModel : BaseModel
{
	public class RequestSendForm
	{
		public string code;
	}

	public static string URL = "ajax/invite/input";
}
