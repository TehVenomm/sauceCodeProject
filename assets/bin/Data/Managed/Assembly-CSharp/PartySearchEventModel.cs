using System.Collections.Generic;

public class PartySearchEventModel : BaseModel
{
	public class Param
	{
		public List<PartyModel.Party> partys = new List<PartyModel.Party>();
	}

	public class RequestSendForm
	{
		public int eid;
	}

	public static string URL = "ajax/party/event-search";

	public Param result = new Param();
}
