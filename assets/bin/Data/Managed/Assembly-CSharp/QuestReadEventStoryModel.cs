public class QuestReadEventStoryModel : BaseModel
{
	public class RequestSendForm
	{
		public int eventId;
	}

	public static string URL = "ajax/quest/read-event-story";
}
