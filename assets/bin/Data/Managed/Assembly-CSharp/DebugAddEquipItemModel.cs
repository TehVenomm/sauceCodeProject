using System.Collections.Generic;

public class DebugAddEquipItemModel : BaseModel
{
	public class RequestSendForm
	{
		public int equipItemId;

		public int level = 1;

		public List<int> aIds = new List<int>();

		public List<int> aPts = new List<int>();

		public int exceedCnt;
	}

	public static string URL = "ajax/debug/addequipitem";
}
