using Network;

public class DebugAddPresentModel : BaseModel
{
	public class RequestSendForm
	{
		public int type;

		public int actionType;

		public string comment;

		public int num;

		public int id;

		public int p0;

		public int p1;
	}

	public static string URL = "ajax/debug/addpresent";

	public Present result = new Present();
}
