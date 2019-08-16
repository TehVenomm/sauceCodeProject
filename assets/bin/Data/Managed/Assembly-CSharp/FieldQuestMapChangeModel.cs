using Network;
using System.Collections.Generic;

public class FieldQuestMapChangeModel : BaseModel
{
	public class Param
	{
		public List<int> gather;

		public List<GatherGrowthInfo> growth;
	}

	public class RequestSendForm
	{
		public int mapId;
	}

	public static string URL = "ajax/field/quest-map-change";

	public Param result = new Param();
}
