using System.Collections.Generic;

public class UniqueEquipSetOrderModel : BaseModel
{
	public class RequestSendForm
	{
		public List<int> selects;

		public List<int> nos;
	}

	public static string URL = "ajax/status/unique-equip-set-order";
}
