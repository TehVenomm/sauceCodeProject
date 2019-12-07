using Network;

public class CheckRegisterModel : BaseModel
{
	public class Param
	{
		public string uh = "";

		public int userId;

		public string userIdHash;

		public UserInfo userInfo = new UserInfo();

		public int newsId = 1;

		public int tutorialStep;

		public bool sendAsset;

		public bool termsCheck;

		public string termsUpdateDay = "";

		public bool recommendUpdate;
	}

	public class RequestSendForm
	{
		public string data;
	}

	public static string URL = "ajax/regist/checkregister";

	public Param result = new Param();
}
