using Network;

public class ArenaCompleteModel : BaseModel
{
	public class RequestSendForm
	{
		public XorInt remainMilliSec;

		public XorInt totalElapseMilliSec;
	}

	public static string URL = "ajax/arena/complete";

	public QuestArenaCompleteData result = new QuestArenaCompleteData();
}
