using Network;

public class GatherStartModel : BaseModel
{
	public class RequestSendForm
	{
		public int pid;
	}

	public static string URL = "ajax/gather/start";

	public GatherEnterData result = new GatherEnterData();
}
