using Network;
using System;

public class QuestStoryCompleteModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public StoryRewardList reward = new StoryRewardList();

		public TaskUpdateInfo actioncount = new TaskUpdateInfo();
	}

	public class RequestSendForm
	{
		public int qid;
	}

	public static string URL = "ajax/quest/storycomplete";

	public Param result = new Param();
}
