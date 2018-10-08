using Network;
using System;
using System.Collections.Generic;

public class OnceDeliveryModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public List<Delivery> delivery = new List<Delivery>();

		public List<ClearStatusDelivery> clearStatusDelivery = new List<ClearStatusDelivery>();
	}

	public static string URL = "ajax/once/delivery";

	public Param result = new Param();
}
