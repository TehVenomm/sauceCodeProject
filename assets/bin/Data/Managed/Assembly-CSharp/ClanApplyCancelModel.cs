public class ClanApplyCancelModel : BaseModel
{
	public class Param
	{
	}

	public class RequestSendForm
	{
		public string cId;
	}

	public static string URL = "ajax/clan/apply-cancel";

	public Param result = new Param();
}
