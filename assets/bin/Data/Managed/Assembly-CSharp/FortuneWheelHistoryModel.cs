using Network;

public class FortuneWheelHistoryModel : BaseModel
{
	public static string URL = "ajax/dragon-vault/history";

	public DarkMarketItemList result = new DarkMarketItemList();
}
