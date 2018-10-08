using Network;
using System.Collections.Generic;

public class TradingPostItemDetailModel : BaseModel
{
	public class RequestSendForm
	{
		public int itemId;

		public int page;
	}

	public static string URL = "ajax/trading-post/shop-item-detail";

	public List<TradingPostDetail> result = new List<TradingPostDetail>();
}
