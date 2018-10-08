using Network;
using System;
using System.Collections.Generic;

public class ArenaLegendRankingModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public Network.EventData eventData;

		public ArenaRankingData rankingData = new ArenaRankingData();
	}

	public static string URL = "ajax/arena/legend-ranking";

	public List<Param> result = new List<Param>();
}
