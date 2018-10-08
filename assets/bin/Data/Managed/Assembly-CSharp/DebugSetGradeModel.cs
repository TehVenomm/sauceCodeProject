public class DebugSetGradeModel : BaseModel
{
	public class RequestSendForm
	{
		public int fg;

		public int qg;
	}

	public static string URL = "ajax/debug/setgrade";
}
