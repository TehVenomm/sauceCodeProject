using Network;
using System.Collections.Generic;

public class ArenaRetireModel : BaseModel
{
	public class RequestSendForm
	{
		public string qt;

		public int timeout;

		public int wave;

		public int enemyHp;

		public List<QuestCompleteModel.BattleUserLog> logs = new List<QuestCompleteModel.BattleUserLog>();

		public TaskUpdateInfo actioncount = new TaskUpdateInfo();
	}

	public static string URL = "ajax/arena/retire";

	public QuestRetireModel.Param result = new QuestRetireModel.Param();
}
