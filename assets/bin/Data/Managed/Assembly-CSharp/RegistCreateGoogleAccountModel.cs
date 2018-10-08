using Network;

public class RegistCreateGoogleAccountModel : BaseModel
{
	public class RequestSendForm
	{
		public string account;

		public string accountKey;

		public string password;

		public string confirmPassword;
	}

	public static string URL = "ajax/regist/creategoogleaccount";

	public UserInfo result = new UserInfo();
}
