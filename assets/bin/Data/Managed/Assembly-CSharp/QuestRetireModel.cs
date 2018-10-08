using Network;
using System;
using System.Collections.Generic;

public class QuestRetireModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public List<FollowPartyMember> friend;

		public int followNum;
	}

	public class RequestSendForm
	{
		public string qt;

		public int timeout;

		public List<int> memids = new List<int>();

		public int wave;

		public string fieldId = "0";

		public List<QuestCompleteModel.BattleUserLog> logs = new List<QuestCompleteModel.BattleUserLog>();

		public TaskUpdateInfo actioncount = new TaskUpdateInfo();

		public int enemyHp;
	}

	public static string URL = "ajax/quest/retire";

	public Param result = new Param();
}
