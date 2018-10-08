public class StatusEquipSetModel : BaseModel
{
	public class RequestSendForm
	{
		public int no;

		public string partyId;
	}

	public static string URL = "ajax/status/equipset";
}
