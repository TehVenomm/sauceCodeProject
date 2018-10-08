using Network;
using System;
using System.Collections.Generic;

public class OnceClearStatusModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public List<ClearStatusQuest> clearStatusQuest = new List<ClearStatusQuest>();

		public List<ClearStatusQuestEnemySpecies> clearStatusQuestEnemySpecies = new List<ClearStatusQuestEnemySpecies>();
	}

	public static string URL = "ajax/once/clearstatus";

	public Param result = new Param();
}
