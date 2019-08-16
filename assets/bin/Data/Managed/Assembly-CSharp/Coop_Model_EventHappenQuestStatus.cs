using System;
using System.Collections.Generic;

public class Coop_Model_EventHappenQuestStatus : Coop_Model_Base
{
	[Serializable]
	public class Status
	{
		public const int ORDER_TYPE_DEFEAT_ENEMY = 2;

		public const int ORDER_TYPE_REMAINING_TIME = 3;

		public int orderType;

		public int order_0;

		public int order_1;

		public int order_2;

		public int defeatEnemyNum;

		public int remainingTime;
	}

	public List<Status> statusList = new List<Status>();

	public Coop_Model_EventHappenQuestStatus()
	{
		base.packetType = PACKET_TYPE.EVENT_HAPPEN_QUEST_STATUS;
	}

	public override string ToString()
	{
		string status_str = string.Empty;
		statusList.ForEach(delegate(Status s)
		{
			status_str += $"(type:{s.orderType},0:{s.order_0},1:{s.order_1},2:{s.order_2},now:{((s.orderType == 2) ? s.defeatEnemyNum : ((s.orderType == 3) ? s.remainingTime : 0))}";
		});
		return base.ToString() + status_str;
	}
}
