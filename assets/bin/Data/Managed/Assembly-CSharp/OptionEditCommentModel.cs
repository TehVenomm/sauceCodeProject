public class OptionEditCommentModel : BaseModel
{
	public class RequestSendForm
	{
		public string comment;
	}

	public static string URL = "ajax/option/editcomment";
}
