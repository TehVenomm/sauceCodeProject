public class PaymentsInappbillingDepositModel : BaseModel
{
	public class RequestSendForm
	{
		public string mainToken;

		public string signature;

		public string signedData;

		public int iabver = 3;
	}

	public static string URL = "ajax/payments/inappbilling/deposit";

	public int code;

	public string name;
}
