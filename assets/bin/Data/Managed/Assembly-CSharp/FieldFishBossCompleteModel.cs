using Network;
using System;

public class FieldFishBossCompleteModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public FieldGatherRewardList reward = new FieldGatherRewardList();
	}

	public class RequestSendForm
	{
		public int ownerUserId;

		public int isSuccess;
	}

	public static string URL = "ajax/field/fishing-boss-complete";

	public Param result = new Param();
}
