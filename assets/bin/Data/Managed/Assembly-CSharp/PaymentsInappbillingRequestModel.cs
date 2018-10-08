public class PaymentsInappbillingRequestModel : BaseModel
{
	public class RequestSendForm
	{
		public string mainToken;

		public string payload;

		public string itemId;
	}

	public static string URL = "ajax/payments/inappbilling/request";

	public int code;

	public string name;
}
