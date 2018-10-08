using Network;
using System;

public class FieldGatherModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public FieldGatherRewardList reward = new FieldGatherRewardList();
	}

	public class RequestSendForm
	{
		public int pId;
	}

	public static string URL = "ajax/field/gather";

	public Param result = new Param();
}
