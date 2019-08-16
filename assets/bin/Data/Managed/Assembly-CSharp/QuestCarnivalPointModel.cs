public class QuestCarnivalPointModel : BaseModel
{
	public class Param
	{
		public int point;

		public int rank;

		public int pointForNextClass;

		public int status;

		public string rankingURL;

		public string linkName;

		public int border;

		public string eventName;

		public int maxLevel;

		public int clearTime;
	}

	public class RequestSendForm
	{
		public int eid;
	}

	public static string URL = "ajax/quest/carnivalpoint";

	public Param result = new Param();
}
