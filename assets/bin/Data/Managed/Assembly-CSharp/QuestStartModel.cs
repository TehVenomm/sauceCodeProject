using Network;

public class QuestStartModel : BaseModel
{
	public class RequestSendForm
	{
		public int qid;

		public string qt;

		public int setNo;

		public int crystalCL;

		public int free;

		public int dId;

		public string d;

		public TaskUpdateInfo actioncount = new TaskUpdateInfo();
	}

	public static string URL = "ajax/quest/start";

	public QuestStartData result = new QuestStartData();
}
