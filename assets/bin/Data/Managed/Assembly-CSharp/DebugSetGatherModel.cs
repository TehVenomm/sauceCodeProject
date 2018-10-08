public class DebugSetGatherModel : BaseModel
{
	public class RequestSendForm
	{
		public int pid;

		public int gid;

		public int interval;
	}

	public static string URL = "ajax/debug/setgather";
}
