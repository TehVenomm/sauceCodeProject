public class DebugSetEquipTargetNumModel : BaseModel
{
	public class RequestSendForm
	{
		public int targetNum;

		public int collectionNum;
	}

	public static string URL = "ajax/debug/setequiptargetnum";
}
