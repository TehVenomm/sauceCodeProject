public class GoldCanPurchaseModel : BaseModel
{
	public class RequestSendForm
	{
		public string productId;

		public string safetyLockPassword;

		public int marketId;
	}

	public static string URL = "ajax/gold/canpurchase";
}
