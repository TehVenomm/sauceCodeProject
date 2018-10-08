public class DebugSetInvMaxModel : BaseModel
{
	public class RequestSendForm
	{
		public int emax;

		public int smax;
	}

	public static string URL = "ajax/debug/setinvmax";
}
