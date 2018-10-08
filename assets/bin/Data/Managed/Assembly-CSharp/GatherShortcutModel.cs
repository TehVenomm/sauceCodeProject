public class GatherShortcutModel : BaseModel
{
	public class RequestSendForm
	{
		public int pid;

		public int crystalCL;
	}

	public static string URL = "ajax/gather/shortcut";
}
