public class UniqueEquipSetNameChangeModel : BaseModel
{
	public class RequestSendForm
	{
		public string name;

		public int setNo;
	}

	public static string URL = "ajax/status/unique-equip-set-name";
}
