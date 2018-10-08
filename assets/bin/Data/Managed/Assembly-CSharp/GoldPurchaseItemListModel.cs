using Network;

public class GoldPurchaseItemListModel : BaseModel
{
	public class SendForm
	{
		public string checkSum;

		public int purchaseType;
	}

	public static string URL = "ajax/gold/purchaseitemlist";

	public ProductDataList result = new ProductDataList();
}
