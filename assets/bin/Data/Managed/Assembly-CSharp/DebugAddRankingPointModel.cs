public class DebugAddRankingPointModel : BaseModel
{
	public class RequestSendForm
	{
		public int point;

		public int eventId;
	}

	public static string URL = "ajax/debug/addrankingpoint";
}
