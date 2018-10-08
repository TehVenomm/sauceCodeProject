using Network;

public class TradingPostHistoryLogModel : BaseModel
{
	public static string URL = "ajax/trading-post/active-history-log";

	public TradingPostTransactionLog result = new TradingPostTransactionLog();
}
