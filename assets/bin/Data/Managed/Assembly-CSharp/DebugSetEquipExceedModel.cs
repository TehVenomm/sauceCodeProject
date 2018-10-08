public class DebugSetEquipExceedModel : BaseModel
{
	public class RequestSendForm
	{
		public string euid;

		public int exceedCnt;
	}

	public static string URL = "ajax/debug/setequipexceed";
}
