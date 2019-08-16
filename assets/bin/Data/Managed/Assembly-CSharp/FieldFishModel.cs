using Network;
using System;

public class FieldFishModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public FieldGatherRewardList reward = new FieldGatherRewardList();

		public int hitBoss;
	}

	public class RequestSendForm
	{
		public int lotId;

		public int time;

		public int isPop;
	}

	public static string URL = "ajax/field/fishing";

	public Param result = new Param();
}
