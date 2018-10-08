public class DebugChangeGatherTimeModel : BaseModel
{
	public class RequestSendForm
	{
		public int pid;

		public string appear;

		public string disappear;

		public string gatherStart;

		public string gatherEnd;
	}

	public static string URL = "ajax/debug/changegathertime";
}
