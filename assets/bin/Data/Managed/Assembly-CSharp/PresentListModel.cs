using Network;

public class PresentListModel : BaseModel
{
	public class RequestSendForm
	{
		public int page;
	}

	public static string URL = "ajax/present/list";

	public PresentList result = new PresentList();
}
