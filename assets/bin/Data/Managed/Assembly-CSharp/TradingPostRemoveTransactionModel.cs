public class TradingPostRemoveTransactionModel : BaseModel
{
	public class RequestSendForm
	{
		public int transactionId;
	}

	public static string URL = "ajax/trading-post/remove-item";
}
