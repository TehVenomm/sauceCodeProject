using Network;
using System;
using System.Collections.Generic;

public class OnceInventoryModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public List<EquipItem> equipItem = new List<EquipItem>();

		public List<SkillItem> skillItem = new List<SkillItem>();

		public List<Item> item = new List<Item>();

		public List<ExpiredItem> expiredItem = new List<ExpiredItem>();

		public List<QuestItem> questItem = new List<QuestItem>();

		public List<AbilityItem> abilityItem = new List<AbilityItem>();
	}

	public class RequestSendForm
	{
		public int req_e;

		public int req_s;

		public int req_i;

		public int req_qi;

		public int req_ai;
	}

	public static string URL = "ajax/once/inventory";

	public Param result = new Param();
}
