using Network;

public class RegistLinkFacebookModel : BaseModel
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

	public class RequestOverrideSendForm
	{
		public string accessToken;

		public int overwriteOldData;
	}

	public class RequestSendForm
	{
		public string accessToken;
	}

	public static string URL = "ajax/regist/linkfacebook";

	public UserInfo result = new UserInfo();

	public ExistInfoParam existInfo = new ExistInfoParam();
}
