using System.Collections.Generic;

public class LoungeSearchModel : BaseModel
{
	public class Param
	{
		public List<LoungeModel.Lounge> lounges = new List<LoungeModel.Lounge>();
	}

	public class RequestSendForm
	{
		public int order;

		public int label;

		public string name;
	}

	public static string URL = "ajax/lounge/search";

	public Param result = new Param();
}
