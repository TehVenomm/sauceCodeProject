public class PaymentsApppurchaseDepositModel : BaseModel
{
	public class RequestSendForm
	{
		public string mainToken;

		public string receiptData;

		public string ua;

		public string clientVer;
	}

	public static string URL = "ajax/payments/apppurchase/deposit";

	public int code;

	public string name;
}
