public class DebugSetBoostModel : BaseModel
{
	public class RequestSendForm
	{
		public int type;

		public int value;

		public string end;
	}

	public static string URL = "ajax/debug/setboost";
}
