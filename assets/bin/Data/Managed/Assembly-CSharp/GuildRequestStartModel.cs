public class GuildRequestStartModel : BaseModel
{
	public class RequestSendForm
	{
		public int slotNo;

		public int questId;

		public int num;

		public int isQuestItem;
	}

	public static string URL = "ajax/guild-request/start";
}
