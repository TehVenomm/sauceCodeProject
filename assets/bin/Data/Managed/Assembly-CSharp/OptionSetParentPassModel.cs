public class OptionSetParentPassModel : BaseModel
{
	public class RequestSendForm
	{
		public string password;

		public string confirmPassword;
	}

	public static string URL = "ajax/option/setparentpass";
}
