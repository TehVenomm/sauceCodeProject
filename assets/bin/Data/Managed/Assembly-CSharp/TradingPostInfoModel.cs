using Network;
using System.Collections.Generic;

public class TradingPostInfoModel : BaseModel
{
	public class RequestSendForm
	{
		public int page;

		public int itemId;
	}

	public static string URL = "/ajax/trading-post/shop-list";

	public List<TradingPostInfo> result = new List<TradingPostInfo>();
}
