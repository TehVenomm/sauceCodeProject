using Network;
using System.Collections.Generic;

public class ArenaProgressModel : BaseModel
{
	public class RequestSendForm
	{
		public int wave;

		public string qt;

		public XorInt remainMilliSec;

		public XorInt elapseMilliSec;

		public List<int> breakIds = new List<int>();

		public int enemyHp;

		public List<QuestCompleteModel.BattleUserLog> logs = new List<QuestCompleteModel.BattleUserLog>();

		public TaskUpdateInfo actioncount = new TaskUpdateInfo();

		public DeliveryBattleInfo deliveryBattleInfo = new DeliveryBattleInfo();
	}

	public static string URL = "ajax/arena/progress";

	public QuestArenaProgressData result = new QuestArenaProgressData();
}
