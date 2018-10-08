using Network;
using System;
using System.Collections.Generic;

public class InventorySellQuestModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public SellQuestItemReward reward = new SellQuestItemReward();
	}

	public class RequestSendForm
	{
		public List<string> uids = new List<string>();

		public List<int> nums;
	}

	public static string URL = "ajax/inventory/sellquest";

	public Param result = new Param();
}
