using Network;

public class DebugDelAccountModel : BaseModel
{
	public class RequestSendForm
	{
		public int uid;
	}

	public static string URL = "ajax/debug/delaccount";

	public UserInfo result = new UserInfo();
}
