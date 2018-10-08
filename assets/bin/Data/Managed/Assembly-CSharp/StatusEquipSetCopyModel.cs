using System.Collections.Generic;

public class StatusEquipSetCopyModel : BaseModel
{
	public class RequestSendForm
	{
		public int no;

		public string name;

		public string wuid0;

		public string wuid1;

		public string wuid2;

		public string auid;

		public string ruid;

		public string luid;

		public string huid;

		public int show;

		public List<string> euids = new List<string>();

		public List<string> suids = new List<string>();

		public List<int> slots = new List<int>();

		public override string ToString()
		{
			return string.Format("RequestSendForm no:{0}, name: {1}, wuid0:{2}, wuid1:{3}, wuid2:{4}, auid:{5}, ruid:{6}, luid:{7}, huid:{8}, show:{9}\n euids:{10}, Count:{11}\n suids:{12}, Count:{13}\n slots:{14}, Count:{15}", no, name, wuid0, wuid1, wuid2, auid, ruid, luid, huid, show, euids.ToJoinString(",", null), euids.Count, suids.ToJoinString(",", null), suids.Count, slots.ToJoinString(",", null), slots.Count);
		}
	}

	public static string URL = "ajax/status/copyequipset";
}
