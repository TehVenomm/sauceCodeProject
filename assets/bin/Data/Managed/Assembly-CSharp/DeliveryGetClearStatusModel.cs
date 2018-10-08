using Network;
using System;
using System.Collections.Generic;

public class DeliveryGetClearStatusModel : BaseModel
{
	public class RequestSendForm
	{
		public List<int> conditionTypes;
	}

	[Serializable]
	public class Param
	{
		public List<ClearStatusDelivery> clearStatusDelivery = new List<ClearStatusDelivery>();
	}

	public static string URL = "ajax/delivery/get-clear-status";

	public Param result = new Param();
}
