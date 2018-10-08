public class OpinionPostModel : BaseModel
{
	public class RequestSendForm
	{
		public string msg;
	}

	public static string URL = "ajax/opinion/post";
}
