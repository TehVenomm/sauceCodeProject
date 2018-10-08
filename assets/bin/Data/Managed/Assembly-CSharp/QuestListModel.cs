using Network;
using System.Collections.Generic;

public class QuestListModel : BaseModel
{
	public class Param
	{
		public List<QuestData> quests = new List<QuestData>();

		public List<QuestData> order = new List<QuestData>();

		public List<QuestData> explores = new List<QuestData>();

		public List<Network.EventData> events = new List<Network.EventData>();

		public List<int> futureEventIds = new List<int>();

		public List<Network.EventData> bingoEvents = new List<Network.EventData>();

		public float dailyRemainTime = -1f;

		public float weeklyRemainTime = -1f;

		public int carnivalEventId;
	}

	public class RequestSendForm
	{
		public int req_q;

		public int req_gq;

		public int req_eq;

		public int req_d;

		public int req_e;

		public int req_bingo;
	}

	public static string URL = "ajax/quest/list";

	public Param result = new Param();
}
