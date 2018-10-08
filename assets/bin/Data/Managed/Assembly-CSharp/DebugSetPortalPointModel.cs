public class DebugSetPortalPointModel : BaseModel
{
	public class RequestSendForm
	{
		public int pId;

		public int point;
	}

	public static string URL = "ajax/debug/setportalpoint";
}
