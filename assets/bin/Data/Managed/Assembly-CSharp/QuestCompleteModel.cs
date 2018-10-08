using Network;
using System.Collections.Generic;

public class QuestCompleteModel : BaseModel
{
	public class RequestSendForm
	{
		public string qt;

		public List<int> breakIds0 = new List<int>();

		public List<int> breakIds1 = new List<int>();

		public List<int> breakIds2 = new List<int>();

		public List<int> breakIds3 = new List<int>();

		public List<int> breakIds4 = new List<int>();

		public List<int> memids = new List<int>();

		public List<int> mClear = new List<int>();

		public float hpRate = 100f;

		public List<int> givenDamageList = new List<int>();

		public string fieldId = "0";

		public List<BattleUserLog> logs = new List<BattleUserLog>();

		public TaskUpdateInfo actioncount = new TaskUpdateInfo();

		public DeliveryBattleInfo deliveryBattleInfo = new DeliveryBattleInfo();

		public int enemyHp;

		public float remainSec;

		public float elapseSec;

		public int dc;

		public int dbc;

		public int pdbc;

		public float rHp;

		public float rSec;
	}

	public class BattleUserLog
	{
		public class AtkInfo
		{
			public string name;

			public int count;

			public int damage;

			public int skillId;
		}

		public int leaveCnt;

		public int userId;

		public string name;

		public int baseId;

		public int objId;

		public bool isNpc;

		public int hostUserId;

		public float startRemaindTime;

		public List<AtkInfo> atkInfos = new List<AtkInfo>();

		public void AddAtkInfo(string name, int count, int damage, int skillId = 0)
		{
			AtkInfo atkInfo = new AtkInfo();
			atkInfo.name = name;
			atkInfo.count = count;
			atkInfo.damage = damage;
			atkInfo.skillId = skillId;
			atkInfos.Add(atkInfo);
		}
	}

	public static string URL = "ajax/quest/complete";

	public QuestCompleteData result = new QuestCompleteData();
}
