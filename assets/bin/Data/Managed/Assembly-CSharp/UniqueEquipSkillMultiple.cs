using System.Collections.Generic;

public class UniqueEquipSkillMultiple : BaseModel
{
	public class RequestSendForm
	{
		public List<string> euids = new List<string>();

		public List<string> suids = new List<string>();

		public List<string> slots = new List<string>();
	}

	public static string URL = "ajax/status/unique-equip-skill-multiple";
}
