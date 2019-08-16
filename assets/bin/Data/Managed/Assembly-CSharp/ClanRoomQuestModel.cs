using System.Collections.Generic;

public class ClanRoomQuestModel : BaseModel
{
	public class Param
	{
		public List<PartyModel.Party> parties = new List<PartyModel.Party>();
	}

	public static string URL = "ajax/clan/party-search";

	public Param result = new Param();
}
