public class GuildRequestExtendModel : BaseModel
{
	public class RequestSendForm
	{
		public int slotNo;

		public int crystalCL;
	}

	public static string URL = "ajax/guild-request/extend";
}
