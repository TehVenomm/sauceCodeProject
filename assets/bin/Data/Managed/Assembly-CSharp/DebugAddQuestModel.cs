public class DebugAddQuestModel : BaseModel
{
	public class RequestSendForm
	{
		public int questId;

		public int num;
	}

	public static string URL = "ajax/debug/addquest";
}
