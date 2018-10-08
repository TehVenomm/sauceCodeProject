using Network;
using System.Collections.Generic;

public class QuestChallengeListModel : BaseModel
{
	public class Param
	{
		public List<QuestData> shadow = new List<QuestData>();
	}

	public class RequestSendForm
	{
		public int rarityBit;

		public int elementBit;

		public int enemyLevel;

		public int enemySpeciesId;

		public string enemySpeciesName;

		public RequestSendForm()
		{
			rarityBit = 8388607;
			elementBit = 8388607;
			enemyLevel = 10;
			enemySpeciesId = 0;
		}
	}

	public static string URL = "ajax/quest/challenge-list";

	public Param result = new Param();
}
