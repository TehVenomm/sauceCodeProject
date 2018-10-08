public class LoungeLeaveModel : BaseModel
{
	public class Param
	{
		public LoungeModel.Lounge lounge;
	}

	public class RequestSendForm
	{
		public string id;
	}

	public static string URL = "ajax/lounge/leave";

	public Param result = new Param();
}
