using Network;
using System.Collections.Generic;

public class UniqueStatusEquipModel : BaseModel
{
	public class RequestSendForm
	{
		public int select;

		public List<int> nos = new List<int>();

		public List<string> wuids0 = new List<string>();

		public List<string> wuids1 = new List<string>();

		public List<string> wuids2 = new List<string>();

		public List<string> auids = new List<string>();

		public List<string> ruids = new List<string>();

		public List<string> luids = new List<string>();

		public List<string> huids = new List<string>();

		public List<int> shows = new List<int>();

		public List<AccessoryPlaceInfo> accs = new List<AccessoryPlaceInfo>();
	}

	public static string URL = "ajax/status/unique-equip";
}
