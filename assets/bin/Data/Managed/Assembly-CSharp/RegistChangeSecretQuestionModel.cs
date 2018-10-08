public class RegistChangeSecretQuestionModel : BaseModel
{
	public class RequestSendForm
	{
		public string password;

		public int secretQuestionType;

		public string secretQuestionAnswer;
	}

	public static string URL = "ajax/regist/changesecretquestion";
}
