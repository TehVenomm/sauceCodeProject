using System.Collections.Generic;

public class QuestRushPointModel : BaseModel
{
	public class Param
	{
		public class Reward
		{
			public int type;

			public int itemId;

			public int num;
		}

		public class NextData
		{
			public int point;

			public List<Reward> reward = new List<Reward>();
		}

		public int point;

		public NextData reward;
	}

	public class RequestSendForm
	{
		public int eid;
	}

	public static string URL = "ajax/quest/rushpoint";

	public Param result = new Param();
}
