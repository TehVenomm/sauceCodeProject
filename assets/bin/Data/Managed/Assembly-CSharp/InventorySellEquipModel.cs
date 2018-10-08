using Network;
using System.Collections.Generic;

public class InventorySellEquipModel : BaseModel
{
	public class RequestSendForm
	{
		public List<string> uids = new List<string>();
	}

	public static string URL = "ajax/inventory/sellequip";

	public SellResult result = new SellResult();
}
