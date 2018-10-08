using Network;
using System.Collections.Generic;

public class InventorySellSkillModel : BaseModel
{
	public class RequestSendForm
	{
		public List<string> uids = new List<string>();
	}

	public static string URL = "ajax/inventory/sellskill";

	public SellResult result = new SellResult();
}
