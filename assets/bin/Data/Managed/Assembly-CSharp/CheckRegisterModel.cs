using Network;

public class CheckRegisterModel : BaseModel
{
	public class Param
	{
		public string uh = string.Empty;

		public int userId;

		public string userIdHash;

		public UserInfo userInfo = new UserInfo();

		public int newsId = 1;

		public int tutorialStep;

		public bool sendAsset;

		public bool recommendUpdate;
	}

	public class RequestSendForm
	{
		public string data;
	}

	public static string URL = "ajax/regist/checkregister";

	public Param result = new Param();
}
