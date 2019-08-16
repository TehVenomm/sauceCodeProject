public class UniqueStatusEquipSkillModel : BaseModel
{
	public class RequestSendForm
	{
		public string euid;

		public string suid;

		public int slot;
	}

	public static string URL = "ajax/status/unique-equip-skill";
}
