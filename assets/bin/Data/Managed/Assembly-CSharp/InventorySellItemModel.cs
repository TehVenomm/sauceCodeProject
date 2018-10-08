using Network;
using System.Collections.Generic;

public class InventorySellItemModel : BaseModel
{
	public class RequestSendForm
	{
		public List<string> uids = new List<string>();

		public List<int> nums = new List<int>();
	}

	public static string URL = "ajax/inventory/sellitem";

	public SellResult result = new SellResult();
}
