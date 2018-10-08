using Network;
using System.Collections.Generic;

public class QuestChallengeEnemyModel : BaseModel
{
	public class Param
	{
		public List<QuestData> shadow = new List<QuestData>();
	}

	public class RequestSendForm
	{
		public int enemyId;
	}

	public static string URL = "ajax/quest/challenge-enemy";

	public Param result = new Param();
}
