public class UniqueStatusDetachSkillModel : BaseModel
{
	public class RequestSendForm
	{
		public string euid;

		public int slot;
	}

	public static string URL = "ajax/status/unique-detach-skill";
}
