public class DebugChangeLastLoginModel : BaseModel
{
	public class RequestSendForm
	{
		public int day;

		public int hour;
	}

	public static string URL = "ajax/debug/changelastlogin";
}
