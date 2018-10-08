public class DebugAddCrystalModel : BaseModel
{
	public class RequestSendForm
	{
		public string productId;
	}

	public static string URL = "ajax/debug/addcrystal";

	public ShopReceiver.PaymentPurchaseData result;
}
