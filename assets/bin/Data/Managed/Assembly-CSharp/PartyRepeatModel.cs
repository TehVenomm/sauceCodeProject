public class PartyRepeatModel : BaseModel
{
	public class RequestSendForm
	{
		public string id;

		public int st;

		public int cs;

		public int ce;
	}

	public static string URL = "ajax/party/repeat";
}
