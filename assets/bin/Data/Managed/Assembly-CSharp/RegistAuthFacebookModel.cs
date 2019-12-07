public class RegistAuthFacebookModel : BaseModel
{
	public class Param : RegistCreateModel.Param
	{
		public int newsId = 1;
	}

	public class RequestSendForm
	{
		public string accessToken;

		public string uid;

		public string d;
	}

	public static string URL = "ajax/regist/authorcreatefacebook";

	public Param result = new Param();
}
