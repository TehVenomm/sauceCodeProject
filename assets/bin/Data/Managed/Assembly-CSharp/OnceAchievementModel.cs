using Network;
using System;
using System.Collections.Generic;

public class OnceAchievementModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public List<AchievementCounter> achievement;

		public EquipItemCollectionList equipCollection;
	}

	public static string URL = "ajax/once/achievement";

	public Param result = new Param();
}
