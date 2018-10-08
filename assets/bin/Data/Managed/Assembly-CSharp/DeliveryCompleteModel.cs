using Network;
using System;
using System.Collections.Generic;

public class DeliveryCompleteModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public DeliveryRewardList reward = new DeliveryRewardList();

		public List<int> openRegionIds;
	}

	public class RequestSendForm
	{
		public string uId;
	}

	public static string URL = "ajax/delivery/complete";

	public Param result = new Param();
}
