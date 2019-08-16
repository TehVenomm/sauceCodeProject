public class TradingPostSellItemModel : BaseModel
{
	public class RequestSendForm
	{
		public int uid;

		public int quantity;

		public int price;
	}

	public static string URL = "/ajax/trading-post/sell";
}
