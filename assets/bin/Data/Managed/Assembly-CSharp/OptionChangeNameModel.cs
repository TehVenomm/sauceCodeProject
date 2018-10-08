public class OptionChangeNameModel : BaseModel
{
	public class RequestSendForm
	{
		public string name;
	}

	public static string URL = "ajax/option/changename";
}
