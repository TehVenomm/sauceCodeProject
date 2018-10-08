public class AppReviewInfoModel : BaseModel
{
	public class RequestSendForm
	{
		public int starValue;

		public int replyAction;
	}

	public static string URL = "ajax/review/info";
}
