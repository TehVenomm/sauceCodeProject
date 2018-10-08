using Network;

public class LoungeRallyListModel : BaseModel
{
	public class RequestSendForm
	{
		public string id;
	}

	public static string URL = "ajax/lounge/rally";

	public LoungeRallyCharaInfo result = new LoungeRallyCharaInfo();
}
