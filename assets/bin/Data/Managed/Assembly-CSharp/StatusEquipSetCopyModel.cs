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
			return $"RequestSendForm no:{no}, name: {name}, wuid0:{wuid0}, wuid1:{wuid1}, wuid2:{wuid2}, auid:{auid}, ruid:{ruid}, luid:{luid}, huid:{huid}, show:{show}\n euids:{euids.ToJoinString()}, Count:{euids.Count}\n suids:{suids.ToJoinString()}, Count:{suids.Count}\n slots:{slots.ToJoinString()}, Count:{slots.Count}";
		}
	}

	public static string URL = "ajax/status/copyequipset";
}
