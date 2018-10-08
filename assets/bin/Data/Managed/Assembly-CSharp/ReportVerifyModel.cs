public class ReportVerifyModel : BaseModel
{
	public class RequestSendForm
	{
		public string fileName;

		public string fileHash;
	}

	public static string URL = "ajax/report/verify";
}
