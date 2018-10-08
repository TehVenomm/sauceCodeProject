public class DebugAddItemModel : BaseModel
{
	public class RequestSendForm
	{
		public int itemId;

		public int num;
	}

	public static string URL = "ajax/debug/additem";
}
