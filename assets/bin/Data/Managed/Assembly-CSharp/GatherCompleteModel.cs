using Network;
using System;

public class GatherCompleteModel : BaseModel
{
	[Serializable]
	public class Param : GatherEnterData
	{
		public bool isNewOpen;

		public GatherRewardList reward = new GatherRewardList();
	}

	public class RequestSendForm
	{
		public int pid;
	}

	public static string URL = "ajax/gather/complete";

	public Param result = new Param();
}
