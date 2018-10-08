public class PartyNextModel : BaseModel
{
	public class RequestSendForm
	{
		public string id;
	}

	public static string URL = "ajax/party/repeatwaiting";

	public PartyModel.Param result = new PartyModel.Param();
}
