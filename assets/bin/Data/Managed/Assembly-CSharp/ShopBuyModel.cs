using Network;

public class ShopBuyModel : BaseModel
{
	public class RequestSendForm
	{
		public int id;

		public int crystalCL;
	}

	public static string URL = "ajax/shop/buy";

	public ShopBuyResult result = new ShopBuyResult();
}
