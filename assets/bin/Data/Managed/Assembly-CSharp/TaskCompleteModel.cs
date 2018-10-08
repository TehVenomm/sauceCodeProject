using Network;

public class TaskCompleteModel : BaseModel
{
	public class RequestSendForm
	{
		public int uId;
	}

	public static string URL = "ajax/task/complete";

	public TaskCompleteReward result = new TaskCompleteReward();
}
