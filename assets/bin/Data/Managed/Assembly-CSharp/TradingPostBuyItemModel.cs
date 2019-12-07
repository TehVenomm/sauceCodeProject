public class TradingPostBuyItemModel : BaseModel
{
	public class RequestSendForm
	{
		public int transactionId;

		public int crystalCL;
	}

	public static string URL = "ajax/trading-post/buy-item";
}
