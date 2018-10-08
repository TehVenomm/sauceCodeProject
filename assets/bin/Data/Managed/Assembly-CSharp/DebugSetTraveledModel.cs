public class DebugSetTraveledModel : BaseModel
{
	public class RequestSendForm
	{
		public int mapId;

		public int cnt;
	}

	public static string URL = "ajax/debug/settraveled";
}
