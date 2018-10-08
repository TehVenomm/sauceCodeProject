public class RegistAuthGoogleModel : BaseModel
{
	public class RequestSendForm
	{
		public string account;

		public string accountKey;

		public string password;

		public string d;

		public int purchasetype;
	}

	public static string URL = "ajax/regist/authgoogle";

	public RegistAuthRobModel.Param result = new RegistAuthRobModel.Param();
}
