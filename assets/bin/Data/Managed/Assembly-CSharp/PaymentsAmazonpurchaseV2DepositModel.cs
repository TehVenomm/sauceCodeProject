public class PaymentsAmazonpurchaseV2DepositModel : BaseModel
{
	public class RequestSendForm
	{
		public string mainToken;

		public string amazonUserId;

		public string sku;

		public string receiptId;
	}

	public static string URL = "ajax/payments/amazonpurchase/v2/deposit";

	public int code;

	public string name;
}
