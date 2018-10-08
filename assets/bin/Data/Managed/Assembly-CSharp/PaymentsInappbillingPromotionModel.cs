public class PaymentsInappbillingPromotionModel : BaseModel
{
	public class RequestSendForm
	{
		public string mainToken;

		public string signature;

		public string signedData;
	}

	public static string URL = "ajax/payments/inappbilling/promotion";

	public int code;

	public string name;
}
