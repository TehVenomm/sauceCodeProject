public class StatusEquipSetNameChangeModel : BaseModel
{
	public class RequestSendForm
	{
		public string name;

		public int setNo;
	}

	public static string URL = "ajax/status/equipsetname";
}
