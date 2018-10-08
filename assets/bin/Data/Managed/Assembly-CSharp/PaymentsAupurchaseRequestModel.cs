public class PaymentsAupurchaseRequestModel : BaseModel
{
	public class RequestSendForm
	{
		public string mainToken;

		public string payload;

		public string itemId;

		public int permItem;
	}

	public static string URL = "ajax/payments/aupurchase/request";

	public int code;

	public string name;
}
