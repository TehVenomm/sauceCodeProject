using Network;

public class LinkRobModel : BaseModel
{
	public class ExistInfoParam
	{
		public int id;

		public string name;

		public int level;

		public string code;

		public int crystal;

		public int money;

		public string lastLogin;
	}

	public class RequestSendForm
	{
		public string email;

		public string password;
	}

	public static string URL = "ajax/regist/linkrob";

	public UserInfo result = new UserInfo();

	public ExistInfoParam existInfo = new ExistInfoParam();
}
