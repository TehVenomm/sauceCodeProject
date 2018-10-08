using Network;
using System.Collections.Generic;

public class QuestRushProgressModel : BaseModel
{
	public class RequestSendForm
	{
		public int wave;

		public string qt;

		public int remainSec;

		public List<int> breakIds = new List<int>();

		public List<int> memids = new List<int>();

		public List<int> mClear = new List<int>();

		public float hpRate = 100f;

		public List<int> givenDamageList = new List<int>();

		public string fieldId = "0";

		public List<QuestCompleteModel.BattleUserLog> logs = new List<QuestCompleteModel.BattleUserLog>();

		public TaskUpdateInfo actioncount = new TaskUpdateInfo();

		public DeliveryBattleInfo deliveryBattleInfo = new DeliveryBattleInfo();

		public int enemyHp;
	}

	public static string URL = "ajax/quest/rush-progress";

	public QuestRushProgressData result = new QuestRushProgressData();
}
