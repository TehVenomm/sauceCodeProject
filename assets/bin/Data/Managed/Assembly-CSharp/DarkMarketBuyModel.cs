public class DarkMarketBuyModel : BaseModel
{
	public class RequestSendForm
	{
		public int marketId;

		public int crystalCL;
	}

	public static string URL = "ajax/black-market/buy";
}
