public class StatusVisualEquipModel : BaseModel
{
	public class RequestSendForm
	{
		public string auid;

		public string huid;

		public string ruid;

		public string luid;
	}

	public static string URL = "ajax/status/visualequip";
}
