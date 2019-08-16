using System.Collections.Generic;

public class PartySearchModel : BaseModel
{
	public class Param
	{
		public List<PartyModel.Party> partys = new List<PartyModel.Party>();
	}

	public class RequestSendForm
	{
		public int order;

		public int rarityBit;

		public int elementBit;

		public int enemyLevelMin;

		public int enemyLevelMax;

		public int enemySpecies;

		public int questTypeBit;

		public int isFs;

		public int isCs;
	}

	public static string URL = "ajax/party/search";

	public Param result = new Param();
}
