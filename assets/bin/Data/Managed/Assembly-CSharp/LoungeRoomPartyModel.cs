using System.Collections.Generic;

public class LoungeRoomPartyModel : BaseModel
{
	public class Param
	{
		public List<PartyModel.Party> parties = new List<PartyModel.Party>();
	}

	public class RequestSendForm
	{
		public int id;
	}

	public static string URL = "ajax/lounge/roomparty";

	public Param result = new Param();
}
