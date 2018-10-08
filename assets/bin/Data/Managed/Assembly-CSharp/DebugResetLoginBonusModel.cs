public class DebugResetLoginBonusModel : BaseModel
{
	public class RequestSendForm
	{
		public int id;

		public int count;
	}

	public static string URL = "ajax/debug/resetloginbonus";
}
