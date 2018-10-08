public class StatusDetachSkillModel : BaseModel
{
	public class RequestSendForm
	{
		public string euid;

		public int slot;

		public int no;
	}

	public static string URL = "ajax/status/detachskill2";
}
