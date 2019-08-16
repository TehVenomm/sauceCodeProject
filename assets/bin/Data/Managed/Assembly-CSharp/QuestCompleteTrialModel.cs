using System.Collections.Generic;

public class QuestCompleteTrialModel : BaseModel
{
	public class RequestSendForm
	{
		public string qt;

		public List<int> mClear = new List<int>();

		public int enemyHp;
	}

	public static string URL = "ajax/quest/completetrial";
}
