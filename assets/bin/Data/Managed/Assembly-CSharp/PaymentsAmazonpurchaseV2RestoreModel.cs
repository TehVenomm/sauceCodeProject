public class PaymentsAmazonpurchaseV2RestoreModel : BaseModel
{
	public class RequestSendForm
	{
		public string mainToken;

		public string amazonUserId;

		public string receipts;
	}

	public static string URL = "ajax/payments/amazonpurchase/v2/restore";

	public int code;

	public string name;
}
