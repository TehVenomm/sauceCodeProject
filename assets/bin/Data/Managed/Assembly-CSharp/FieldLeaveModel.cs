using Network;

public class FieldLeaveModel : BaseModel
{
	public class RequestSendForm
	{
		public int toHome;

		public int retire;

		public TaskUpdateInfo actioncount = new TaskUpdateInfo();
	}

	public static string URL = "ajax/field/leave";
}
