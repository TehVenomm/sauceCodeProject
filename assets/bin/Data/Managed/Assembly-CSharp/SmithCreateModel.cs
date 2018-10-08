using System;

public class SmithCreateModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public string equipUniqId = string.Empty;
	}

	public class RequestSendForm
	{
		public int cid;
	}

	public static string URL = "ajax/smith/create";

	public Param result = new Param();
}
