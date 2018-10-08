public class PointShopBuyModel : BaseModel
{
	public class SendForm
	{
		public string uid;

		public int num;
	}

	public static string URL = "ajax/pointshop/buy";
}
