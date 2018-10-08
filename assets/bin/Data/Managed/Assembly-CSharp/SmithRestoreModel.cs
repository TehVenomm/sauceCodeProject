public class SmithRestoreModel : BaseModel
{
	public class RequestSendForm
	{
		public string euid;

		public int crystalCL;
	}

	public static string URL = "ajax/smith/restore";
}
