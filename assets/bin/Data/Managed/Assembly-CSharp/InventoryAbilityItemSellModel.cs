using System.Collections.Generic;

public class InventoryAbilityItemSellModel : BaseModel
{
	public class RequestSendForm
	{
		public List<string> uids = new List<string>();
	}

	public static string URL = "ajax/inventory/sellabilityitem";
}
