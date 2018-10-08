public class InventoryAutoItemModel : BaseModel
{
	public class Param
	{
		public double timeLeft;
	}

	public class RequestSendForm
	{
		public string uid;
	}

	public static string URL = "ajax/inventory/useautopotion";

	public Param result = new Param();
}
