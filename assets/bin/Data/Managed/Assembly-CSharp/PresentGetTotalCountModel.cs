using System;

public class PresentGetTotalCountModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public int totalCount;
	}

	public static string URL = "ajax/present/get-total-count";

	public Param result = new Param();
}
