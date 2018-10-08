using Network;
using System;
using System.Collections.Generic;

public class OnceGatherListModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public int gathering;

		public List<GatherPointData> gather = new List<GatherPointData>();
	}

	public static string URL = "ajax/once/gatherlist";

	public Param result = new Param();
}
