using Network;

public class TradingPostItemStartingAtPriceModel : BaseModel
{
	public class RequestSendForm
	{
		public int itemId;
	}

	public static string URL = "ajax/trading-post/daily-item-summary";

	public TradingPostItemStartingAtPrice result = new TradingPostItemStartingAtPrice();
}
