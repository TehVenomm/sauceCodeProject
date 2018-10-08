public class AnalyticTrackingPointModel : BaseModel
{
	public class RequestSendForm
	{
		public string name;

		public string category;
	}

	public static string URL = "ajax/status/tracking";
}
