using System.Collections.Generic;

public class PartySearchRushModel : BaseModel
{
	public class Param
	{
		public List<PartyModel.Party> partys = new List<PartyModel.Party>();
	}

	public class RequestSendForm
	{
		public int floorMinQuestId;

		public int floorMaxQuestId;
	}

	public static string URL = "ajax/party/towersearchparty";

	public Param result = new Param();
}
