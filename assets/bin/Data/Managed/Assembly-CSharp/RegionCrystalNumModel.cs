using System;

public class RegionCrystalNumModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public int crystalNum;

		public string text;
	}

	public class RequestSendForm
	{
		public int regionId;
	}

	public static string URL = "ajax/region/crystalnum";

	public Param result = new Param();
}
