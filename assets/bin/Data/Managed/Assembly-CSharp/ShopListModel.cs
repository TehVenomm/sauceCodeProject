using Network;

public class ShopListModel : BaseModel
{
	public class RequestSendForm
	{
	}

	public static string URL = "ajax/shop/list";

	public ShopList result = new ShopList();
}
