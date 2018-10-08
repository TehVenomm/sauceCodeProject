public class GoPayDepositModel : BaseModel
{
	public class SendForm
	{
		public string mainToken;

		public string productId;

		public string orderId;
	}

	public static string URL = "ajax/payments-secure/gopay/deposit";

	public ShopReceiver.PaymentPurchaseData result;
}
