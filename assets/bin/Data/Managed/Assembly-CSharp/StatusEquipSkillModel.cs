public class StatusEquipSkillModel : BaseModel
{
	public class RequestSendForm
	{
		public string euid;

		public string suid;

		public int slot;

		public int no;
	}

	public static string URL = "ajax/status/equipskill2";
}
