using Network;
using System.Collections.Generic;

public class ClanDeliveryModel : BaseModel
{
	public class Param
	{
		public List<ClanDelivery> deliveryList;
	}

	public static string URL = "ajax/clan/delivery-list";

	public Param result = new Param();
}
