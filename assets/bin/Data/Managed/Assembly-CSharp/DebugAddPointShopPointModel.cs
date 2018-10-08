public class DebugAddPointShopPointModel : BaseModel
{
	public class RequestSendForm
	{
		public int pointShopId;

		public int point;
	}

	public static string URL = "ajax/debug/shoppoint";
}
