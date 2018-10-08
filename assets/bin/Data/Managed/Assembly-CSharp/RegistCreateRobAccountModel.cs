using Network;

public class RegistCreateRobAccountModel : BaseModel
{
	public class RequestSendForm
	{
		public string email;

		public string password;

		public string confirmPassword;

		public int secretQuestionType;

		public string secretQuestionAnswer;
	}

	public static string URL = "ajax/regist/createrobaccount";

	public UserInfo result = new UserInfo();
}
