public class GuildRequestExtendAndStartModel : BaseModel
{
	public class RequestSendForm
	{
		public int slotNo;

		public int questId;

		public int num;

		public int isQuestItem;

		public int crystalCL;
	}

	public static string URL = "ajax/guild-request/extend-and-start";
}
