public class QuestStoryReadModel : BaseModel
{
	public class RequestSendForm
	{
		public int qid;

		public int prev = 1;
	}

	public static string URL = "ajax/quest/storyread";
}
