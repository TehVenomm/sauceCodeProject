public class PaymentsAupurchaseDepositModel : BaseModel
{
	public class RequestSendForm
	{
		public string mainToken;

		public string signature;

		public string signedData;
	}

	public static string URL = "ajax/payments/aupurchase/deposit";

	public int code;

	public string name;
}
