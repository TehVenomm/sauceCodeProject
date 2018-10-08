public class DebugUsedPortalModel : BaseModel
{
	public class RequestSendForm
	{
		public int pId;

		public int used;
	}

	public static string URL = "ajax/debug/usedportal";
}
