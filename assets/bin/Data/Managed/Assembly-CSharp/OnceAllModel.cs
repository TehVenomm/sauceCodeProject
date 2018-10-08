using Network;
using System.Collections.Generic;

public class OnceAllModel : BaseModel
{
	public class Param
	{
		public OnceStatusInfoModel.Param statusinfo;

		public OnceTraveledListModel.Param traveledlist;

		public OnceInventoryModel.Param inventory;

		public OnceDeliveryModel.Param delivery;

		public OnceClearStatusModel.Param clearstatus;

		public List<int> blacklist;

		public OnceAchievementModel.Param achievement;

		public List<TaskInfo> task;

		public List<int> region;

		public List<GuildRequestItem> guildRequestItemList;
	}

	public class RequestSendForm
	{
		public int req_e;

		public int req_s;

		public int req_i;

		public int req_qi;

		public int req_ai;

		public int req_ac;
	}

	public static string URL = "ajax/once/all";

	public Param result = new Param();
}
