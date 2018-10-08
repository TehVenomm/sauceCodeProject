public class GoPayRequestModel : BaseModel
{
	public class SendForm
	{
		public string mainToken;

		public string productId;
	}

	public static string URL = "ajax/payments-secure/gopay/request";

	public string payload;
}
