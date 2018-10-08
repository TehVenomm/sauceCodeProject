public class StatusDetachAllSkillModel : BaseModel
{
	public class RequestSendForm
	{
		public string euid;

		public int no;
	}

	public static string URL = "ajax/status/detachskillall";
}
