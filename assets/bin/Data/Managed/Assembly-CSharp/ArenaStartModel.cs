using Network;

public class ArenaStartModel : BaseModel
{
	public class RequestSendForm
	{
		public int aid;

		public int qid;

		public string qt;

		public int setNo;
	}

	public static string URL = "ajax/arena/start";

	public QuestStartData result = new QuestStartData();
}
