using System;

public class PharmacyCreateModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public int itemId;

		public int getNum;
	}

	public class RequestSendForm
	{
		public int cid;

		public int cnt;
	}

	public static string URL = "ajax/pharmacy/create";

	public Param result = new Param();
}
